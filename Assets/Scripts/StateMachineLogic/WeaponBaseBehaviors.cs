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
        pc.DemonSword.SetActive(false);
        pc.WoodenSword.SetActive(false);
        switch (typeWeapon) {
            case 0:pc.DemonSword.SetActive(true);

                break;
            case 1:
                //pc.AngelSword.SetActive(true);
                break;
            case 2:
                pc.WoodenSword.SetActive(true);
                break;
            //case 3:
            //    break;
        }
    }
}
