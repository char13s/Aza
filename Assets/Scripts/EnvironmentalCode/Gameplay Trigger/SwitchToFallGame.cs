using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwitchToFallGame : MonoBehaviour
{
    [Tooltip("2 for entering, 0 for exiting")]
    [SerializeField] private int controlSet;
    [Tooltip("1000000 for entering, 0 for exiting")]
    [SerializeField] private int camPriority;
    [SerializeField] private int level;
    public static event UnityAction<int> switchToFall;
    public static event UnityAction<int> switchCam;
    public static event UnityAction changeControls;
    public static event UnityAction<int> unloadLevel;
    public static event UnityAction unparent;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        //switch controls
        switchToFall.Invoke(controlSet);
        //switch camera

        //change gravity fall speed
        if (switchCam != null) {
            switchCam(camPriority);
        }
        //Switch animation layer
        unparent.Invoke();
        //unload level where portal is
        if (level > 0) {
            unloadLevel.Invoke(level);
        }
    }
}
