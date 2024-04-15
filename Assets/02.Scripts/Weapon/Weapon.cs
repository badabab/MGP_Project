using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage = 10;
    //private WeaponType wType;

    private void Start()
    {
        PlayerWeapon weapon = GetComponent<PlayerWeapon>();
        //wType = weapon.CurrentWeapon;
    }
    private void Update()
    {
        /*if (wType == WeaponType.Arrow)
        {

        }*/
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damaged(Damage);
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().Damaged(Damage);
        }
    }
}
