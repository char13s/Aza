using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SpawnSetters : MonoBehaviour
{
    public static event UnityAction<GameObject> setSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (setSpawner != null) {
                setSpawner(gameObject);
            }
        }
    }
}
