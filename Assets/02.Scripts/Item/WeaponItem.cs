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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
