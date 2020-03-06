using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour
{
    public static UnityAction<int> triggered;
    public static UnityAction<int> close;
    [SerializeField] private int dialogueNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")|| other.CompareTag("Dashu")) {
            Debug.Log("bruh");
            if (triggered != null) {
                triggered(dialogueNum);
            }
            StartCoroutine(WaitToClose());
        }
    }
    private IEnumerator WaitToClose() {
        YieldInstruction wait = new WaitForSeconds(3f);
        yield return wait;
        if (close != null) {
            close(dialogueNum);
        }
    }
}
