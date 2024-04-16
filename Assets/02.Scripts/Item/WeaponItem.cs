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

    private void Start()
    {
        
    }

    private IEnumerator Drop_Coroutine()
    {
        yield return new WaitForSeconds(3);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
