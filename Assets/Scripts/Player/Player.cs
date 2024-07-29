using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerAttack))]
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;

    private Health _health;
    private int _wallet;

    public event Action<int> Taken;
    public event Action<float> HealthChanged;
    public event Action Dead;

    public float MaxHealth => _health.GetMaxHealth();

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        HealthChanged?.Invoke(_health.GetCurrentHealth());
    }

    private void Awake() =>
        _health = new Health(_maxHealth);

    private void OnEnable()
    {
        _health.Died += Died;
    }

    private void OnDisable()
    {
        _health.Died -= Died;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _wallet += coin.Collect();

            Taken?.Invoke(_wallet);
        }

        if (collision.gameObject.TryGetComponent(out Heart heart))
        {
            _health.Heal(heart.Heal());

            HealthChanged?.Invoke(_health.GetCurrentHealth());
        }
    }

    private void Died()
    {
        Dead?.Invoke();
    }
}