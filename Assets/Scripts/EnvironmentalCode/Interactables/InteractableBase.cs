using UnityEngine;
using UnityEngine.Events;
public abstract class InteractableBase : MonoBehaviour
{
    public static event UnityAction<int> interact;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (interact != null) {
            interact(2);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (interact != null) {
            interact(0);
        }
    }
    public abstract void Interact();
}
