using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthIndicator healthIndicatorPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private XPPickup xpOrbPrefab;
    [SerializeField] private int xpReward;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyAttack enemyAttack;

    private Player _player;
    private Health _health;
    private HealthIndicator _healthIndicator;
    private EnemyDataSO _enemyDataSo;

    private IObjectPool<Enemy> _pool;
    public void SetPool(IObjectPool<Enemy> pool) => _pool = pool;

    public void OnBeeHit(float slowPercentage, int damage)
    {
        enemyMovement.ApplySlow(slowPercentage);

        if (damage > 0)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }

    public void SubscribeActionOnDeath(System.Action action)
    {
        _health.RemoveOnDeathSubscription();
        _health.OnDeath += action;
        _health.OnDeath += () => _healthIndicator.gameObject.SetActive(false);
    }

    private void OnDeath()
    {
        if (_healthIndicator != null)
            _healthIndicator.gameObject.SetActive(false);

        enemyMovement.StopMovement();

        if (xpOrbPrefab != null && _player != null)
        {
            var orb = Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
            orb.Initialize(_player.transform, xpReward);
        }

        _pool?.Release(this);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.OnDeath -= OnDeath;
    }

    public void OnSpawn(Player player, EnemyDataSO enemyDataSo, Transform healthIndicatorParent)
    {
        _player = player;
        _enemyDataSo = enemyDataSo;
        spriteRenderer.color = _enemyDataSo.Color;
        _health = new Health(_enemyDataSo.MaxHealth);
        enemyAttack.InitializeAttack(player, _enemyDataSo);
        enemyMovement.InitializeMovement(player.transform, _enemyDataSo);
        SetupHealthIndicator(healthIndicatorParent);
    }

    public void OnDespawn()
    {
        if (_health != null)
            _health.OnDeath -= OnDeath;

        if (_healthIndicator != null)
            _healthIndicator.gameObject.SetActive(false);

        enemyMovement.StopMovement();
    }

    private void SetupHealthIndicator(Transform healthIndicatorParent)
    {
        if (_healthIndicator == null)
            _healthIndicator = Instantiate(healthIndicatorPrefab, healthIndicatorParent);
        _healthIndicator.Initialize(_health, transform);
        
        _healthIndicator.gameObject.SetActive(true);

        _health.OnDeath += OnDeath;
    }
}