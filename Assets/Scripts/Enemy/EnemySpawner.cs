using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawnerReceiver
{
    [SerializeField] private PoolObject _objectPool;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (enabled)
        {
            if (_objectPool.TryGetObject(out Enemy enemy) == true)
            {
                enemy.Initialize(this);
                enemy.SetHealth();

                enemy.transform.position = transform.position;
            }

            yield return new WaitForSeconds(2);
        }
    }

    public void ReturnToPool(Enemy enemy)
    {
        _objectPool.ReturnObject(enemy);
    }
}