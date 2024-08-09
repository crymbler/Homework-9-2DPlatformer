using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max;

    private float _current;

    public event Action<float> HealthChanged;
    public event Action Died;

    public float Max => _max;

    private void Start()
    {
        _current = _max;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _current -= damage;

        if (_current <= 0)
        {
            _current = 0;

            Died?.Invoke();
        }

        HealthChanged?.Invoke(_current);
    }

    public void TakeHeal(float amount)
    {
        if (amount < 0)
            return;

        _current += amount;

        if (_current > _max)
            _current = _max;

        HealthChanged?.Invoke(_current);
    }
}