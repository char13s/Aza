using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject equipmentCanvas;
    private int page;

    public int Page { get => page; set { page = Mathf.Clamp(value,0,1);ChooseCanvas(); } }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputs.turnPage += TurnPage;
    }
    private void InitialPage() {
        Page = 0;
    }
    private void TurnPage(int val) {
        Page += val;
        print("Page has been turnt");
    }
    private void ChooseCanvas() {
        CloseCanvases();
        switch (Page) {
            case 0:
                pauseCanvas.gameObject.SetActive(true);
                break;
            case 1:
                equipmentCanvas.gameObject.SetActive(true);
                break;
            default:
                print("fuck u UI shit");
                break;
        }
    }
    private void CloseCanvases() {
        pauseCanvas.gameObject.SetActive(false);
        equipmentCanvas.gameObject.SetActive(false);
    }
}
