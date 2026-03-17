using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 _direction;
    private float _moveSpeed;
    public Vector2 MoveDirection { get; private set; }

    private void Update()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        _direction = new Vector2(h, v).normalized;

        MoveDirection = _direction;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = _direction * _moveSpeed;
    }

    public void InitializeMovement(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public void IncreaseSpeed(int amount)
    {
        _moveSpeed += amount;
    }
}