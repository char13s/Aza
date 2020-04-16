using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : StateMachineBehaviour
{
    [SerializeField] private GameObject thunderArrow;
    [SerializeField] private GameObject fireArrow;
    [SerializeField] private GameObject basic;
    private GameObject arrow;
    private Player pc;
    private bool shot;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        arrow = ArrowSelected();
        Instantiate(arrow, pc.transform.position + new Vector3(0, 0.4F, 0), Quaternion.identity);
    }
    private GameObject ArrowSelected() {
        switch (pc.ArrowType) {
            case 0:
                return fireArrow;
            case 1:
                return thunderArrow;
            case 2:
                return basic;
            default:return basic;
        }
    }
}
