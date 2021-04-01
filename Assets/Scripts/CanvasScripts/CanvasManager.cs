using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject saveCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void PlayerCanvas(bool val) {
        playerCanvas.SetActive(val);
    }
    private void PauseCanvas(bool val) {
        pauseCanvas.SetActive(val);
    }
    private void SaveCanvas(bool val) {
        saveCanvas.SetActive(val);
    }
}
