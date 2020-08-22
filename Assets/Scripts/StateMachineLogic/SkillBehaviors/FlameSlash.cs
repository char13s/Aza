using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSlash : StateMachineBehaviour
{
    [SerializeField] private GameObject fireSlash;
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Instantiate(fireSlash, Player.GetPlayer().transform.position + new Vector3(0, 0.4F, 0), Quaternion.identity);
        fireSlash.transform.position = animator.rootPosition;
    }
}
