using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackVfxPrefab;
    [SerializeField] private float vfxSpeed;

    private int _damage;
    private float _cooldown;
    private float _lastAttackTime;
    private float _timer;
    private bool _canAttack;


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            return;

        if (!_canAttack)
            return;

        var enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            SpawnAttackVfx(enemy.transform);
        }

        _canAttack = false;
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _cooldown)
        {
            _canAttack = true;
        }
    }

    public void IncreaseDamage(int amount)
    {
        _damage += amount;
    }

    public void Initialize(int damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }

    private void SpawnAttackVfx(Transform enemy)
    {
        GameObject vfxObj = Instantiate(attackVfxPrefab, transform.position, Quaternion.identity);

        var vfx = vfxObj.GetComponent<AttackVFX>();
        vfx.Initialize(enemy, vfxSpeed, () =>
        {
            var enemyHealth = enemy.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
            }
        });
    }
}