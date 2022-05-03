using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFist : StateMachineBehaviour
{
    [SerializeField] private GameObject demonFist;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //Instantiate(demonFist,Player.GetPlayer().CenterBodyPoint.transform.position, Player.GetPlayer().CenterBodyPoint.transform.rotation);
    }
}
