using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossRoom : MonoBehaviour
{
    public static event UnityAction triggerBossRoom;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (triggerBossRoom != null) {
                triggerBossRoom();
            }
        }
    }
}
