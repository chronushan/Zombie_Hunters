using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private HPBarHandler hpBarHandler;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform fireColliderParent;
    [SerializeField] private PauseHandler pauseHandler;
    
    private Rigidbody2D rb;

    private Vector2 input;
    private Vector2 direction;
    
    [SerializeField] private float moveSpeed = 5f;
    
    public float fireReloadTime = 5f;
    private float fireReloadTimer;
    
    public float fireDamage;
    public float slashDamage;
    
    public float health;
    public float maxHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pauseHandler.isPause)
            return;

        if (GameManager.I.upgradeHandler.IsShowing())
            return;    
        
        hpBarHandler.SetHPBar(health, maxHealth);
    }

    private void FixedUpdate()
    {
        if (pauseHandler.isPause)
            return;

        if (GameManager.I.upgradeHandler.IsShowing())
            return;
        
        direction = input * moveSpeed * Time.fixedDeltaTime;
        
        rb.MovePosition(rb.position + direction);

        fireReloadTimer += Time.fixedDeltaTime;
        
        if (fireReloadTimer > fireReloadTime)
        {
            anim.SetTrigger("Attack");
            
            SoundManager.I.PlayFire();
            fireReloadTimer = 0;
        }
    }

    private void LateUpdate()
    {
        if (pauseHandler.isPause)
            return;

        if (GameManager.I.upgradeHandler.IsShowing())
            return;
        
        anim.SetFloat("Speed", input.magnitude);
        
        if (input.x != 0)
        {
            sr.flipX = input.x < 0;
            fireColliderParent.localScale = new Vector3(sr.flipX ? -1 : 1, 1, 1);
        }
    }

    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }
    
    public Vector2 GetInput()
    {
        return input;
    }
    
    public void PlusHealth(float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void PlusHealth()
    {
        maxHealth += 10;
        
        health = maxHealth;
    }
}
