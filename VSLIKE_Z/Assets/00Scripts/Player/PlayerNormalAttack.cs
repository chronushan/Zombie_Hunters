using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : MonoBehaviour
{
    [SerializeField] private PlayerNormalAttackColliderHandler[] playerNormalAttackColliderHandlers;
    
    public void Active1()
    {
        playerNormalAttackColliderHandlers[0].Active();
    }
    
    public void Active2()
    {
        playerNormalAttackColliderHandlers[1].Active();
    }
    
    public void Active3()
    {
        playerNormalAttackColliderHandlers[2].Active();
    }
}
