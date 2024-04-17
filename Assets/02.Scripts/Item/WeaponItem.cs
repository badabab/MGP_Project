using System.Collections;
using UnityEngine;

public enum WeaponItemType
{
    WindWeapon,
    FireWeapon,
    ArrowWeapon
}
public class WeaponItem : MonoBehaviour
{
    public WeaponItemType type;
    private bool _drop = false;

    private void Start()
    {
        StartCoroutine(Drop_Coroutine());
    }
    private void Update()
    {
        if (!_drop)
        {
            return;
        }
        transform.Translate(Vector3.down * 1f * Time.deltaTime);
    }
    private IEnumerator Drop_Coroutine()
    {
        yield return new WaitForSeconds(3);
        _drop = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyWeaponItem();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            _drop = false;
        }
    }
    
    private void DestroyWeaponItem()
    {
        WeaponItem[] weaponItems = FindObjectsOfType<WeaponItem>();
        foreach (WeaponItem w in weaponItems)
        {
            Destroy(w.gameObject);
        }
    }
}
