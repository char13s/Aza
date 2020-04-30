using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class VirtualCameraManager : MonoBehaviour
{
    //[SerializeField] private CinemachineVirtualCamera main;
    [Header("CamRefs")]
    [SerializeField] private CinemachineFreeLook freeCam;
    [SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private CinemachineVirtualCamera battleCam;

    [SerializeField] private CinemachineVirtualCamera meditationCam;
    [SerializeField] private CinemachineVirtualCamera titleScreenCam;

    [SerializeField] private CinemachineVirtualCamera archeryCam;

    [SerializeField] private CinemachineVirtualCamera weakZendCam;
    [SerializeField] private CinemachineVirtualCamera deathCam;
    [SerializeField] private CinemachineVirtualCamera swordsCam;
    [Space]
    [Header("ObjectRefs")]
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject titleScreenObjs;
    [SerializeField] private GameObject aimingPoint;
    [Space]

    private Player pc;
    private bool freelook;
    private Vector3 currentEulerAngles;

    public bool Freelook { get => freelook; set { freelook = value; if (freelook) { freeCam.m_Priority = 12; } else { freeCam.m_Priority = 1; } } }

    public static event UnityAction grey;
    public static event UnityAction ungrey;
    public static event UnityAction weakZend;
    // Start is called before the first frame update
    private void Awake()
    {
        Player.playerIsLockedOn += LookingForTarget;
        Player.notAiming += NotAiming;
        Player.archery += ArcheryCamUp;
        //Player.onPlayerDeath += TurnTitleCamOn;
        UiManager.portal += ControlMainCam;

		PlayerBattleSceneMovement.onLockOn += AimBattleCam;

        ExpConverter.levelMenuUp += MeditationCam;

        GameController.onNewGame += TurnOffTitleScreenCam;
        GameController.respawn += TurnOffTitleScreenCam;
        GameController.returnToLevelSelect+=TurnTitleCamOn;
        GameController.readyDeathCam += DeathCamUp;


    }
    void Start()
    {
        pc = Player.GetPlayer();
        EventManager.endOfDeathIntro += CamTransitions;

        EventTrigger.chooseSword+=SwordsCam;
        UiManager.demonSword += SwordsCamDown;
        UiManager.angelSword += SwordsCamDown;
        aimingPoint = Player.GetPlayer().AimmingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("R3")) {
        //
        //    FreeLookControls();
        //}
    }
    private void FreeLookControls() {
        if (freelook) {
            Freelook = false;
        }
        else {
            Freelook = true;
        }
    }
    private void ControlMainCam(int portal) {
        switch (portal) {
            case 0:
                Debug.Log("Cam off");
                main.GetComponent<ThreeDCamera>().enabled=false;
                break;
            default:
                Debug.Log("Cam on");
                main.GetComponent<ThreeDCamera>().enabled = true;
                break;
        }
        
    }
    private void ArcheryCamUp(int priority) {
        archeryCam.m_Priority = priority;
    }
    private void LookingForTarget() {
        //main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 1;
        battleCam.m_Priority = 24;
        if (pc.BattleMode.EnemyTarget != null) {
            //ttleCam.m_LookAt = Player.GetPlayer().BattleMode.EnemyTarget.transform;
        }
        else {
            battleCam.m_LookAt = aimingPoint.transform;
        }
        main.transform.position = battleCam.transform.position;
        //if (grey != null) {
        //    grey();
        //}
    }
    private void NotAiming() {
        //main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 6;
        //main.m_LookAt = Player.GetPlayer().transform;
        battleCam.m_Priority = 1;
        //if (ungrey != null)
        //{
        //    ungrey();
        //}
    }
	private void AimBattleCam() {

        
        //if (Player.GetPlayer().BattleMode.EnemyTarget != null&&Player.GetPlayer().LockedOn) {
        //    //main.GetRig(1).LookAt = 
        //    main.m_LookAt=Player.GetPlayer().BattleMode.EnemyTarget.transform;
        //    //battleCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 7.5f+(Vector3.Distance(Player.GetPlayer().BattleMode.EnemyTarget.transform.position, Player.GetPlayer().transform.position)/2);
        //    //battleCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 10;
        //}
        //else {
        //    main.GetRig(1).LookAt = body.transform;
        //}
    }
	private void RetargetBattleCam() {
		battleCam.m_LookAt = Player.GetPlayer().transform;
        battleCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = Vector3.Distance(Player.GetPlayer().BattleMode.EnemyTarget.transform.position, Player.GetPlayer().transform.position);
	}
    private void MeditationCam(int muda) {
        meditationCam.Priority = 25;

    }
    #region TitleScreen cam
    private void TurnOffTitleScreenCam() {
        titleScreenCam.m_Priority = 0;
        titleScreenObjs.SetActive(false);
    }
    private void TurnTitleCamOn() {
        Debug.Log("TitleCamComesBackOn");
        titleScreenCam.m_Priority = 35;
        titleScreenObjs.SetActive(true);
    }

    #endregion
    private void CamTransitions() {
        StartCoroutine(WaitToSwitchCam());
    }
    private IEnumerator WaitToSwitchCam() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        WeakZendCam();
    }
    private void WeakZendCam() {
        deathCam.m_Priority = 0;
        pc.Weak=true;
    }
    private void TurnWeakZendCamOff() {
        weakZendCam.m_Priority = 0;
    }
    private void DeathCamUp() {
        deathCam.m_Priority = 990;

    }
    private void SwordsCam() {
        swordsCam.m_Priority = 30;
    }
    private void SwordsCamDown() {
        swordsCam.m_Priority = 0;
    }
}
