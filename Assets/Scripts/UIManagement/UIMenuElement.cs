using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMenuElement : MonoBehaviour
{
    //private enum UIElement { }
    //[SerializeField] private UIElement element;
    [SerializeField] private Text text;
    [SerializeField] private int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Use() {
        switch (id) {
            case 0:
                Debug.Log("Item with id num: "+id+" selected");
                break;
            case 1:
                Debug.Log("Item with id num: " + id + " selected");
                break;
            case 2:
                Debug.Log("Item with id num: " + id + " selected");
                break;
            case 3:
                Debug.Log("Item with id num: " + id + " selected");
                break;
        }
    }
    public void OnSelected() {
        Debug.Log("element was selected.");
        text.color = new Color(1,1,1,1);
        text.fontSize = 60;
    }
    public void UnSelect() {
        text.color = new Color(0,0,0);
        text.fontSize = 24;
    }
}
