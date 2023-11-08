using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    private CanvasGroup cg;
    
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    
    public bool isPause = false;
    
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (GameManager.I.playerBodyCollider.IsDead())
            return;

        if (GameManager.I.upgradeHandler.IsShowing())
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
                Active();
            }
            else
            {
                isPause = false;
                DeActive();
            }
        }
    }

    private void Active()
    {
        Time.timeScale = 0;
        
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    
    private void DeActive()
    {
        Time.timeScale = 1;
        
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    
    private void Restart()
    {
        SceneManager.LoadScene(1);
        
        Time.timeScale = 1;
    }
    
    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
