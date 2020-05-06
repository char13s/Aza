using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HoldAndRelease : StateMachineBehaviour {
    private Player pc;
    [SerializeField] private GameObject thunderArrow;
    [SerializeField] private GameObject fireArrow;
    [SerializeField] private GameObject basic;
    private GameObject arrow;
    public static event UnityAction destroy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        pc = Player.GetPlayer();
         
        //arrow =Instantiate(MakeArrow(), pc.RightHand.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (pc.BowUp) {
            if (Input.GetButtonUp("L1")) {
                pc.CmdInput = 0;
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (destroy != null) {
            destroy();
        }
    }
    private GameObject MakeArrow() {

        switch (pc.ArrowType) {
            case 0:
                return basic;
            case 1:
                return thunderArrow;
            case 2:
                return fireArrow;
            default: return basic;
        }
    }
}
