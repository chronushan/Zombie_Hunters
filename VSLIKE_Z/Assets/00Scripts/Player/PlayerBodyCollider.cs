using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour
{
    private PlayerHandler playerHandler;
    [SerializeField] private GameOverHandler gameOverHandler;
    
    [SerializeField] private float deActiveTime = 0.5f;
    private float deActiveTimer;
    private bool isHit;
        

    private bool isDead;
    
    private void Awake()
    {
        playerHandler = GetComponentInParent<PlayerHandler>();
        
        isDead = false;
    }

    private void Update()
    {
        if (!isHit)
            return;
        
        deActiveTimer += Time.deltaTime;

        if (deActiveTimer >= deActiveTime)
        {
            isHit = false;
            deActiveTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy") && !col.CompareTag("Ball"))
            return;

        if (col.CompareTag("Enemy"))
        {
            if (!isHit)
            {
                isHit = true;
                playerHandler.health -= col.GetComponent<Enemy>().damage;
            }
        }
        else if (col.CompareTag("Ball"))
        {
            var enemyBall = col.GetComponent<EnemyBall>();
            
            if (!isHit)
            {
                isHit = true;

                playerHandler.health -= enemyBall.damage;
            }
            
            enemyBall.gameObject.SetActive(false);
        }
        
        if (isDead)
            return;

        if (playerHandler.health <= 0)
        {
            isDead = true;
            gameOverHandler.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.transform.CompareTag("Enemy") && !col.transform.CompareTag("Ball"))
            return;

        if (col.transform.CompareTag("Enemy"))
        {
            playerHandler.health -= col.transform.GetComponent<Enemy>().damage;
        }
        else if (col.transform.CompareTag("Ball"))
        {
            playerHandler.health -= col.transform.GetComponent<EnemyBall>().damage;
        }
        
        if (isDead)
            return;
        
        if (playerHandler.health <= 0)
        {
            isDead = true;
            gameOverHandler.GameOver();
        }
    }
    
    public bool IsDead()
    {
        return isDead;
    }
}
