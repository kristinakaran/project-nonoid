using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitVfxPrefab;
    [SerializeField] private float vfxSpeed;

    private Player _player;
    private EnemyDataSO _enemyDataSo;
    private bool _isInCooldown;
    private Coroutine _attackRoutine;

    private void OnEnable()
    {
        _attackRoutine = StartCoroutine(CheckForAutoAttack());

        _isInCooldown = false;
    }

    private void OnDisable()
    {
        if (_attackRoutine != null)
            StopCoroutine(_attackRoutine);
    }

    public void InitializeAttack(Player player, EnemyDataSO enemyDataSo)
    {
        _player = player;
        _enemyDataSo = enemyDataSo;
    }

    private IEnumerator CheckForAutoAttack()
    {
        while (true)
        {
            yield return null;

            AttackIfPossible();
        }
        // ReSharper disable once IteratorNeverReturns
    }


    private void AttackIfPossible()
    {
        if (_isInCooldown) return;
        if (_player == null) return;

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance <= _enemyDataSo.AttackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        SpawnAttackVfx();

        _player.TakeDamageFromEnemy(_enemyDataSo.Damage);
        _isInCooldown = true;

        StartCoroutine(ResetCooldownAfterDelay());
    }

    private void SpawnAttackVfx()
    {
        if (hitVfxPrefab == null || _player == null) return;

        GameObject vfxObj = Instantiate(hitVfxPrefab, transform.position, Quaternion.identity);

        var vfx = vfxObj.GetComponent<AttackVFX>();
        vfx.Initialize(_player.transform, vfxSpeed, () => { _player.TakeDamageFromEnemy(_enemyDataSo.Damage); });
    }


    private IEnumerator ResetCooldownAfterDelay()
    {
        yield return new WaitForSeconds(_enemyDataSo.AttackRate);

        _isInCooldown = false;
    }
}