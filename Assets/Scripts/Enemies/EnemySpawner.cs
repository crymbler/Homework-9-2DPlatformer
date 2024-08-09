using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PoolObject _objectPool;
    [SerializeField] private float _spawnDelay;

    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);

        StartCoroutine(SpawnEnemy());
    }

    public void ReturnToPool(Enemy enemy) =>
        _objectPool.ReturnObject(enemy);

    private IEnumerator SpawnEnemy()
    {
        while (enabled)
        {
            if (_objectPool.TryGetObject(out Enemy enemy) == true)
            {
                enemy.Died += ReturnToPool;

                enemy.transform.position = transform.position;
            }

            yield return _delay;
        }
    }
}