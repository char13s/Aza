using UnityEngine;
using UnityEngine.Events;
#pragma warning disable 0649
public class Slam : StateMachineBehaviour
{
    [SerializeField] private GameObject boom;
    private GameObject AoeHitbox;
    public static event UnityAction<float> slam;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AoeHitbox = Player.GetPlayer().AoeHitbox1;
        if (slam != null) {
            slam(10375);
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime > 0.95f)
        {
            AoeHitbox.SetActive(true);
            //Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
            Player.GetPlayer().RBody.drag = 0;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (slam != null)
        {
            slam(68.5f);
        }
		Instantiate(boom, Player.GetPlayer().DemonSword.transform.position, Quaternion.identity);
		AoeHitbox.SetActive(false);
    }
}
