using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteRenderers;
    [SerializeField] private SpriteRenderer _currentBackground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
            _currentBackground.sprite = _spriteRenderers[Random.Range(0, _spriteRenderers.Length)];
    }
}