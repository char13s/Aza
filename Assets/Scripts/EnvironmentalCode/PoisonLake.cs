using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PoisonLake : MonoBehaviour {
    [SerializeField] private GameObject splash;
    public static event UnityAction<int> poisoned;
    private Coroutine poison;
    
    private void OnTriggerEnter(Collider other) {
        if (splash != null) {
            Instantiate(splash, transform.position, Quaternion.identity);
        }
        if (other.GetComponent<Player>()) {
            poison = StartCoroutine(Poison());
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            StopCoroutine(poison);
        }
    }
    private IEnumerator Poison() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        while (isActiveAndEnabled) {
            yield return wait;
            if (poisoned != null) {
                poisoned(1);
            }

        }

    }
}
