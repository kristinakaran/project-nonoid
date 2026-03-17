using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    private float _currentSpeed;
    private Transform _player;
    private EnemyDataSO _enemyDataSo;

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public void InitializeMovement(Transform player, EnemyDataSO data)
    {
        _player = player;
        _currentSpeed = data.MoveSpeed;
        _enemyDataSo = data;
    }

    private void MoveEnemy()
    {
        Vector3 direction = _player.position - transform.position;
        float distance = direction.magnitude;

        float tolerance = 0.1f;

        direction = direction.normalized;

        if (distance > _enemyDataSo.AttackRange - tolerance)
        {
            SmoothAccelerate(direction);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void SmoothAccelerate(Vector2 direction)
    {
        const float acceleration = 12f;
        Vector2 targetVelocity = direction * _currentSpeed;

        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    public void ApplySlow(float percentage)
    {
        _currentSpeed *= 1 - (percentage / 100f);

        StartCoroutine(ResetSpeedAfterDelay());
    }

    private IEnumerator ResetSpeedAfterDelay()
    {
        const float slowDuration = 1f;
        yield return new WaitForSeconds(slowDuration);
        _currentSpeed = _enemyDataSo.MoveSpeed;
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}