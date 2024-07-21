using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnpoints;
    [SerializeField] private Coin _prefab;

    private Transform _currentSpawnpoint;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Transform randomSpawnpoint = GetSpawnpoint();

        while (randomSpawnpoint == _currentSpawnpoint)
        {
            randomSpawnpoint = GetSpawnpoint();
        }

        _currentSpawnpoint = randomSpawnpoint;

        Coin coin = Instantiate(_prefab, randomSpawnpoint);
        coin.Collected += Spawn;
    }

    public Transform GetSpawnpoint()
    {
        return _spawnpoints[Random.Range(0, _spawnpoints.Count)];
    }
}