using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLouder : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Dead += Reload;
    }

    private void OnDisable()
    {
        _player.Dead -= Reload;

    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
