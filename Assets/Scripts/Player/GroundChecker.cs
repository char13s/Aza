using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour {
    private AudioClip landing;
    public static event UnityAction<AudioClip> landed;

    private float distanceGround;
    private Player player;
    // Start is called before the first frame update
    void Start() {
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        player = Player.GetPlayer();
    }
    private void FixedUpdate() {
        if (!Physics.Raycast(transform.position, -Vector2.up, distanceGround + 0.1f)) {
            player.Grounded = false;
        }
        else {
            player.Grounded = true;
        }
    }
}
