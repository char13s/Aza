using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class VirtualCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera main;
	[SerializeField] private CinemachineVirtualCamera battleCam;
    [SerializeField] private GameObject lookAtTarget;

    public static event UnityAction grey;
    public static event UnityAction ungrey;
    // Start is called before the first frame update
    private void Awake()
    {
        Player.lockOn += LookingForTarget;
        Player.notAiming += NotAiming;
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
        main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 1;
        main.m_LookAt=lookAtTarget.transform;
        if (grey != null) {
            grey();
        }
    }
    private void NotAiming() {
        main.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 6;
        main.m_LookAt = Player.GetPlayer().transform;
        if (ungrey != null)
        {
            ungrey();
        }
    }
	private void AimBattleCam() {
		if (Player.GetPlayer().BattleMode.EnemyTarget!=null) {
			battleCam.m_LookAt = Player.GetPlayer().BattleMode.EnemyTarget.transform;
		}
	}
	private void RetargetBattleCam() {
		battleCam.m_LookAt = Player.GetPlayer().transform;
	}
}
