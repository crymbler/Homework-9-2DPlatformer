using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable() =>
        _player.Dead += Reload;

    private void OnDisable() =>
        _player.Dead -= Reload;

    public void Reload() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
