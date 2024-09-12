using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _sceneNumber;

    private void OnEnable() =>
        _player.Dead += BackToMenu;

    private void OnDisable() =>
        _player.Dead -= BackToMenu;

    public void LoadGame(int scene) =>
        SceneManager.LoadScene(scene);

    public void BackToMenu() =>
        SceneManager.LoadScene(_sceneNumber);

    public void Quit() =>
        Application.Quit();
}