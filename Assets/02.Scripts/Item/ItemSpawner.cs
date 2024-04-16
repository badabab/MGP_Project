using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject StarItem;
    public GameObject IceItem;
    public GameObject PowerItem;

    public float MinTime = 2f;
    public float MaxTime = 10f;
    private float _spawnTime = 5;
    private float _timer = 0;

    public int PoolSize = 5;
    private List<Item> _itemPool;

    private void Awake()
    {
        _itemPool = new List<Item>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(StarItem);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(IceItem);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject itemObject = Instantiate(PowerItem);
            itemObject.SetActive(false);
            _itemPool.Add(itemObject.GetComponent<Item>());
        }
    }
    private void Update()
    {       
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
                item.transform.position = this.transform.position;
                item.gameObject.SetActive(true);
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
                item.transform.position = this.transform.position;
                item.gameObject.SetActive(true);
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
                item.transform.position = this.transform.position;
                item.gameObject.SetActive(true);              
            }
            _timer = 0;
            _spawnTime = Random.Range(MinTime, MaxTime);
            Debug.Log($"아이템 스폰{item.IType} / {_spawnTime}");
        }
    }
}