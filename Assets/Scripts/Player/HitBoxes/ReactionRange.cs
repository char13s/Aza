using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ReactionRange : MonoBehaviour
{
    public static event UnityAction dodged;
    public static event UnityAction<int> zoom;
    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<EnemyHitBox>()) {
            Debug.Log("Enemy Hit Box Detected");
            if (dodged != null) {
                dodged();
            }
            //Time.timeScale = 0.1f;
            //zoom.Invoke(4);
            //StartCoroutine(ResetTimeStop());
        }
    }
    private IEnumerator ResetTimeStop() {
        //YieldInstruction wait;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        zoom.Invoke(7);
        print("Reset");
    }
}
