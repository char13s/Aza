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
    [Space]
    [Header("ObjectRefs")]
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject titleScreenObjs;
    [SerializeField] private GameObject aimingPoint;
    [Space]
    
    private bool freelook;
    private Vector3 currentEulerAngles;

    public bool Freelook { get => freelook; set { freelook = value; if (freelook) { freeCam.m_Priority = 12; } else { freeCam.m_Priority = 1; } } }

    public static event UnityAction grey;
    public static event UnityAction ungrey;
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
    }
    void Start()
    {
        aimingPoint = Player.GetPlayer().AimmingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("R3")) {

            FreeLookControls();
        }
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
        if (Player.GetPlayer().BattleMode.EnemyTarget != null) {
            battleCam.m_LookAt = Player.GetPlayer().BattleMode.EnemyTarget.transform;
        }
        else {
            battleCam.m_LookAt = aimingPoint.transform;
        }

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
    private void TurnOffTitleScreenCam() {
        titleScreenCam.m_Priority = 0;
        titleScreenObjs.SetActive(false);
    }
    private void TurnTitleCamOn() {
        Debug.Log("TitleCamComesBackOn");
        titleScreenCam.m_Priority = 35;
        titleScreenObjs.SetActive(true);
    }
}
