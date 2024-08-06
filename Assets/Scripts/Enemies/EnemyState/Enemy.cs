using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Animator))]
[RequireComponent(typeof(Patroller), typeof(PlayerFollower), typeof(Health))]
public class Enemy : MonoBehaviour, IStateable
{
    private static readonly int Damaged = Animator.StringToHash(nameof(Damaged));

    [SerializeField] private float _damage;

    [SerializeField] private Health _health;
    [SerializeField] private State _currentState;

    [SerializeField] private Animator _animator;
    [SerializeField] private TriggerZone _triggerZone;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    public event Action<Enemy> Died;

    private void Awake()
    {
        _states.Add(typeof(Idle), GetComponent<Idle>());
        _states.Add(typeof(Patroller), GetComponent<Patroller>());
        _states.Add(typeof(PlayerFollower), GetComponent<PlayerFollower>());

        foreach (State state in _states.Values)
            state.Initialize(this);
    }

    private void OnEnable()
    {
        _health.Died += ReturnToPool;
        _triggerZone.Detected += StartFollowing;
        _triggerZone.Missed += StartPatrolling;
    }

    private void OnDisable()
    {
        _health.Died -= ReturnToPool;
        _triggerZone.Detected -= StartFollowing;
        _triggerZone.Missed -= StartPatrolling;
    }

    private void Start()
    {
        _currentState.Enter();

        _animator = GetComponentInChildren<Animator>();

        ChangeState(typeof(Patroller));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }

    public void ChangeState(Type type)
    {
        if (_states.TryGetValue(type, out State state) == false)
            throw new ArgumentException();

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger(Damaged);

        _health.TakeDamage(damage);
    }

    private void ReturnToPool() =>
        Died?.Invoke(this);

    private void StartPatrolling()  =>
        ChangeState(typeof(Patroller));

    private void StartFollowing() =>
        ChangeState(typeof(PlayerFollower));
}
