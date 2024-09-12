using UnityEngine;

public class PullDownTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            _sceneLoader.BackToMenu();

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            Destroy(enemy);
    }
}