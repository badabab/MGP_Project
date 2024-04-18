using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;

    public int PoolSize = 7;
    private List<Enemy> _enemyPool;

    public float MinX = 2;
    public float EnemySpace = 0.6f;
    private float _currentX = 0;

    private void Awake()
    {
        _currentX = MinX;
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
        ShuffleEnemyPool();
    }
    private void ShuffleEnemyPool()
    {
        // 랜덤으로 섞기 위해 Fisher-Yates 셔플 알고리즘을 사용합니다.
        int n = _enemyPool.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Enemy value = _enemyPool[k];
            _enemyPool[k] = _enemyPool[n];
            _enemyPool[n] = value;
        }
    }

    public void SpawnEnemies()
    {        
        foreach (Enemy enemy in _enemyPool)
        {
            enemy.MaxHP += (FindAnyObjectByType<Player>().Level - 1) * 3;
            Vector3 spawnPosition = new Vector3(_currentX, -0.5f, 0);
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
            _currentX += EnemySpace; ;
        }
    }
    public void DestroyEnemies()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            enemy.gameObject.SetActive(false);
        }
        _currentX = MinX;
        ShuffleEnemyPool();
    }
}
