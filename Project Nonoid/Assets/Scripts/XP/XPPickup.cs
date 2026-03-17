using UnityEngine;

public class XPPickup : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;

    private Transform _player;
    private int _xpAmount;


    public void Initialize(Transform player, int xpAmount)
    {
        _player = player;
        _xpAmount = xpAmount;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.position);
        
        if (distance <= stopDistance)
        {
            Collect();
            return;
        }
        
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }
    
    private void Collect()
    {
        XPManager.Instance.AddXp(_xpAmount);

        Destroy(gameObject);
    }
}