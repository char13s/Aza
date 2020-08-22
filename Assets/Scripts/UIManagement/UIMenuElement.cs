using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIMenuElement : MonoBehaviour
{
    //private enum UIElement { }
    //[SerializeField] private UIElement element;
    [SerializeField] private Text text;
    [SerializeField] private int id;
    public static event UnityAction equipment;
    public static event UnityAction skills;
    public static event UnityAction combos;
    public static event UnityAction map;
    //public static event UnityAction upgradeMenu; Maybe

    public void Use() {
        switch (id) {
            case 0:
                Debug.Log("Item with id num: "+id+" selected");
                if (map != null) {
                    map();
                }
                break;
            case 1:
                if (equipment != null) {
                    equipment();
                }
                Debug.Log("Item with id num: " + id + " selected");
                break;
            case 2:
                if (combos != null) {
                    combos();
                }
                Debug.Log("Item with id num: " + id + " selected");
                break;
            case 3:
                if (skills != null) {
                    skills();
                }
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
