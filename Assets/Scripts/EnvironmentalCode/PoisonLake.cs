using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PoisonLake : MonoBehaviour
{
    public static event UnityAction poisoned;
    public static event UnityAction unpoisoned;
    
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            if (poisoned != null) {
                poisoned();
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            if (unpoisoned != null) {
                unpoisoned();
            }
        }
    }
}
