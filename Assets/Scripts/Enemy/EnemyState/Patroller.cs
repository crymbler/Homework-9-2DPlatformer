using UnityEngine;

public class Patroller : State
{
    private static readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _raycastDistance;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Animator _animator;

    private float _speed;

    private void Start() =>
        _speed = Random.Range(_minSpeed, _maxSpeed);

    private void FixedUpdate()
    {
        RotateX();
        Move();
    }

    private void RotateX()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);

        RaycastHit2D frontHit = Physics2D.Raycast(transform.position, transform.right, _raycastDistance, _groundLayer);

        if (groundHit.collider == null || frontHit.collider != null)
            transform.rotation *= Quaternion.Euler(0, 180, 0);
    }

    private void Move()
    {
        _animator.SetFloat(Speed, _speed);

        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}