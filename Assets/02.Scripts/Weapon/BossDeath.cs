using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : MonoBehaviour
{
    public GameObject[] WeaponItems;
    public GameObject StarItem;
    public int PoolSize = 20;
    private List<Item> _starPool;

    private void Awake()
    {
        _starPool = new List<Item>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject star = Instantiate(StarItem, transform);
            star.SetActive(false);
            _starPool.Add(star.GetComponent<Item>());
        }
    }

    public void CreateStar()
    {
        foreach (Item i in _starPool)
        {
            float randomX = Random.Range(-2f, 2f);
            float randomY = Random.Range(6f, 12f);
            i.transform.position = new Vector2(randomX, randomY);
            i.gameObject.SetActive(true);
        }
    }
    public void CreateWeaponItem()
    {
        List<GameObject> availableItems = new List<GameObject>(WeaponItems);
        int randomIndex = Random.Range(0, availableItems.Count);
        GameObject excludedItem = availableItems[randomIndex];
        availableItems.RemoveAt(randomIndex);

        for (int i = 0; i < 2; i++)
        {
            int randomIndex2 = Random.Range(0, availableItems.Count);
            GameObject selectedWeapon = availableItems[randomIndex2];

            float xPos = (i == 0) ? -1.5f : 1.5f; // 첫 번째 루프에서는 -1.5, 두 번째 루프에서는 1.5로 xPos 설정
            Vector2 spawnPosition = new Vector2(xPos, 3);
            Instantiate(selectedWeapon, spawnPosition, Quaternion.identity);

            availableItems.RemoveAt(randomIndex2);
        }
        StartCoroutine(NextStage_Coroutine());
    }
    private IEnumerator NextStage_Coroutine()
    {
        yield return new WaitForSeconds(10);
        StageManager.instance.NextStage();
    }
}