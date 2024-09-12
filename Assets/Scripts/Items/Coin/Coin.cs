using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;

    private AudioSource _audioSource;

    public event Action Collected;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public int Collect()
    {
        Collected?.Invoke();
        
        _audioSource.Play();

        Destroy(gameObject);

        return _value;
    }
}