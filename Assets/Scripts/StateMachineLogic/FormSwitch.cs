using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FormSwitch : StateMachineBehaviour
{
    [SerializeField] private GameObject lightningChannel;
    [SerializeField] private GameObject fireGather;
    [SerializeField] private GameObject boom;
    [SerializeField] private GameObject gatherEffect;

    public static event UnityAction<bool> inviciblity;
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        gatherEffect = PickEffect();
        if (inviciblity != null) {
            inviciblity(true);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (animatorStateInfo.normalizedTime < 0.9) {
            Instantiate(gatherEffect,pc.CenterPoint.transform);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc.PowerUp = false;
        if (inviciblity != null) {
            inviciblity(false);
        }
    }
    private GameObject PickEffect() {
        switch (pc.Style) {
            case 1:
                return fireGather;
                
            case 2:
                return lightningChannel;
            default:return fireGather;
        }
    }
}
