using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float damage;
    public float speed;
    public float activeTime;
    private float activeTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        activeTimer += Time.deltaTime;
        
        if (activeTimer >= activeTime)
        {
            gameObject.SetActive(false);
        }
    }
    public void Init(float damage, float speed, float activeTime, Vector3 dir)
    {
        this.damage = damage;
        this.speed = speed;
        this.activeTime = activeTime;
        activeTimer = 0f;
        
        rb.velocity = dir * speed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
