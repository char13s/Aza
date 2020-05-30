using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueManager : MonoBehaviour {
    [SerializeField] private Text dialogue;
    //[SerializeField] private GameObject DialogueScreen;
    [SerializeField] private Text whoseTalking;
    [SerializeField] private GameObject textPanel;
    private bool dialogueIsRunning;
    public static event UnityAction requestNextLine;
    public static event UnityAction skipDialogue;
    // Start is called before the first frame update
    void Start()
    {
        SceneDialogue.pullUpDialogue += DialogueUp;
        SceneDialogue.turnOffDialogue += DialogueUp;
        SceneDialogue.sendName += SetTalker;
        SceneDialogue.sendLine += SetDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Circle")&&dialogueIsRunning) {
            if (requestNextLine != null) {
                requestNextLine();
            }

        }
        if (Input.GetButtonDown("Square")) {
            if (skipDialogue != null) {
                skipDialogue();
            }
            if (requestNextLine != null) {
                requestNextLine();
            }
        }
    }
    private void DialogueUp(bool val) {
        textPanel.SetActive(val);
        dialogueIsRunning = val;
    }
    private void SetTalker(string name) {
        whoseTalking.text = name;
    }
    private void SetDialogue(string text) {
        dialogue.text = text;
    }
    
}
