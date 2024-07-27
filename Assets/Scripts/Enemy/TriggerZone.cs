using System;
using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Vector2 _triggerZone;

    private CircleCollider2D _circleCollider2D;

    public Action Detected;

    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.transform.localScale = _triggerZone;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            Detected?.Invoke();
    }
}