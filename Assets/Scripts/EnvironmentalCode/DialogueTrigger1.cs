using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueTrigger1 : MonoBehaviour
{
    public static event UnityAction<bool> dialogueUp;
    private SceneDialogue dialogue;
    private void Awake() {
        dialogue = GetComponent<SceneDialogue>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            dialogue.enabled = true;
            if (dialogueUp != null) {
                dialogueUp(true);
            }
        }
    }
}
