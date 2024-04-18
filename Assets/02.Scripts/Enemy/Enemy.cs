using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Normal, Rush, Jump, Drop,
}
public class Enemy : MonoBehaviour
{
    public EnemyType EnemyType;
    public Slider HP_Slider;
    public int HP;
    public int MaxHP = 10;
    public float Speed = 0.5f;
    public float MinSpeed = 0.3f;
    public float MaxSpeed = 0.6f;  
    public float MinRushSpeed = 0.7f;
    public float MaxRushSpeed = 1f;
    private float _rushSpeed = 0.8f;
    public int Damage = 7;
    public int EnemyScore = 10;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float KnockbackForce = 1f;
    public float KnockbackDuration = 1f;
    public float FlickerDuration = 0.1f;
    private bool _damaged = false;
    private Player _player;
    private float _attackTimer = 0;

    private Animator _animator;
    private bool _isPaused = false;
    private float _animationSpeed;
    public GameObject IceImage;
    private bool _isGround = true;
    public float jumpForce = 7f;

    private void Start()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        if (_isPaused)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }
        //transform.Translate(Vector2.left * Speed * Time.deltaTime);      
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = new Vector2(direction.x * Speed, _rb.velocity.y);
        if (EnemyType == EnemyType.Jump)
        {
            JumpType();
        }
    }

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        IceImage.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _animationSpeed = _animator.speed;
        HP = MaxHP;
        Refresh();
        if (EnemyType == EnemyType.Rush)
        {
            _rushSpeed = Random.Range(MinRushSpeed, MaxRushSpeed);
            Speed = _rushSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_damaged)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            player.Damaged(Damage);
        }
        if (other.CompareTag("Weapon"))
        {
            PlayerWeapon player = _player.GetComponent<PlayerWeapon>();
            Damaged(player.CurrentWeaponDamage);
        }
        if (other.CompareTag("Ground"))
        {
            if (EnemyType == EnemyType.Jump)
            {
                _isGround = true;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_damaged)
            {
                return;
            }
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= 2)
            {
                PlayerMove player = other.GetComponent<PlayerMove>();
                player.Damaged(Damage);
                _attackTimer = 0;
            }          
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
            _spriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(FlickerDuration);

            _spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(FlickerDuration);

            elapsedTime += FlickerDuration * 2f;
        }

        _spriteRenderer.color = new Color(1, 1, 1, 1f);
        _rb.velocity = Vector2.zero;
        _damaged = false;
        if (HP <= 0)
        {
            _player.GetComponent<Player>().XP += 1;
            _player.GetComponent<Player>().Score += EnemyScore;
            gameObject.SetActive(false);
        }
    }

    public void IceItem()
    {
        StartCoroutine(Ice_Coroutine());
    }
    private IEnumerator Ice_Coroutine()
    {
        _isPaused = true;
        IceImage.SetActive(true);
        _animator.speed = 0;
        yield return new WaitForSeconds(5);
        _isPaused = false;
        IceImage.SetActive(false);
        _animator.speed = _animationSpeed;
    }

    private void JumpType()
    {
        if (_isGround)
        {
            if (_isGround)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                _isGround = false;
            }
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
