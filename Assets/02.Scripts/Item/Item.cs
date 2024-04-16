using UnityEngine;

public enum ItemType
{
    Star,
    Ice,
    Power,
}

public class Item : MonoBehaviour
{
    public ItemType IType;
    public int StarScore = 300;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (IType == ItemType.Star)
            {
                other.GetComponent<Player>().Score += StarScore;
            }
            else if (IType == ItemType.Ice)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject e in enemies)
                {
                    Enemy enemy = e.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.IceItem();
                    }
                }
            }
            else if (IType == ItemType.Power)
            {
                other.GetComponent<PlayerWeapon>().PowerItem();
            }
        }
    }
}