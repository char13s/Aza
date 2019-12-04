using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueBlock : MonoBehaviour
{
    [SerializeField] Dialogue words = new Dialogue();
    [SerializeField]  private string[] lines;
    [System.NonSerialized] private int currentLine;
    [System.NonSerialized] private bool blockIsDone;
    [SerializeField] private int action;
    [SerializeField] private bool isEndingBlock;
    [SerializeField] private bool isDefaultblock;
    public bool BlockIsDone { get => blockIsDone; set { blockIsDone = value; } }
    public int CurrentLine { get => currentLine; set => currentLine = value; }
    public bool IsEndingBlock { get => isEndingBlock; set => isEndingBlock = value; }
    public int Action { get => action; set => action = value; }
    public bool IsDefaultblock { get => isDefaultblock; set => isDefaultblock = value; }

    private void Awake()
    {

        
        //endingLineOfEachPhrase = new byte[numberOfDifferentPhrases];
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ReadLine()
    {
        if (lines.Length  == currentLine)
            {
            Debug.Log("ya done");
                BlockIsDone = true;
            EndOfBlockEvent();
            }
        if (!BlockIsDone)
        {
            Debug.Log("new line"+currentLine);
            UiManager.GetUiManager().DialogueText.text = lines[currentLine];
            currentLine++;
            
            
        }
    }
    public void EndOfBlockEvent() {
        
        switch (action) {
            case 1:
                if (AzaNpc.bowUp != null) {
                    AzaNpc.bowUp();
                }
                Debug.Log("fuck you");
                break;
            case 2:
                if (AzaNpc.bowDown != null)
                {
                    AzaNpc.bowDown();
                }
                if (ObjectiveManager.mission1Active != null) {ObjectiveManager.mission1Active(); }
                
                break;
            case 3:
                if (Mission1.mission1Done != null)
                    Mission1.mission1Done();
                break;
			case 99:
				GameController.GetGameController().OnQuit();
				break;

        }

    }
}
