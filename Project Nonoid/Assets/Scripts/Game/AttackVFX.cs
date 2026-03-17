using UnityEngine;

public class AttackVFX : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private System.Action _onHit;

    public void Initialize(Transform target, float speed, System.Action onHit)
    {
        _target = target;
        _speed = speed;
        _onHit = onHit;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            _speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _target.position) < 0.15f)
        {
            _onHit?.Invoke();
            Destroy(gameObject);
        }
    }
}