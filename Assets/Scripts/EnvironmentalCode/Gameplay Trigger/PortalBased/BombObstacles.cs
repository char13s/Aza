using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObstacles : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
    }
    private void OnTriggerEnter(Collider other) {
        player.stats.HealthLeft -= 5;
    }
}
