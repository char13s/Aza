using UnityEngine;
using UnityEngine.Events;

public class PortalConnector : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public static event UnityAction<int> portalListUp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            portal.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (Input.GetButtonDown("X")) {
                if (portalListUp != null) {
                    portalListUp(4);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            portal.SetActive(false);
        }
    }

}
