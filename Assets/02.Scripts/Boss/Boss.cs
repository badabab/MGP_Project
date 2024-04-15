using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BossType
{
    WalkJump, Jump, Teleport, LeftRight
}
public class Boss : MonoBehaviour
{
    public BossType BossType;
    public Slider HP_Slider;
    public int HP;
    public int MaxHP = 30;
    private Rigidbody2D _rb;
    private bool _isGround = true;
    public int Damage = 10;

    public float jumpForce = 7f;
    public float moveSpeed = 2f;
    private Transform _playerTransform;
    public float followDistance = 10f;

    public float WalkJumpTime = 3;
    private float _timer = 0;

    private SpriteRenderer _spriteRenderer;
    public float KnockbackForce = 1f;
    public float KnockbackDuration = 1f;
    public float FlickerDuration = 0.1f;
    private bool _damaged = false;

    private Player _player;

    void Start()
    {
        HP = MaxHP;
        Refresh();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerTransform = GameObject.Find("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (_isGround)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        switch(BossType)
        {
            case BossType.WalkJump:
                WalkJumpType(); break;
            case BossType.Jump:
                JumpType(); break;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void WalkJumpType()
    {
        _timer += Time.deltaTime;
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= followDistance && _isGround)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.velocity = new Vector2(direction.x * moveSpeed, _rb.velocity.y);
        }

        if (_isGround)
        {
            if (_timer > WalkJumpTime)
            {
                Jump();
                _timer = 0;
            }
        }
    }
    private void JumpType()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= followDistance && _isGround)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.velocity = new Vector2(direction.x * moveSpeed, _rb.velocity.y);
        }

        if (_isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_isGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
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
    private void Refresh()
    {
        HP_Slider.value = HP / (float)MaxHP;
    }
    public void Damaged(int damage)
    {
        if (_damaged)
        {
            return;
        }
        _damaged = true;
        HP -= damage;
        Refresh();
        StartCoroutine(Damaged_Coroutine());
    }
    private IEnumerator Damaged_Coroutine()
    {
        _rb.AddForce(transform.right * KnockbackForce, ForceMode2D.Impulse);

        float elapsedTime = 0f;
        while (elapsedTime < KnockbackDuration)
        {
            // Alpha 값을 빠르게 반복하도록 변경
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
            yield return new WaitForSeconds(FlickerDuration);

            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.4f);
            yield return new WaitForSeconds(FlickerDuration);

            elapsedTime += FlickerDuration * 2f;
        }

        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
        _rb.velocity = Vector2.zero;
        _damaged = false;
        if (HP <= 0)
        {
            _player.GetComponent<Player>().XP += 5;
            Death();
        }
    }
}
