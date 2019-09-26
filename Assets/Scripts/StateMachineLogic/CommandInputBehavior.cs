using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInputBehavior : StateMachineBehaviour {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Player.GetPlayer().CmdInput = 0;
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Input.GetButtonDown("X")) {
			Player.GetPlayer().CmdInput = 1;
		}
		if (Input.GetButtonDown("Triangle")) {
			Player.GetPlayer().CmdInput = 2;
		}

	}

}
