using UnityEngine;

public class DemonFistHitBox : StateMachineBehaviour
{
    [SerializeField] float hitOn;
    [SerializeField] float hitOff;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (animatorStateInfo.normalizedTime >= hitOn && animatorStateInfo.normalizedTime <= hitOff) {
            Player.GetPlayer().FistHitBox.SetActive(true);
        }
        else {
            Player.GetPlayer().FistHitBox.SetActive(false);
        }
    }
}
