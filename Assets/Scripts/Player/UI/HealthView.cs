using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _duration;

    private Coroutine _coroutine;

    private void View(float currentHealth)
    {
        StopCoroutine();
        _coroutine = StartCoroutine(SmoothTakeDamage(currentHealth));
    }

    private void OnEnable()
    {
        _player.HealthChanged += View;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= View;
    }

    private IEnumerator SmoothTakeDamage(float currentHealth)
    {
        float elapsedTime = 0;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _slider.value = Mathf.Lerp(_slider.value, currentHealth / _player.MaxHealth, elapsedTime / _duration);

            yield return null;
        }
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}