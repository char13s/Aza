using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chidori : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private GameObject chidori;
    [SerializeField] private GameObject electricField;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.MoveSpeed = 0;
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (Input.GetButton("Triangle")) {
            Instantiate(electricField,pc.transform);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Instantiate(chidori,pc.RightHand.transform);
    }
}
