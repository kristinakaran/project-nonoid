using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatsDataSO playerStatsData;
    [SerializeField] private HealthIndicator healthIndicatorPrefab;
    [SerializeField] private Transform healthIndicatorParent;
    [SerializeField] private Bubble bubble;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    private Health _health;
    private HealthIndicator _healthIndicator;

    private void OnEnable()
    {
        _health = new Health(playerStatsData.MaxHealth);
        SetHealthIndicator();
        playerMovement.InitializeMovement(playerStatsData.MoveSpeed);
        playerAttack.Initialize(playerStatsData.AttackDamage, playerStatsData.AttackCooldown);
    }

    private void SetHealthIndicator()
    {
        _healthIndicator = Instantiate(healthIndicatorPrefab, healthIndicatorParent);
        _healthIndicator.Initialize(_health, transform);
    }

    public void TakeDamageFromEnemy(int damage)
    {
        if (bubble.IsActive)
        {
            bubble.AbsorbHit();
            return;
        }

        _health.TakeDamage(damage);

        if (_health.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        _health.AddMaxHealth(amount);
        _healthIndicator.Initialize(_health, transform);
    }

    public void IncreaseSpeed(int amount)
    {
        playerMovement.IncreaseSpeed(amount);
    }

    public void IncreaseDamage(int amount)
    {
        playerAttack.IncreaseDamage(amount);
    }

    public void Die()
    {
        Debug.Log("PLAYER DIED!");

        GameManager.Instance.GameOver();
    }
}