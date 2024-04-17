using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject StarItem;
    public GameObject IceItem;
    public GameObject PowerItem;

    public float MinTime = 2f;
    public float MaxTime = 7f;
    private float _spawnTime = 5;
    private float _timer = 0;

    public int PoolSize = 5;
    private List<Item> _itemPool;

    private void Awake()
    {
        _itemPool = new List<Item>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(StarItem, transform);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(IceItem, transform);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(PowerItem, transform);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= _spawnTime)
        {
            int probability = Random.Range(0, 10);
            Item item = null;
            if (probability < 6)
            {
                foreach (Item i in _itemPool)
                {
                    if (!i.gameObject.activeInHierarchy && i.IType == ItemType.Star)
                    {
                        item = i; break;
                    }
                }
            }
            else if (probability>= 6 && probability < 8)
            {
                foreach (Item i in _itemPool)
                {
                    if (!i.gameObject.activeInHierarchy && i.IType == ItemType.Ice)
                    {
                        item = i; break;
                    }
                }
            }
            else
            {
                foreach (Item i in _itemPool)
                {
                    if (!i.gameObject.activeInHierarchy && i.IType == ItemType.Power)
                    {
                        item = i; break;
                    }
                }                        
            }
            float randomX = Random.Range(-2f, 2f);
            item.transform.position = new Vector2(randomX, 6);
            item.gameObject.SetActive(true);
            _timer = 0;
            _spawnTime = Random.Range(MinTime, MaxTime);
        }
    }
}