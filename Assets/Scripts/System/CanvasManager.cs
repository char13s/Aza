using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] protected GameObject canvas;
    // Start is called before the first frame update
    private void Start() {
        GameManager.close += CancelCanvas;
    }
    public virtual void CanvasControl(bool val) {
        canvas.SetActive(val);
    }
    public virtual void CancelCanvas() {
        CanvasControl(false);
    }
    public virtual void AssignCircle() {

    }
}
