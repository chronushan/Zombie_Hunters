using System;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public float damage;

    [SerializeField] private float speed = 5;
    [SerializeField] private float activeTime = 5;
    private float activeTimer;
    
    private Rigidbody2D rb;

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

    public void Init(float damage, Vector2 dir)
    {
        this.damage = damage;

        rb.velocity = dir * speed;
    }
    
    private void OnDisable()
    {
        activeTimer = 0;
        rb.velocity = Vector2.zero;
    }

    public void Final()
    {
        gameObject.SetActive(false);
    }
}
