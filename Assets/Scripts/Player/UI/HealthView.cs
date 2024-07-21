using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour, IHealthReceive
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _duration;

    Coroutine _coroutine;

    private void Start()
    {
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.MaxHealth;
        _player.Initialize(this);
    }

    private IEnumerator SmoothTakeDamage(float currentHealth)
    {
        float elapsedTime = 0;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _slider.value = Mathf.Lerp(_slider.value, currentHealth, elapsedTime / _duration);

            yield return null;
        }

        StopCoroutine();
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void View(float currentHealth)
    {
        _coroutine = StartCoroutine(SmoothTakeDamage(currentHealth));
    }

}