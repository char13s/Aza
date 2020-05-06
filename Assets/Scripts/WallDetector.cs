using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallDetector : MonoBehaviour
{

    private Player pc;
    private void Start() {
        pc = Player.GetPlayer();
    }
    private void OnDisable() {
       // pc.MoveSpeed = 6;
    }
    private void OnTriggerEnter(Collider other) {
        pc.JumpForce = 0;
        pc.Jumping = false;
        Debug.Log("something is colliding with the wall detector");
        
    }
    private void OnTriggerExit(Collider other) {
        
    }
}
