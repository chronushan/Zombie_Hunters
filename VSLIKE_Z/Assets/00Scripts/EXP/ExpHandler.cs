using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpHandler : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    public int level;
    public float exp;
    public float maxExp;

    private List<float> expList = new List<float>();
    
    private void Awake()
    {
        level = 1;
        exp = 0;
        maxExp = 3;
    }

    private void Start()
    {
        expList.Add(3);
        expList.Add(7); // +4
        expList.Add(15); // +8
        expList.Add(25); // +10
        expList.Add(40); // +15
        expList.Add(60); // +20
        expList.Add(85); // +25
        expList.Add(115); // +30
        expList.Add(155); // +40
        expList.Add(210); // +55
        expList.Add(280); // +70
        expList.Add(370); // +90
        expList.Add(480); // +110
        expList.Add(610); // +130
        expList.Add(800); // +150
        expList.Add(1000); // +240
        expList.Add(1000); // +310
        expList.Add(1000); // +400
        expList.Add(1000); // +500
        expList.Add(1000); // +650
    }

    public void AddExp(float exp)
    {
        this.exp += exp;
        
        if (this.exp >= maxExp)
        {
            this.exp -= maxExp;
            level++;
            
            if (expList.Count < level)
            {
                maxExp = expList[expList.Count - 1];
            }
            else
            {
                maxExp = expList[level - 1];
            }
            
            SetLevel();
        }
        
        SetExp();
    }
    private void SetExp()
    {
        expSlider.value = exp / maxExp;
    }

    private void SetLevel()
    {
        levelText.text = "Level : " + level;
        
        GameManager.I.upgradeHandler.Active();
    }
}
