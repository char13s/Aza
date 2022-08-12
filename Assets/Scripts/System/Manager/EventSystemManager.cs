using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EventSystemManager : MonoBehaviour
{
    // Start is called before the first frame updatew
    [SerializeField] private EventSystem eventSystem;
    private void Start() {
        CanvasManager.sendObj += SetPauseCanvasFirstSelected;
    }
    private void SetPauseCanvasFirstSelected(GameObject selectThis) {
        eventSystem.SetSelectedGameObject(selectThis);
    }

}
