using UnityEngine;

public class HitBoxBehaviour : StateMachineBehaviour
{
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    private GameObject hitbox;
    [SerializeField]private int hitType;
    private Player pc;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
    }
}
