using UnityEngine;
//using UnityEngine.Events;
using Cinemachine;
public class HouseCamManagement : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private int priority;
    [SerializeField] private bool done;
    private Player pc;
    //public static event UnityAction 

    private void Start() {
        pc = Player.GetPlayer();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")&&!done) {
            
            cam.Priority = priority;
            if (cam.gameObject.GetComponent<SceneDialogue>() != null) {
                cam.gameObject.GetComponent<SceneDialogue>().enabled = true;
               // pc.InputSealed = true;
               // pc.Animations = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
           // pc.InputSealed = false ;
            cam.Priority = 0;
            done = true;
        }
    }

}
