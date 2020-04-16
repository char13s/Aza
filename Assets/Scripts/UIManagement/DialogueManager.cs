using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    [SerializeField] private Text dialogue;
    [SerializeField] private GameObject DialogueScreen;
    [SerializeField] private Image character1;
    [SerializeField] private Image character2;
    [SerializeField] private GameObject textPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DialogueUp(bool val) {
        DialogueScreen.SetActive(val);
    }
}
