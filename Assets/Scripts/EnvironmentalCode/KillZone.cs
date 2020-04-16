using UnityEngine;
using UnityEngine.Events;
public class KillZone : MonoBehaviour
{
    public static event UnityAction respawn;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (respawn != null) {
                respawn();
            }

        }
    }
}
