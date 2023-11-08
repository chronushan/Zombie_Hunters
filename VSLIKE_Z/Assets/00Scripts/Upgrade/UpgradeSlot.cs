using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public Image iconImage;
    [SerializeField] private List<Image> upgradeLevelImages;

    [SerializeField] private TextMeshProUGUI contextText;
    [SerializeField] private List<string> contexts;
    
    
    
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    
    private void Click()
    {
        foreach (var image in upgradeLevelImages.Where(image => !image.enabled))
        {
            if (!iconImage.enabled)
                iconImage.enabled = true;
            
            image.enabled = true;
            break;
        }

        GameManager.I.upgradeHandler.Click(this);
    }
    
    public void Set(int index)
    {
        var context = contexts[index];
        
        // context에 \n이 있으면 줄바꿈
        if (context.Contains("\\n"))
        {
            context = context.Replace("\\n", "\n");
        }
        
        contextText.text = context;
    }
}
