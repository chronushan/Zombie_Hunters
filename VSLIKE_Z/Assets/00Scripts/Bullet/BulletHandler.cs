using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float damage;
    public float reloadTime = -1;
    private float reloadTimer = 3f;

    private Scanner scanner;

    private Vector3 dir;
    private Transform bullet;
    
    private void Awake()
    {
        scanner = GetComponent<Scanner>();
    }

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
        if (!scanner.nearestTarget)
            return;
        
        dir = (scanner.nearestTarget.position - transform.position).normalized;
        
        bullet = GameManager.I.poolManager.GetBullet().transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        bullet.GetComponent<Bullet>().Init(damage, 10f, 3f, dir);
        
        SoundManager.I.PlayBullet();
    }
}
