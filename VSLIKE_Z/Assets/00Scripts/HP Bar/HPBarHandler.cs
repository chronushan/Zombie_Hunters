using UnityEngine;

public class HPBarHandler : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    
    public void SetHPBar(float hp, float maxHp)
    {
        pivot.localScale = new Vector3(hp / maxHp, 1, 1);
    }
}
