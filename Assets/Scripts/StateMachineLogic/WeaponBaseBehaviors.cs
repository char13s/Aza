using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseBehaviors : StateMachineBehaviour
{
    private Player pc;
    
    [SerializeField]private int typeWeapon;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        GetWeapon();
        
    }
   
    private void GetWeapon() {
        

    }
}
