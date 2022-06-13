using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxBehavior : StateMachineBehaviour
{
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    private GameObject hitBox;

    public GameObject HitBox { get => hitBox; set => hitBox = value; }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (HitBox == null) {
            Debug.Log("oh no");
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff)
        {
            HitBox.SetActive(true);
        }
        else
        {
            HitBox.SetActive(false);
        }
    }
}
