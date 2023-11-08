using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeHandler : MonoBehaviour
{
    private CanvasGroup cg;

    private List<UpgradeInfo> upgradeInfoList;
    [SerializeField] private List<Transform> weaponList;
    private List<string> weaponOrder;
    
    private bool isShowing;
    
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        
        weaponOrder = new List<string>();
    }

    private void Start()
    {
        upgradeInfoList = new List<UpgradeInfo>();
        
        // UpgradeSlot을 찾아서 리스트에 추가
        UpgradeSlot[] slots = GetComponentsInChildren<UpgradeSlot>();
        foreach (var slot in slots)
        {
            upgradeInfoList.Add(new UpgradeInfo
            {
                level = 0,
                name = slot.name,
                upgradeSlot = slot
            });
        }
        
        upgradeInfoList[0].level = 1;
        weaponOrder.Add(upgradeInfoList[0].name);
    }

    public bool IsShowing()
    {
        return isShowing;
    }

    public void Active()
    {
        if (GameManager.I.playerBodyCollider.IsDead())
            return;
        
        GameManager.I.playerHandler.PlusHealth();
        
        HideAllSlot();
        
        if (upgradeInfoList.Count >= 3)
        {
            var numbers = new List<int>();
            
            for (var i = 0; i < upgradeInfoList.Count; i++)
            {
                numbers.Add(i);
            }
            
            var pickedNumbers = new List<int>();
            
            for (var i = 0; i < 2; i++)
            {
                var randomIndex = Random.Range(0, numbers.Count);
                
                pickedNumbers.Add(numbers[randomIndex]);
                numbers.RemoveAt(randomIndex);
            }

            foreach (var upgradeInfo in pickedNumbers.Select(number => upgradeInfoList[number]))
            {
                upgradeInfo.upgradeSlot.Set(upgradeInfo.level);
                upgradeInfo.upgradeSlot.gameObject.SetActive(true);
            }
            
        }
        else if (upgradeInfoList.Count is 1 or 2)
        {
            foreach (var upgradeInfo in upgradeInfoList)
            {
                upgradeInfo.upgradeSlot.Set(upgradeInfo.level);
                upgradeInfo.upgradeSlot.gameObject.SetActive(true);
            }
        }
        else
        {
            return;
        }

        SoundManager.I.PlayUpgradeActive();
        
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        
        isShowing = true;
        
        Time.timeScale = 0;
    }

    private void HideAllSlot()
    {
        foreach (var upgradeInfo in upgradeInfoList)
        {
            upgradeInfo.upgradeSlot.gameObject.SetActive(false);
        }
    }

    public void Click(UpgradeSlot slot)
    {
        SoundManager.I.PlayUpgrade();
        
        var upgradeInfo = upgradeInfoList.Find(info => info.upgradeSlot == slot);
            upgradeInfo.level++;

            switch (upgradeInfo.name)
            {
                case "Fire":
                    GameManager.I.playerHandler.fireDamage += 1;
                    GameManager.I.playerHandler.fireReloadTime -= 1;
                    break;
                case "Arrow":
                    if (GameManager.I.arrowHandler.reloadTime == -1)
                    {
                        GameManager.I.arrowHandler.Init(2, 4);
                    }
                    else
                    {
                        GameManager.I.arrowHandler.damage += 2;
                        GameManager.I.arrowHandler.reloadTime -= 1;
                    }
                    break;
                case "Bullet":
                    if (GameManager.I.bulletHandler.reloadTime == -1)
                    {
                        GameManager.I.bulletHandler.Init(1, 3.5f);
                    }
                    else
                    {
                        GameManager.I.bulletHandler.damage += 1;
                        GameManager.I.bulletHandler.reloadTime -= 1;
                    }
                    break;
                case "Knife":
                    switch (GameManager.I.slashHandler.slashLevel)
                    {
                        case 0:
                            GameManager.I.slashHandler.Init(3.5f, 1);
                            break;
                        case 1:
                            GameManager.I.slashHandler.slashLevel++;
                            GameManager.I.slashHandler.slashTime -= 1;
                            break;
                        case 2:
                            GameManager.I.playerHandler.slashDamage += 2;
                            GameManager.I.slashHandler.slashTime -= 1;
                            break;
                        case 3:
                            GameManager.I.slashHandler.slashLevel++;
                            GameManager.I.slashHandler.slashTime -= .5f;
                            break;
                    }
                    break;
            }

        if (upgradeInfo.level == 1)
        {
            weaponOrder.Add(upgradeInfo.name);

            Array();
        }
        else if (upgradeInfo.level == 4)
        {
            Remove(slot);
        }

        Hide();
    }

    private void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        
        isShowing = false;
        
        Time.timeScale = 1;
    }

    private void Array()
    {
        for (int i = 0; i < weaponOrder.Count; i++)
        {
            var weapon = weaponOrder[i];
            weaponList.Find(w => w.name == weapon).SetSiblingIndex(i);
        }
    }
    
    private void Remove(UpgradeSlot slot)
    {
        slot.gameObject.SetActive(false);
        upgradeInfoList.Remove(upgradeInfoList.Find(info => info.upgradeSlot == slot));
    }
        
}

    public class UpgradeInfo
    {
        public int level;
        public string name;
        public UpgradeSlot upgradeSlot;
    }