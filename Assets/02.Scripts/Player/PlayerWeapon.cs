using UnityEngine;

public enum WeaponType
{
    Wind,
    Fire,
    Arrow
}

public class PlayerWeapon : MonoBehaviour
{
    public WeaponType CurrentWeapon;
    public GameObject[] Weapons;

    private void Start()
    {
        SwitchWeapon(WeaponType.Wind);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(WeaponType.Wind);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(WeaponType.Fire);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(WeaponType.Arrow);
        }
        ActivateWeapon();
    }

    private void SwitchWeapon(WeaponType newWeapon)
    {
        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false);
        }
        CurrentWeapon = newWeapon;
    }

    private void ActivateWeapon()
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
}