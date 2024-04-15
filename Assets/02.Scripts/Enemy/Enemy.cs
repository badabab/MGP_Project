using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float KnockbackForce = 1f;
    public float KnockbackDuration = 1f;
    public float FlickerDuration = 0.1f;
    private bool _damaged = false;
    private Player _player;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            _player.GetComponent<Player>().XP += 1;
            Destroy(gameObject);
        }
    }
}
