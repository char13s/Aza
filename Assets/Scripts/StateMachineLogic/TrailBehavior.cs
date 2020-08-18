using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : StateMachineBehaviour
{
    [SerializeField]private GameObject trail;

    private Player player;
    private GameObject position;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        position = player.TrailPoint;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(trail, position.transform.position, Quaternion.identity);
    }
}
