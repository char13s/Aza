using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBehavior : StateMachineBehaviour
{
    private Enemy enemy;
    public Enemy Enemy { get => enemy; set => enemy = value; }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Enemy.UnsetHit();
        Debug.Log("Okay Im good now");
    }
}
