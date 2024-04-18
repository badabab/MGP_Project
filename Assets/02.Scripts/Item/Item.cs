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
    public int StarScore = 15;
    public int ItemScore = 5;

    private void Update()
    {
        transform.Translate(Vector3.down * 2f * Time.deltaTime);
    }

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
                other.GetComponent<Player>().Score += ItemScore;
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
                other.GetComponent<Player>().Score += ItemScore;
                other.GetComponent<PlayerWeapon>().PowerItem();
            }
            this.gameObject.SetActive(false);
        }       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            this.gameObject.SetActive(false);
        }
    }
}