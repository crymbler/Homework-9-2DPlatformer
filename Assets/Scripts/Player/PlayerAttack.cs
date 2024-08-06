using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _attackRadius = 5;
    [SerializeField] private PlayerMover _playerMover;

    private void OnEnable() =>
        _playerMover.Jumped += Attack;

    private void OnDisable() =>
        _playerMover.Jumped -= Attack;

    private void Attack()
    {
        foreach (Enemy enemy in GetAttackedObjects())
            enemy.TakeDamage(_damage);
    }

    private List<Enemy> GetAttackedObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        List<Enemy> enemies = new();

        foreach (Collider2D hit in hits)
            if (hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                enemies.Add(enemy);

        return enemies;
    }
}