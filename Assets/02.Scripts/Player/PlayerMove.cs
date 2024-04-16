using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool LookingRight = true;
    public float Speed = 0.5f;
    public float MinX = -2f;
    public float MaxX = 0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public float KnockbackForce = 1f;
    public float KnockbackDuration = 1f;
    public float FlickerDuration = 0.1f;

    private bool _damaged = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerTurn();
        }

        if (!LookingRight)
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        
        if (transform.position.x < MinX)
        {
            transform.position = new Vector2 (MinX, transform.position.y);
        }
        if (transform.position.x > MaxX)
        {
            transform.position = new Vector2(MaxX, transform.position.y);
        }
    }

    private void PlayerTurn()
    {
        LookingRight = !LookingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void Damaged(int damage)
    {
        Player player = GetComponent<Player>();
        if (player.HP <= 0 || _damaged)
        {
            return;
        }
        _damaged = true;
        player.HP -= damage;
        StartCoroutine(Damaged_Coroutine());
    }
    private IEnumerator Damaged_Coroutine()
    {
        _animator.Play("Idle");
        if (LookingRight)
        {
            _rb.AddForce(-transform.right * KnockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(transform.right * KnockbackForce, ForceMode2D.Impulse);
        }

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
        _animator.Play("Walk");
        _rb.velocity = Vector2.zero;
        _damaged = false;
    }
}
