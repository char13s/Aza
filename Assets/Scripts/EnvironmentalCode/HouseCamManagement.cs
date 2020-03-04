using UnityEngine;
//using UnityEngine.Events;
using Cinemachine;
public class HouseCamManagement : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private int priority;
    //public static event UnityAction 
   
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {

            cam.Priority = priority;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {

            cam.Priority = 0;
        }
    }

}
