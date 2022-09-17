using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Cinemachine.PostFX;

public class VirtualCameraManager : MonoBehaviour
{
    //[SerializeField] private CinemachineVirtualCamera main;
    [Header("CamRefs")]
    //[SerializeField] private CinemachineFreeLook freeCam;
    [SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private CinemachineVirtualCamera battleCam;

    //[SerializeField] private CinemachineVirtualCamera meditationCam;
    //[SerializeField] private CinemachineVirtualCamera titleScreenCam;

    //[SerializeField] private CinemachineVirtualCamera archeryCam;

    //[SerializeField] private CinemachineVirtualCamera weakZendCam;
    //[SerializeField] private CinemachineVirtualCamera deathCam;
    //[SerializeField] private CinemachineVirtualCamera swordsCam;

    [Header("Cinematic cams")]
    //[SerializeField] private CinemachineVirtualCamera firstDemonSpawn;
    //[SerializeField] private CinemachineVirtualCamera nextCam;
    //[SerializeField] private CinemachineVirtualCamera dedCam;
    
    [Header("ObjectRefs")]
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject titleScreenObjs;
    [SerializeField] private GameObject aimingPoint;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject lightning;
    [Space]
    [Header("Effects Camera")]
    [SerializeField] private GameObject zoomIn;
    private Player pc;
    private bool freelook;
    private Vector3 currentEulerAngles;

    //public bool Freelook { get => freelook; set { freelook = value; if (freelook) { freeCam.m_Priority = 12; } else { freeCam.m_Priority = 1; } } }

    public static event UnityAction spawnDemon;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    private void OnEnable() {
        Player.playerIsLockedOn += LookingForTarget;
        Player.notAiming += NotAiming;
        Player.zoom += ZoomIn;

    }
    private void OnDisable() {
        
    }
    private void ZoomIn(int val) {
        main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = val;
        if(val==4)
            main.GetComponent<CinemachineVolumeSettings>().enabled = true;
        else
            main.GetComponent<CinemachineVolumeSettings>().enabled = false;
    }

    private void LookingForTarget() {
        
        battleCam.m_Priority = 24;
        
    }
    private void NotAiming() {
        battleCam.m_Priority = 1;
    }

	private void RetargetBattleCam() {
		battleCam.m_LookAt = Player.GetPlayer().transform;
        battleCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Vector3.Distance(Player.GetPlayer().BattleMode.EnemyTarget.transform.position, Player.GetPlayer().transform.position);
	}


}
