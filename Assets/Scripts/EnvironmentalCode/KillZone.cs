using UnityEngine;
using UnityEngine.Events;
public class KillZone : MonoBehaviour
{
    public static event UnityAction respawn;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (respawn != null) {
                respawn();
            }

        }
        if (other.GetComponent<Enemy>()) {
            Destroy(other);
        }
    }
}
