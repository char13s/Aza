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
        
        WhatTypeOfHit();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff)
        {
            hitbox.SetActive(true);
        }
        else
        {
           hitbox.SetActive(false);
        }
    }
    private void WhatTypeOfHit() {
        switch (hitType) {
            case 0:
                hitbox = pc.HitBox;
                break;
            case 1:
                hitbox = pc.KatanaHitbox;
                break;
            case 2:
                hitbox = pc.ScytheHitBox;
                break;
            case 3:
                hitbox = pc.WoodenSwordHitBox;
                break;
        }
    }
}
