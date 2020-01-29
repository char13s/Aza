using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class VirtualCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private GameObject lookAtTarget;

    public static event UnityAction grey;
    public static event UnityAction ungrey;
    // Start is called before the first frame update
    private void Awake()
    {
        Player.lockOn += LookingForTarget;
        Player.notAiming += NotAiming;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
