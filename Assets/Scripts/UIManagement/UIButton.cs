using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UIButton : MonoBehaviour
{
    public static event UnityAction click;
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(click);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
