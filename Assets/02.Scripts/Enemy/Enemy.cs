using System.Collections;
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
    public float Speed = 0.5f;
    public float RushSpeed = 0.7f;
    public int Damage = 5;
    public int EnemyScore = 100;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float KnockbackForce = 1f;
    public float KnockbackDuration = 1f;
    public float FlickerDuration = 0.1f;
    private bool _damaged = false;
    private Player _player;

    private Animator _animator;
    private bool _isPaused = false;
    private float _animationSpeed;
    public GameObject IceImage;

    private void Start()
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
            Speed = RushSpeed;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        if (_isPaused)
        {
            return;
        }
        transform.Translate(Vector2.left * Speed * Time.deltaTime);       
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
            Destroy(gameObject);
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
            Destroy(gameObject);
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
}
