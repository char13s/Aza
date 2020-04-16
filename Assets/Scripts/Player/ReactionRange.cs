using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ReactionRange : MonoBehaviour
{

    public static event UnityAction dodged;
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            
            if (Player.GetPlayer().CombatAnimations!=0&&!activated) {
                if (dodged != null) {
                    dodged();
                }
                activated = true;
                StartCoroutine(WaitToReset());
            }

        }

    }
    private IEnumerator WaitToReset() {
        YieldInstruction wait = new WaitForSeconds(5);
        yield return wait;
        activated = false;
    }
}
