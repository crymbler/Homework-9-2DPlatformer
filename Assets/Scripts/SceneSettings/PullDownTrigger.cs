using UnityEngine;

public class PullDownTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            player.TakeDamage(player.MaxHealth);

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(enemy.MaxHealth);
    }
}