using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0649
public class Slam : StateMachineBehaviour {
    [SerializeField] private GameObject boom;
    private GameObject AoeHitbox;
    [SerializeField] private bool spin;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AoeHitbox = Player.GetPlayer().AoeHitbox;
        //if (slam != null) {
        //    slam(10375);
        //}
        player = Player.GetPlayer();
       // player.RBody.AddForce(new Vector3(0,-4.5f,0),ForceMode.VelocityChange);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (stateInfo.normalizedTime >= 0.95f) {
            AoeHitbox.SetActive(true);
            //Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
            
            //Player.GetPlayer().RBody.drag = 0;
        }
        if (stateInfo.normalizedTime > 0.2f && stateInfo.normalizedTime < 0.98f) {
            if (spin) {

            }
            else {
                Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);

            }


        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (slam != null) {
        //    slam(68.5f);
        //}

        //Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
        AoeHitbox.SetActive(false);
    }
}
