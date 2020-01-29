using UnityEngine;
//using UnityEngine.Events;
using Cinemachine;
public class HouseCamManagement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    //public static event UnityAction 
   
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {

            cam.Priority = 20;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {

            cam.Priority = 0;
        }
    }

}
