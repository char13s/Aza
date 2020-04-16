using UnityEngine;
using UnityEngine.Events;
public class GroundSound : MonoBehaviour
{
    [SerializeField] private AudioClip soundGround;
    

    public static event UnityAction<AudioClip> sendSound;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (sendSound != null) {
                sendSound(soundGround);
            }
        }
    }
}
