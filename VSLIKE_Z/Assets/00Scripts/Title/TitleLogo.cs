using DG.Tweening;
using TMPro;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    [SerializeField] private TitleStartButtonHandler titleStartButtonHandler;

    [SerializeField] private TextMeshProUGUI text;
    
    private async void Start()
    {
        await text.DOColor(Color.white, 3.0f).From(Color.black).SetEase(Ease.Linear).AsyncWaitForCompletion();
        
        await text.transform.DOLocalMoveY(100, 1.0f).From(0).SetEase(Ease.Linear).AsyncWaitForCompletion();
        
        titleStartButtonHandler.Active();
    }
}
