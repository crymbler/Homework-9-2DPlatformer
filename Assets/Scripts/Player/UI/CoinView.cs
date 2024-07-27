using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start() =>
        _text.text = "0";

    private void OnEnable() =>
        _player.Taken += View;

    private void OnDisable() =>
        _player.Taken -= View;

    private void View(int currentValue) =>
        _text.text = currentValue.ToString();
}