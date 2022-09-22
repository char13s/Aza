using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
public class FindPlayer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField]bool isMain;
    public static event UnityAction<CinemachineVirtualCamera> sendThisCam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        PlayerSpawnPoint.targetMe += SwitchPoint;
        CameraPoint.sendThis += LookATME;
        Player.onPlayerEnabled += SendCam;
        //vcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering.m_enabled = true;
    }
    void SwitchPoint(GameObject val) {
        vcam.Follow = val.transform;
    }
    void LookATME(GameObject val) { 
        vcam.LookAt= val.transform;
    }
    void SendCam() { 
        if (isMain) {
            sendThisCam.Invoke(vcam);
        }
    
    }
}
