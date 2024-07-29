using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnpoints;
    [SerializeField] private Coin _prefab;

    private Transform _currentSpawnpoint;
    Transform randomSpawnpoint;

    private void Start() =>
        Spawn();

    public void Spawn()
    {
        while (randomSpawnpoint == _currentSpawnpoint)
            randomSpawnpoint = _spawnpoints[Random.Range(0, _spawnpoints.Count)];

        _currentSpawnpoint = randomSpawnpoint;

        Coin coin = Instantiate(_prefab, randomSpawnpoint);
        coin.Collected += Spawn;
    }
}