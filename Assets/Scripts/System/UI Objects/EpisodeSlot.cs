using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class EpisodeSlot : MonoBehaviour
{
    //public static event UnityAction<int> off;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        //button.onClick.AddListener(IconAction);
    }
    private void IconAction() {
        
    }
}
