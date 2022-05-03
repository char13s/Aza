using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Cast : StateMachineBehaviour
{
    [SerializeField] private GameObject burst;
    public static event UnityAction setWeightBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Instantiate(burst,Player.GetPlayer().LeftHand.transform.position,Quaternion.identity);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (setWeightBack != null) {
            setWeightBack();
        }
        Player.GetPlayer().CastItem = false;
    }
}
