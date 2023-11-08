using UnityEngine;

public class PlayerNormalAttackColliderHandler : MonoBehaviour
{
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public void Active()
    {
        collider.enabled = true;
        
        Invoke(nameof(DeActive), 0.5f);
    }
    
    private void DeActive()
    {
        collider.enabled = false;
    }
}
