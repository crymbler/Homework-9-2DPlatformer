using UnityEngine;

public class Patroller : State
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _rotateDirection = 180;
    [SerializeField] private float _RaycastDistance = 1;

    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _rotate;
    private float _speed;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _rotate.y = _rotateDirection;
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetFloat("Move", _speed);

        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if (Physics2D.Raycast(transform.position, transform.right, _RaycastDistance, _groundLayer))
        {
            transform.Rotate(_rotate);
        }
    }
}