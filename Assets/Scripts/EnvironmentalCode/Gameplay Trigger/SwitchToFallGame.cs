using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwitchToFallGame : MonoBehaviour
{
    public static event UnityAction<int> switchToFall;
    public static event UnityAction<int> switchCam;
    public static event UnityAction changeControls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //switch controls
        switchToFall.Invoke(2);
        //switch camera

        //change gravity fall speed
        switchCam.Invoke(100000);
        //Switch animation layer

    }
}
