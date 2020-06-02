using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingFireBall : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private GameObject fire;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        
        //Instantiate(fire,pc.RightHand.transform.position,Quaternion.identity);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (Input.GetButton("Triangle")) {
            Instantiate(fire, pc.transform.position, Quaternion.identity);
        }
        if (Input.GetButtonUp("Triangle")) {
            pc.SkillId = 1;
            
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        
    }
}
