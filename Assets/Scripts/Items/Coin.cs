using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;

    public Action Collected;

    public int Collect()
    {
        Collected?.Invoke();

        Destroy(gameObject);

        return _value;
    }
}