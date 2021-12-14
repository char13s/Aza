using UnityEngine;
using UnityEngine.Events;

public class StopTime : StateMachineBehaviour
{
    public static event UnityAction zaWarudo;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (zaWarudo != null) {
            zaWarudo();
        }
    }
}
