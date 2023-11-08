using UnityEngine;

public class EnemyAttack2 : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Enemy enemy;
    
    private Vector2 dir;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void Active()
    {
        dir = playerRB.position - (Vector2)transform.position;
        dir = dir.normalized;

        var enemyBall = GameManager.I.poolManager.GetZombieBall2().transform;
            enemyBall.position = transform.position;
            enemyBall.GetComponent<EnemyBall>().Init(enemy.damage, dir);
    }
    
    private void OnEnable()
    {
        playerRB = GameManager.I.playerHandler.GetComponent<Rigidbody2D>();
    }
}
