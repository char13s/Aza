using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneDialogue : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private string talker;
    [SerializeField]private bool forBeginning;
    [SerializeField] private int eventNum;
    private int current;
    public static event UnityAction<int> sendEndEvent;
    public int Current { get => current; set => current = value; } //Mathf.Clamp(value,0,lines.Length-1); } }

    public static event UnityAction<string> sendName;
    public static event UnityAction<string> sendLine;
    public static event UnityAction<bool> pullUpDialogue;
    public static event UnityAction<bool> turnOffDialogue;
    // Start is called before the first frame update
    void Start()
    {
        GameController.onNewGame += TheFirstDialouge;
        DialogueManager.requestNextLine += ProcessLineRequest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitABit() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        if (pullUpDialogue != null) {
            pullUpDialogue(true);
        }
        if (sendName != null) {
            sendName(talker);
        }
        if (sendLine != null) {
            sendLine(lines[0]);
        }
    }
    private void TheFirstDialouge() {
        if (forBeginning) {
            StartCoroutine(WaitABit());
        }
    }
    private void ProcessLineRequest() {
        Current++;
        if (current == lines.Length) {
            if (turnOffDialogue != null) {
                turnOffDialogue(false);
            }
            if (sendEndEvent != null) {
                sendEndEvent(eventNum);
            }
        }
        else {
            if (sendLine != null) {
                sendLine(lines[current]);
            }
        }
        
    }
}
