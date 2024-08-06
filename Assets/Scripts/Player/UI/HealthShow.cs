using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthShow : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _duration;

    private Coroutine _coroutine;
    private float _maxHealth;

    private void OnEnable()
    {
        _health.HealthChanged += View;
        _maxHealth = _health.MaxHealth;
    }

    private void OnDisable() =>
        _health.HealthChanged -= View;

    private void View(float currentHealth)
    {
        StopCoroutine();
        _coroutine = StartCoroutine(SmoothTakeDamage(currentHealth));
    }

    private IEnumerator SmoothTakeDamage(float currentHealth)
    {
        float destination = currentHealth / _maxHealth;
        float speed = Mathf.Abs(_slider.value - destination) / _duration;

        while (Mathf.Abs(_slider.value - destination) > 0.01f)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, destination, Time.deltaTime * speed);

            yield return null;
        }
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}