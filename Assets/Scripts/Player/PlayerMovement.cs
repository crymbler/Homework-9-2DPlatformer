using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _groundRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    public Action Jumped;

    private bool _onGround = true;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        Flip();

        TryJump();
    }

    private void Flip()
    {
        if(_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void Move()
    {
        _direction.x = Input.GetAxis(nameof(Orientation.Horizontal));

        _animator.SetFloat("Move", Mathf.Abs(_direction.x));

        _rigidbody2D.velocity = new Vector2(_direction.x * _speed, _rigidbody2D.velocity.y);
    }

    private void TryJump()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundMask);

        _animator.SetBool("OnGround", _onGround);

        if (_onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Jumped");

        _rigidbody2D.AddForce(Vector2.up * _jumpForce);

        Jumped?.Invoke();
    }
}