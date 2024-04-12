using UnityEngine;
using UnityEngine.UI;

public enum BossType
{
    Jump,
}
public class Boss : MonoBehaviour
{
    public BossType BossType;
    public Slider HP_Slider;
    public int HP;
    public int MaxHP = 30;

    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    public Transform playerTransform;
    public float followDistance = 10f;
    private Rigidbody2D rb;
    private bool _isGround = true;
    public int Damage = 10;

    void Start()
    {
        HP = MaxHP;
        Refresh();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= followDistance && _isGround)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }

        if (_isGround)
        {
            Jump();
        }
    }

    private void Refresh()
    {
        HP_Slider.value = HP / (float)MaxHP;
    }

    private void Jump()
    {
        if (_isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _isGround = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            player.Damaged(Damage);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }
}
