using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    
    public void SetTimer(float minute, float second)
    {
        timerText.text = $"{minute:00} : {second:00}";
    }
}
