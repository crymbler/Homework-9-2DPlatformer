using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float _heal;

    public float Heal()
    {
        Destroy(gameObject);

        return _heal;
    }
}