using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public PoolManager poolManager;
    public PlayerHandler playerHandler;
    public PlayerBodyCollider playerBodyCollider;
    public ExpHandler expHandler;
    [SerializeField] private TimerHandler timerHandler;
    public ArrowHandler arrowHandler;
    public BulletHandler bulletHandler;
    public SlashHandler slashHandler;
    [SerializeField] private ClearHandler clearHandler;
    public UpgradeHandler upgradeHandler;

    public float gameTime;
    public float maxGameTime = 5 * 10f;

    private float second;
    private float minute;

    private bool isClear;
    
    private void Awake()
    {
        I = this;
    }
    
    private void Update()
    {
        if (playerBodyCollider.IsDead())
            return;

        if (upgradeHandler.IsShowing())
            return;
        
        if (isClear)
            return;
        
        gameTime += Time.deltaTime;
        second = Mathf.FloorToInt(gameTime % 60);
        minute = Mathf.FloorToInt(gameTime / 60);

        timerHandler.SetTimer(minute, second);
        
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;

            Clear();
        }
    }

    private void Clear()
    {
        isClear = true;

        SoundManager.I.PlayBGM(1);
        
        clearHandler.Active();
    }
}
