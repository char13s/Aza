using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReciever : MonoBehaviour
{
    private Player player;

    [SerializeField] private LightBulbHolder holder;
    // Start is called before the first frame update
    void Start() {
        player = Player.GetPlayer();
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {


            if (player.Charging) {
                holder.Powered = true;
            }
        }
    }
}
