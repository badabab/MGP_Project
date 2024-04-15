using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponType CurrentWeapon;
    public GameObject[] Weapons;

    public int WindWeaponLevel = 0;
    public int FireWeaponLevel = 0;
    public int ArrowWeaponLevel = 0;

    private void Start()
    {
        SwitchWeapon(WeaponType.Wind);
        ActiveWeapon();
    }

    private void SwitchWeapon(WeaponType newWeapon)
    {
        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false);
        }
        if (newWeapon == WeaponType.Wind)
        {
            WindWeaponLevel += 1;
        }
        else if (newWeapon == WeaponType.Fire)
        {
            FireWeaponLevel += 1;
        }
        else if (newWeapon == WeaponType.Arrow)
        {
            ArrowWeaponLevel += 1;
        }
        CurrentWeapon = newWeapon;
    }

    private void ActiveWeapon()
    {
        switch (CurrentWeapon)
        {
            case WeaponType.Wind:
                Weapons[0].SetActive(true);
                break;
            case WeaponType.Fire:
                Weapons[1].SetActive(true);
                break;
            case WeaponType.Arrow:
                Weapons[2].SetActive(true);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WeaponItem"))
        {
            WeaponItem weaponItem = other.gameObject.GetComponent<WeaponItem>();
            if (weaponItem.type == WeaponItemType.WindWeapon)
            {
                SwitchWeapon(WeaponType.Wind);
            }
            else if (weaponItem.type == WeaponItemType.FireWeapon)
            {
                SwitchWeapon(WeaponType.Fire);
            }
            else if (weaponItem.type == WeaponItemType.ArrowWeapon)
            {
                SwitchWeapon(WeaponType.Arrow);
            }
        }       
        ActiveWeapon();
    }
}