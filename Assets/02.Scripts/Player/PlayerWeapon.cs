using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponType CurrentWeapon;
    public GameObject[] Weapons;
    public int PoolSize = 7;
    private List<GameObject>[] _weaponPool;

    public int CurrentWeaponLevel = 0;
    public int WindWeaponLevel = 0;
    public int FireWeaponLevel = 0;
    public int ArrowWeaponLevel = 0;

    public int CurrentWeaponDamage = 0;
    public int WindWeaponDamage = 5;
    public int FireWeaponDamage = 5;
    public int ArrowWeaponDamage = 5;

    public Transform SpawnPoint;
    private float _timer = 0;
    public float SpawnTime = 0.5f;

    public GameObject PowerImage;

    private void Awake()
    {
        _weaponPool = new List<GameObject>[Weapons.Length];
        for (int i = 0; i < Weapons.Length; i++)
        {
            _weaponPool[i] = new List<GameObject> ();
            for (int j = 0; j < PoolSize; j++)
            {
                GameObject weaponObject = Instantiate(Weapons[i], SpawnPoint);
                weaponObject.SetActive(false);
                _weaponPool[i].Add(weaponObject);
            }
        }
        PowerImage.SetActive(false);
    }
    private void Start()
    {
        _timer = 0;
        SwitchWeapon(WeaponType.Wind);
        ActiveWeapon((int)CurrentWeapon);
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        _timer += Time.deltaTime;

        if (_timer > SpawnTime)
        {
            ActiveWeapon((int)CurrentWeapon);
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
            CurrentWeaponLevel = WindWeaponLevel;
        }
        else if (newWeapon == WeaponType.Fire)
        {
            FireWeaponLevel += 1;
            CurrentWeaponLevel = FireWeaponLevel;
        }
        else if (newWeapon == WeaponType.Arrow)
        {
            ArrowWeaponLevel += 1;
            CurrentWeaponLevel = ArrowWeaponLevel;
        }
        CurrentWeaponDamage = (CurrentWeaponLevel - 1) * 2 + 5;
        CurrentWeapon = newWeapon;
    }

    private void ActiveWeapon(int index)
    {
        foreach (GameObject weapon in _weaponPool[index])
        {
            if (!weapon.activeInHierarchy)
            {
                weapon.transform.position = SpawnPoint.position;
                weapon.SetActive(true);
                break;
            }
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
        ActiveWeapon((int)CurrentWeapon);
    }

    public void PowerItem()
    {
        StartCoroutine(Power_Coroutine());
    }
    private IEnumerator Power_Coroutine()
    {
        CurrentWeaponDamage += 10;
        PowerImage.SetActive(true);
        yield return new WaitForSeconds(5);
        CurrentWeaponDamage -= 10;
        PowerImage.SetActive(false);
    }
}