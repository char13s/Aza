using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] private int tutorialNum;
    private bool triggered;
    public static event UnityAction<int> requestTutorial;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")&&!triggered) {
            if (requestTutorial != null) {
                requestTutorial(tutorialNum);
            }
            triggered = true;
        }
    }
}
