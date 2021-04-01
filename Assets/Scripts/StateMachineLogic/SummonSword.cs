using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SummonSword : StateMachineBehaviour
{
    [SerializeField] private bool active;
    public static event UnityAction<bool> activate;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        activate.Invoke(active);
    }
}
