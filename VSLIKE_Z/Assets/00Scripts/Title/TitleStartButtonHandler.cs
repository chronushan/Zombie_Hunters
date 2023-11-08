using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleStartButtonHandler : MonoBehaviour
{
    private EventTrigger eventTrigger;
    [SerializeField] private TextMeshProUGUI text;
    private CanvasGroup cg;
    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;

        // Enter, Click, Exit 이벤트를 등록
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { Enter(); });
        eventTrigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { Exit(); });
        eventTrigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { Click(); });
        eventTrigger.triggers.Add(entry);
    }
    
    private void Enter()
    {
        text.color = Color.red;
    }
    
    private void Exit()
    {
        text.color = Color.gray;
    }
    
    private void Click()
    {
        SceneManager.LoadScene(1);
    }


    public async void Active()
    {
        
        await cg.DOFade(1, 1.0f).From(0).AsyncWaitForCompletion();
        
        cg.blocksRaycasts = true;
        cg.interactable = true;
        
    }
}
