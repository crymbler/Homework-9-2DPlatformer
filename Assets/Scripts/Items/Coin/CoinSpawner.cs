using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnpoints;
    [SerializeField] private Coin _prefab;

    private Transform _currentSpawnpoint;
    private Transform _randomSpawnpoint;

    private void Start() =>
        Spawn();

    public void Spawn()
    {
        while (_randomSpawnpoint == _currentSpawnpoint)
            _randomSpawnpoint = _spawnpoints[Random.Range(0, _spawnpoints.Count)];

        _currentSpawnpoint = _randomSpawnpoint;

        Coin coin = Instantiate(_prefab, _randomSpawnpoint);
        coin.Collected += Spawn;
    }
}