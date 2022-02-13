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

    [Header("Cinematic cams")]
    [SerializeField] private CinemachineVirtualCamera firstDemonSpawn;
    [SerializeField] private CinemachineVirtualCamera nextCam;
    [SerializeField] private CinemachineVirtualCamera dedCam;
    [Header("ObjectRefs")]
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject titleScreenObjs;
    [SerializeField] private GameObject aimingPoint;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject lightning;
    [Space]

    private Player pc;
    private bool freelook;
    private Vector3 currentEulerAngles;

    public bool Freelook { get => freelook; set { freelook = value; if (freelook) { freeCam.m_Priority = 12; } else { freeCam.m_Priority = 1; } } }

    public static event UnityAction spawnDemon;
    // Start is called before the first frame update
    private void Awake()
    {
        Player.playerIsLockedOn += LookingForTarget;
        Player.notAiming += NotAiming;

        //Player.onPlayerDeath += TurnTitleCamOn;
        UiManager.portal += ControlMainCam;



        ExpConverter.levelMenuUp += MeditationCam;
        LevelManager.off += TurnOffTitleScreenCam;
        GameController.onNewGame += TurnOffTitleScreenCam;
        GameController.respawn += TurnOffTitleScreenCam;
        GameController.returnToLevelSelect+=TurnTitleCamOn;
        GameController.readyDeathCam += DeathCamTransitions;


    }
    void Start()
    {
        pc = Player.GetPlayer();
        EventManager.endOfDeathIntro += WeakZendCamTransitions;
        EventManager.demonSpawning += FirstDemonSpawn;
        EventManager.demonWryed += DemonWryed;

        EventTrigger.chooseSword+=SwordsCam;
        UiManager.demonSword += SwordsCamDown;
        UiManager.angelSword += SwordsCamDown;
        UiManager.bothSwords += SwordsCamDown;
        EventManager.nextCam += NextCam;
        EventManager.demoRestart += RestartDemo;
       //EventManager.demoRestart += DedCamDown;
       //Player.Player
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
        
        battleCam.m_Priority = 24;
        
    }
    private void NotAiming() {
        battleCam.m_Priority = 1;
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
    private void DeathCamTransitions() {
        StartCoroutine(WaitToSwitchCam(1));
    }
    private void WeakZendCamTransitions() {
        StartCoroutine(WaitToSwitchCam(0));
    }
    private IEnumerator WaitToSwitchCam(int val) {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        switch (val) {
            case 0:
                WeakZendCam();
                break;
            case 1:
                DeathCamUp();
                break;
        }
        
    }
    private void WeakZendCam() {
        deathCam.m_Priority = 0;
        Debug.Log("wtf");
        pc.Weak=true;
    }
    private void TurnWeakZendCamOff() {
        weakZendCam.m_Priority = 0;
    }
    private void DeathCamUp() {
        deathCam.m_Priority = 990;
        deathCam.gameObject.GetComponent<SceneDialogue>().enabled = true;
    }
    private void SwordsCam() {
        swordsCam.m_Priority = 30;
        swordsCam.gameObject.GetComponent<SceneDialogue>().enabled = true;
    }
    private void SwordsCamDown() {
        swordsCam.m_Priority = 0;
        lightning.SetActive(false);
        fire.SetActive(false);
    }
    private void FirstDemonSpawn() {
        firstDemonSpawn.m_Priority = 100;
        if (spawnDemon != null) {
            spawnDemon();
        }
        firstDemonSpawn.gameObject.GetComponent<SceneDialogue>().enabled = true;
    }
    private void DemonWryed() {
        firstDemonSpawn.m_Priority = 0;

    }
    private void NextCam(int val) {
        if (val!=0){
           nextCam.gameObject.GetComponent<SceneDialogue>().enabled = true;

        }
        nextCam.Priority = val;
    }
    private void DedCamUp() {
        dedCam.Priority = 100;
    }
    private void DedCamDown() {
        dedCam.Priority = 0;
    }
    private void RestartDemo() {
        StartCoroutine(ReturnToTitleSoon());
    }
    private IEnumerator ReturnToTitleSoon() {
        YieldInstruction wait = new WaitForSeconds(2.5f);
        yield return wait;
        TurnTitleCamOn();
    }
    
}
