using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649
public class Slam : StateMachineBehaviour {
    [SerializeField] private GameObject boom;
    private GameObject AoeHitbox;
    [SerializeField] private bool spin;
    public static event UnityAction<float> slam;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AoeHitbox = Player.GetPlayer().AoeHitbox1;
        if (slam != null) {
            slam(10375);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (stateInfo.normalizedTime >= 0.95f) {
            AoeHitbox.SetActive(true);
            //Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
            GamePad.SetVibration(0, 0.75f, 0.75f);
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
        if (slam != null) {
            slam(68.5f);
        }
        GamePad.SetVibration(0, 0, 0);
        //Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
        AoeHitbox.SetActive(false);
    }
}
