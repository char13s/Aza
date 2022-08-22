using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FindPlayer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        PlayerSpawnPoint.targetMe += SwitchPoint;
        CameraPoint.sendThis += LookATME;
    }
    void SwitchPoint(GameObject val) {
        vcam.Follow = val.transform;
    }
    void LookATME(GameObject val) { 
        vcam.LookAt= val.transform;
    }
}
