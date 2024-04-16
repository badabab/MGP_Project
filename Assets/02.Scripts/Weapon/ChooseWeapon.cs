using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public GameObject[] WeaponItems;

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

            Vector2 spawnPosition = new Vector2(i * 2 - 1, 3);
            Instantiate(selectedWeapon, spawnPosition, Quaternion.identity);

            availableItems.RemoveAt(randomIndex2);
        }
    }
}