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

    }

}
