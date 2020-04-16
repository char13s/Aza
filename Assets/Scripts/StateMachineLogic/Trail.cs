using UnityEngine;


public class Trail : StateMachineBehaviour
{
    [SerializeField] private GameObject reminant;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (reminant != null) {
            Instantiate(reminant, Player.GetPlayer().transform.position, Player.GetPlayer().transform.rotation);
        }
    }

}
