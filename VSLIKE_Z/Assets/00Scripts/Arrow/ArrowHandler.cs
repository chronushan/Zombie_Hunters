using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    public float damage;
    public float reloadTime = -1;
    private float reloadTimer = 3.5f;

    private Vector3 dir;

    private Transform arrow;
    private void Update()
    {
        if (reloadTime == -1)
            return;
        
        reloadTimer += Time.deltaTime;

        if (reloadTimer > reloadTime)
        {
            reloadTimer = 0f;

            Shoot();
        }
    }
    
    public void Init(float damage, float reloadTime)
    {
        this.damage = damage;
        this.reloadTime = reloadTime;
        reloadTimer = this.reloadTime - 0.5f;
    }

    void Shoot()
    {
        // random direction
        dir = Random.insideUnitCircle.normalized;
        
        arrow = GameManager.I.poolManager.GetArrow().transform;
        arrow.position = transform.position;
        arrow.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        arrow.GetComponent<Arrow>().Init(damage, 5f, 3f, dir);
        
        SoundManager.I.PlayArrow();
    }
}
