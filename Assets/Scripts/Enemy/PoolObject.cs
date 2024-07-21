using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _enemyCount;

    private Queue<Enemy> _pool;
    private Enemy _instanceObject;

    private void Awake()
    {
        _pool = new Queue<Enemy>();

        while (_pool.Count < _enemyCount)
        {
            _instanceObject = CreateObject(_prefab);
            _instanceObject.gameObject.SetActive(false);

            _pool.Enqueue(_instanceObject);
        }
    }

    private Enemy GetObject()
    {
        Enemy takenObject = _pool.Dequeue();
        takenObject.gameObject.SetActive(true);

        return takenObject;
    }

    private Enemy CreateObject(Enemy prefab)
    {
        return Instantiate(prefab);
    }

    public bool TryGetObject(out Enemy enemy)
    {
        if (_pool.Count > 0)
        {
            enemy = GetObject();
        }
        else
        {
            enemy = null;
        }

        return enemy != null;
    }

    public void ReturnObject(Enemy returnedEnemy)
    {
        returnedEnemy.gameObject.SetActive(false);
        _pool.Enqueue(returnedEnemy);
    }
}