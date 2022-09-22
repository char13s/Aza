using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VoidSettings : MonoBehaviour
{
    public static event UnityAction resetVoid;
    // Start is called before the first frame update
    void Start() {

    }
    public void RestartVoidButton() {
        resetVoid.Invoke();
    }
}
