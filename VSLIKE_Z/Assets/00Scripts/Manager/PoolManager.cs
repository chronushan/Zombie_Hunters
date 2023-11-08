using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject zombieBallPrefab;
    [SerializeField] private GameObject zombieBall2Prefab;
    

    private List<GameObject>[] zombiePoolList;
    private List<GameObject> bulletPoolList;
    private List<GameObject> arrowPoolList;
    private List<GameObject> zombieBallPoolList;
    private List<GameObject> zombieBall2PoolList;

    private void Awake()
    {
        zombiePoolList = new List<GameObject>[zombiePrefabs.Length];
        
        for (int i = 0; i < zombiePoolList.Length; i++)
        {
            zombiePoolList[i] = new List<GameObject>();
        }
        
        bulletPoolList = new List<GameObject>();
        arrowPoolList = new List<GameObject>();
        zombieBallPoolList = new List<GameObject>();
        zombieBall2PoolList = new List<GameObject>();
    }

    public GameObject GetZombie(int index)
    {
        GameObject zombie = null;

        foreach (var pool in zombiePoolList[index])
        {
            if (!pool.activeSelf)
            {
                zombie = pool;
                zombie.SetActive(true);
                break;
            }
        }

        if (!zombie)
        {
            zombie = Instantiate(zombiePrefabs[index], transform);
            zombiePoolList[index].Add(zombie);
        }
        
        return zombie;
    }

    public GameObject GetBullet()
    {
        GameObject bullet = null;
        
        foreach (var pool in bulletPoolList.Where(pool => !pool.activeSelf))
        {
            bullet = pool;
            bullet.SetActive(true);
            break;
        }

        if (bullet)
            return bullet;
        
        bullet = Instantiate(bulletPrefab, transform);
        bulletPoolList.Add(bullet);

        return bullet;
    }
    
    public GameObject GetArrow()
    {
        GameObject arrow = null;
        
        foreach (var pool in arrowPoolList.Where(pool => !pool.activeSelf))
        {
            arrow = pool;
            arrow.SetActive(true);
            break;
        }

        if (arrow)
            return arrow;
        
        arrow = Instantiate(arrowPrefab, transform);
        arrowPoolList.Add(arrow);

        return arrow;
    }
    
    public GameObject GetZombieBall()
    {
        GameObject zombieBall = null;
        
        foreach (var pool in zombieBallPoolList.Where(pool => !pool.activeSelf))
        {
            zombieBall = pool;
            zombieBall.SetActive(true);
            break;
        }

        if (zombieBall)
            return zombieBall;
        
        zombieBall = Instantiate(zombieBallPrefab, transform);
        zombieBallPoolList.Add(zombieBall);

        return zombieBall;
    }
    
    public GameObject GetZombieBall2()
    {
        GameObject zombieBall2 = null;
        
        foreach (var pool in zombieBall2PoolList.Where(pool => !pool.activeSelf))
        {
            zombieBall2 = pool;
            zombieBall2.SetActive(true);
            break;
        }

        if (zombieBall2)
            return zombieBall2;
        
        zombieBall2 = Instantiate(zombieBall2Prefab, transform);
        zombieBall2PoolList.Add(zombieBall2);

        return zombieBall2;
    }

    public void Final()
    {
        foreach (var zombiePool in zombiePoolList)
        {
            foreach (var zombie in zombiePool)
            {
                zombie.GetComponent<Enemy>().FinalDead();
            }
        }

        foreach (var ball in zombieBallPoolList)
        {
            ball.GetComponent<EnemyBall>().Final();
        }

        foreach (var ball2 in zombieBall2PoolList)
        {
            ball2.GetComponent<EnemyBall>().Final();
        }
    }
}
