using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponType CurrentWeapon;
    public GameObject[] Weapons;

    public int WindWeaponLevel = 0;
    public int FireWeaponLevel = 0;
    public int ArrowWeaponLevel = 0;
   
    public Transform SpawnPoint;
    private float _timer = 0;
    public float SpawnTime = 1;

    private void Start()
    {
        _timer = 0;
        SwitchWeapon(WeaponType.Wind);
        ActiveWeapon();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > SpawnTime)
        {
            ActiveWeapon();
            _timer = 0;
        }
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
                //Weapons[0].SetActive(true);
                Instantiate(Weapons[0], SpawnPoint).SetActive(true);
                Debug.Log("바람 무기");
                break;
            case WeaponType.Fire:
                //Weapons[1].SetActive(true);
                Instantiate(Weapons[1], SpawnPoint).SetActive(true);
                Debug.Log("불 무기");
                break;
            case WeaponType.Arrow:
                //Weapons[2].SetActive(true);
                Instantiate(Weapons[2], SpawnPoint).SetActive(true);
                Debug.Log("화살 무기");
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