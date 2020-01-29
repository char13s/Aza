using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private GameObject darkEnd;
    [SerializeField] private GameObject azaHouse;
    // Start is called before the first frame update
    private void Awake() {
        //UiManager.portal += AreaControl;
    }
    
    private void AreaControl(int area) {
        azaHouse.SetActive(false);
        darkEnd.SetActive(false);
        switch (area) {
            case 0:
                azaHouse.SetActive(true);
                break;
            case 1:
                darkEnd.SetActive(true);
                break;
        }
    }
}
