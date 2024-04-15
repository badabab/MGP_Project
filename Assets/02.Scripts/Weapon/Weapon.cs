using UnityEngine;

public enum WeaponType
{
    Wind,
    Fire,
    Arrow
}

public class Weapon : MonoBehaviour
{
    public WeaponType Wtype;
    public int Damage = 5;

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
