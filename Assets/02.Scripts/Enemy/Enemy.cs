using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Normal, Rush, Fly, Drop,
}
public class Enemy : MonoBehaviour
{
    public EnemyType EnemyType;
    public Slider HP_Slider;
    public int HP;
    public int MaxHP = 10;
    public float Speed = 0.3f;
    public float RushSpeed = 0.5f;
    public int Damage = 5;

    private void Start()
    {
        HP = MaxHP;
        Refresh();
        if (EnemyType == EnemyType.Rush)
        {
            Speed = RushSpeed;
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);       
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            player.Damaged(Damage);
        }
    }

    private void Refresh()
    {
        HP_Slider.value = HP / (float)MaxHP;
    }

    public void Damaged(int damage)
    {
        HP -= damage;
        Refresh();
    }
}
