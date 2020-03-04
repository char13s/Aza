using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour {
    [SerializeField] private GameObject burst;
    [SerializeField] private GameObject reminant;
    [SerializeField] private bool freefall;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Player.GetPlayer().LeftHand.SetActive(true);
        //Player.GetPlayer().RightHand.SetActive(true);
        //Player.GetPlayer().DevilFoot.SetActive(true);
        if (burst != null) {
            Instantiate(burst, Player.GetPlayer().transform.position, Player.GetPlayer().transform.rotation);
        }
        //burst.transform.position=

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Player.GetPlayer().RBody.drag = 0;
        if (freefall) {
            if (FreeFallZend.GetFreeFallingZend().Falling) {
                Instantiate(reminant, FreeFallZend.GetFreeFallingZend().transform.position + new Vector3(0, 0, -0.25f), FreeFallZend.GetFreeFallingZend().transform.rotation);
            }
        }
        else {
            Instantiate(reminant, Player.GetPlayer().transform.position, Player.GetPlayer().transform.rotation);
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Player.GetPlayer().LeftHand.SetActive(false);
        Player.GetPlayer().RightHand.SetActive(false);

    }

}
