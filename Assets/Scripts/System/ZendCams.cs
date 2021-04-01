using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class ZendCams : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera freeLook;
    [SerializeField] private CinemachineVirtualCamera lockOnCam;
    // Start is called before the first frame update
    void Start()
    {
        Player.lockOn += CamSwitch;
    }
    private void CamSwitch(int val) {
        freeLook.Priority = val;
        print("switch: "+val);
    }
}
