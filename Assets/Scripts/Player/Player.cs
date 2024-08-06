using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerAttack), typeof(Health))]
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;

    private int _wallet;

    public event Action<int> Taken;
    public event Action Dead;

    private void OnEnable() =>
        _health.Died += Died;

    private void OnDisable() =>
        _health.Died -= Died;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _wallet += coin.Collect();

            Taken?.Invoke(_wallet);
        }

        if (collision.gameObject.TryGetComponent(out Heart heart))
            _health.Heal(heart.Heal());
    }

    public void TakeDamage(float damage) =>
        _health.TakeDamage(damage);

    private void Died() =>
        Dead?.Invoke();
}