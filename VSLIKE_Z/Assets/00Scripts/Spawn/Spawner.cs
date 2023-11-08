using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoints;

    private int level;
    private float timer;
 
    [SerializeField] private ClearHandler clearHandler;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (clearHandler.isClear)
            return;
        
        if (GameManager.I.playerBodyCollider.IsDead())
            return;


        if (GameManager.I.upgradeHandler.IsShowing())
            return;

        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.I.gameTime / 10f);

        if (timer > Mathf.Clamp(1.2f - level * 0.1f, 0.1f, 1.2f))
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        var zombie = GameManager.I.poolManager.GetZombie(Mathf.Min(Random.Range(0, level + 1), 7));
        zombie.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        zombie.GetComponent<Enemy>().Init(level);
    }
}