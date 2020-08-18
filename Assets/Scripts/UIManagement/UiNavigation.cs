using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
public class UiNavigation : MonoBehaviour
{
    public enum UIState {GameMode,Paused }
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private UIMenuElement[] pauseButtons;
    private int index;
    [SerializeField]private UIMenuElement[] currentList;
    private UIState state;
    private UIState prevState;
    private UIMenuElement current;
    private bool pressed;
    public UIState State { get => state; set { state = value;UpdatePrevState(); } }

    public UIMenuElement Current { get => current; set { current = value;current.OnSelected(); } }

    public int Index { get => index; set { index = value; UnselectFromList(); } }

    
    private void Awake() {
        State = UIState.GameMode;
    }
    void Start()
    {
        UiManager.pause += PauseState;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != UIState.GameMode) { 
        Inputs();}
        
    }
    private void PauseState() {
        Debug.Log("Pause Up");
        pauseMenu.SetActive(true);
        State = UIState.Paused;
        Current = pauseButtons[0];
    }
    private void UpdatePrevState() {
        Index = 0;
        switch (state) {
            case UIState.Paused:
                
                currentList = pauseButtons;

                prevState = UIState.GameMode;
                break;
        }
    }
    private void Inputs() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Debug.Log(y);
        if (y > 0 && !pressed) {
            Debug.Log("Up");
            pressed = true;
            MovingUp();
        }
        if (y < 0&&!pressed) {
            Debug.Log("Down");
            pressed = true;
            MovingDown();
        }
        if (y == 0 && pressed) {
            pressed = false;
        }
        if (Input.GetButtonDown("Circle")) {
            state = prevState;
        }
        if (Input.GetButtonDown("X")) {
            Current.Use();
        }
    }
    private void MovingDown() {
        if (index == currentList.Length-1) {
            Index = 0;
        }
        else {
            Index++;
        }

        Current = currentList[index];
    }
    private void MovingUp() {
        if (index == 0) {
            Index = currentList.Length-1;
        }
        else {
            Index--;
        }
        Current = currentList[index];
    }
    private void UnselectFromList() {
        foreach (UIMenuElement b in currentList) {
            b.UnSelect();
        }
    }
}
