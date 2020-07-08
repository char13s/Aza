using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SpawnSetters : MonoBehaviour
{
    public static event UnityAction<GameObject> setSpawner;
    public static event UnityAction saveGame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (setSpawner != null) {
                setSpawner(gameObject);
            }
            StartCoroutine(AutoSave());
        }
    }
    private IEnumerator AutoSave() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return null;
        if (saveGame != null) {
            saveGame();
        }
    }
}
