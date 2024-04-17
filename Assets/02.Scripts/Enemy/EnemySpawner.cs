using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;

    public int PoolSize = 7;
    private List<Enemy> _enemyPool;

    public float MinX = 2;
    public float MaxX = 40;

    private void Awake()
    {
        _enemyPool = new List<Enemy>();
        for (int i = 0; i < Enemy.Length; i++)
        {
            for (int j = 0;  j < PoolSize; j++)
            {
                GameObject enemyObject = Instantiate(Enemy[i], transform);
                enemyObject.SetActive(false);
                _enemyPool.Add(enemyObject.GetComponent<Enemy>());
            }
        }
    }

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            float randomX = Random.Range(MinX, MaxX);
            Vector3 spawnPosition = new Vector3(randomX, -0.5f, 0);
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
        }
    }
    public void DestroyEnemies()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
