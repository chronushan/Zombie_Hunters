using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    private CanvasGroup cg;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
    }
    
    public async void GameOver()
    {
        SoundManager.I.PlayBGM(2);
        
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        
        await gameOverText.DOColor(Color.red, 3f).SetEase(Ease.Linear).AsyncWaitForCompletion();
        
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        
        Time.timeScale = 0;
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
