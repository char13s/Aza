using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraPoint : MonoBehaviour
{
    public static event UnityAction<GameObject> sendThis;
    // Start is called before the first frame update
    void Start()
    {
        sendThis.Invoke(gameObject);
    }
    private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }
}
