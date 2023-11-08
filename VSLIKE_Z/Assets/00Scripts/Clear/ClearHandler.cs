using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClearHandler : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup cg;
    [SerializeField] private TextMeshProUGUI clearText;

    public bool isClear;
    
    public void Active()
    {
        // DoColor를 이용해서 clearText 색상 랜덤으로 변경
        clearText.DOColor(Random.ColorHSV(), 1f);

        if (clearText.transform.parent.gameObject.activeSelf)
        {
            // 반복
            Invoke(nameof(Active), 1f);
        }
        
        GameManager.I.poolManager.Final();

        isClear = true;
        
        cg.alpha = 1;
    }
}
