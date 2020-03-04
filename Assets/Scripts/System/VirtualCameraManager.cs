using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class VirtualCameraManager : MonoBehaviour
{
    //[SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private CinemachineFreeLook main;
	[SerializeField] private CinemachineVirtualCamera battleCam;
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject body;
    public static event UnityAction grey;
    public static event UnityAction ungrey;
    // Start is called before the first frame update
    private void Awake()
    {
        //Player.lockOn += LookingForTarget;
       // Player.notAiming += NotAiming;
        UiManager.portal += ControlMainCam;
		PlayerBattleSceneMovement.onLockOn += AimBattleCam;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void LookingForTarget() {
        //main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 1;
        main.m_LookAt=lookAtTarget.transform;
        if (grey != null) {
            grey();
        }
    }
    private void NotAiming() {
        //main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 6;
        main.m_LookAt = Player.GetPlayer().transform;
        if (ungrey != null)
        {
            ungrey();
        }
    }
	private void AimBattleCam() {
        Debug.Log("cam target set");
        
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
}
