using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float initSpeed;
    [SerializeField] private float initHealth;
    [SerializeField] private float initDamage;
    [SerializeField] private float speedCoefficient;
    [SerializeField] private float healthCoefficient;
    [SerializeField] private float damageCoefficient;

    [SerializeField] private float exp;
    
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public float damage;
    private int currentLevel;
    
    private Rigidbody2D playerRB;
    [SerializeField] private bool isLive;
    
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    // 원거리 몬스터
    [SerializeField] private bool isEnemyBall;
    [SerializeField] private float attackTime = 5f;
    private float attackTimer;
    
    private Vector2 direction;
    private Vector2 moveDirection;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;
        
        direction = playerRB.position - rb.position;
        moveDirection = direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
        rb.velocity = Vector2.zero;


        if (!isEnemyBall)
            return;
        
        attackTimer += Time.fixedDeltaTime;
        
        if (attackTimer > attackTime)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0;
        }
    }
    
    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriteRenderer.flipX = playerRB.position.x < rb.position.x;
        
    }

    private void OnEnable()
    {
        playerRB = GameManager.I.playerHandler.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
        collider.enabled = true;
    }

    public void Init(int level)
    {
        speed = initSpeed + speedCoefficient * level;
        health = initHealth + healthCoefficient * level;
        maxHealth = initHealth + healthCoefficient * level;
        damage = initDamage + damageCoefficient * level;
        currentLevel = level;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Bullet") && !col.CompareTag("Fire")  && !col.CompareTag("Arrow") && !col.CompareTag("Slash"))
            return;

        if (col.CompareTag("Bullet"))
        {
            var bullet = col.GetComponent<Bullet>();
            health -= bullet.damage;
            bullet.gameObject.SetActive(false);
            
        }
        else if (col.CompareTag("Arrow"))
        {
            var arrow = col.GetComponent<Arrow>();
            health -= arrow.damage;
        }
        else if (col.CompareTag("Slash"))
        {
            health -= col.transform.parent.parent.parent.GetComponent<PlayerHandler>().slashDamage;
        }
        else if (col.CompareTag("Fire"))
        {
            health -= col.transform.parent.parent.parent.GetComponent<PlayerHandler>().fireDamage;
        }
        
        SoundManager.I.PlayHit();
        
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            Hit();
        }
    }

    private void Hit()
    {
        spriteRenderer.DOColor(Color.white, 0.3f).From(Color.HSVToRGB(1, 0.6f, 0.6f)).SetEase(Ease.Linear);
    }

    private void Dead()
    {
        GameManager.I.expHandler.AddExp((currentLevel + 1) * exp);
        
        isLive = false;
        collider.enabled = false;
        
        rb.velocity = Vector2.zero;
        
        animator.SetTrigger("Death");
        
        Invoke(nameof(DeActive), 1f);
    }

    public void FinalDead()
    {
        isLive = false;
        collider.enabled = false;
        
        rb.velocity = Vector2.zero;
        
        animator.SetTrigger("Death");
        
        Invoke(nameof(DeActive), 1f);
    }
    
    private void DeActive()
    {
        gameObject.SetActive(false);
    }
}
