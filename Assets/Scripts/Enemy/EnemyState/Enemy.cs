using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
[RequireComponent(typeof(Patroller))]
public class Enemy : MonoBehaviour, IStateable, ITriggerDetected
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    [SerializeField] private State _currentState;
    [SerializeField] private TriggerZone _triggerZone;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    private ISpawnerReceiver _spawnerReceiver;
    private Animator _animator;
    private float _currentHealth;

    private void Awake()
    {
        _states.Add(typeof(Patroller), GetComponent<Patroller>());
        _states.Add(typeof(Idle), GetComponent<Idle>());

        foreach (State state in _states.Values)
            state.Initialize(this);
    }

    private void Start()
    {
        _currentState.Enter();

        _animator = GetComponentInChildren<Animator>();
        _triggerZone.Initialize(this);

        ChangeState(typeof(Idle));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }

    public void ChangeState(Type type)
    {
        if (_states.TryGetValue(type, out State state) == false)
        {
            throw new ArgumentException();
        }

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        _animator.SetTrigger("Damaged");

        if (_currentHealth <= 0)
        {
            _spawnerReceiver?.ReturnToPool(this);
        }
    }

    public void SetHealth()
    {
        _currentHealth = _health;
    }

    public void Initialize(ISpawnerReceiver spawnerReceiver)
    {
        _spawnerReceiver = spawnerReceiver;
    }

    public void StartPatrolling()
    {
        ChangeState(typeof(Patroller));
    }
}