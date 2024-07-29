using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Vector2 _triggerZone;

    private BoxCollider2D _boxCollider2D;

    public event Action Detected;
    public event Action<Player> Follow;
    public event Action Missed;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.transform.localScale = _triggerZone;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            Detected?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            Follow?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            Missed?.Invoke();
    }
}