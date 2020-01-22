using UnityEngine;
using UnityEngine.Events;

public class PortalConnector : MonoBehaviour
{
    public static event UnityAction<int> portalListUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
