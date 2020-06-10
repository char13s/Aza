using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            portal.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            portal.SetActive(false);
        }
    }
}
