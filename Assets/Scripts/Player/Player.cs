using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _attackRadius = 50;

    [SerializeField] private PlayerMovement _playerMovement;

    private float _currentHealth;
    private int _wallet;
    private IHealthReceive _healthReceiver;

    public float MaxHealth => _maxHealth;

    public event Action<int> Taken;

    private void OnEnable()
    {
        _playerMovement.Jumped += Attack;
    }

    private void OnDisable()
    {
        _playerMovement.Jumped -= Attack;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            _wallet += coin.Collect();
            Taken?.Invoke(_wallet);
        }
    }

    private void Attack()
    {
        foreach(Enemy enemy in GetAttackedObjects())
        {
            enemy.TakeDamage(_damage);
        }  
    }

    private List<Enemy> GetAttackedObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        List<Enemy> enemies = new();

        foreach(Collider2D hit in hits)
            if(hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                enemies.Add(enemy);

        return enemies;
    }

    public void Initialize(IHealthReceive healthReceive)
    {
        _healthReceiver = healthReceive;
    }

    public void TakeDamage(float damage)
    {
        _healthReceiver.View(_currentHealth -= damage);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}