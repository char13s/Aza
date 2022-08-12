using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] protected GameObject canvas;
    [SerializeField] private GameObject firstOnMenu;
    public static event UnityAction<GameObject> sendObj;
    // Start is called before the first frame update
    private void Start() {
    }
    public virtual void CanvasControl(bool val) {
        canvas.SetActive(val);
        if (val == true) {
            sendObj.Invoke(firstOnMenu);
            AssignButtons();
        }
        else {
            UnAssignButtons();
        }
    }
    public virtual void CancelCanvas() {
        CanvasControl(false);
    }
    public virtual void AssignCircle() {
        //close this Canvas and go back to main pause menu

    }
    public virtual void AssignButtons() { 

    }
    public virtual void UnAssignButtons() {

    }
}
