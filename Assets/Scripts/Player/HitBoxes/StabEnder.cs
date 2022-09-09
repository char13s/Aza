using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StabEnder : MonoBehaviour
{
    public static event UnityAction stop;
    private void OnTriggerEnter(Collider other) {
        stop.Invoke();
        Debug.Log("Interacted");
    }
}
