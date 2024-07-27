using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash(nameof(Speed));
    private static readonly int IsJump = Animator.StringToHash(nameof(IsJump));

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isGround;
    private bool _isJump;
    private float _direction;
    private Rigidbody2D _rigidbody2D;

    public Action Jumped;

    private void Start() =>
        _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
        _direction = Input.GetAxis(nameof(Orientation.Horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            _isJump = true;
    }

    private void FixedUpdate()
    {
        Move();
        Flip();

        if (_isJump)
            Jump();
    }

    private void Flip()
    {
        if (_direction > 0)
            _spriteRenderer.flipX = false;

        else if (_direction < 0)
            _spriteRenderer.flipX = true;
    }

    private void Move()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_direction));
        _rigidbody2D.velocity = new Vector2(_direction * _speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        _animator.SetTrigger(IsJump);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        _isJump = false;
        _isGround = false;

        Jumped?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _isGround = true;
    }
}