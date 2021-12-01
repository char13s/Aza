using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private bool pause;
    public static event UnityAction<bool> pauseScreen;
    public static event UnityAction close;
    public static event UnityAction<int> switchMap;
    // Start is called before the first frame update
    void Start() {
        PlayerInputs.pause += PauseGame;
        //PauseCanvas.pause += PauseGame;
    }
    public void PauseGame() {
        if (pause) {
            pause = false;
            Time.timeScale = 1;
            switchMap.Invoke(0);
        }
        else {
            pause = true;
            Time.timeScale = 0;
            switchMap.Invoke(1);
            //close.Invoke();
        }
        pauseScreen.Invoke(pause);
    }
    public void QuitGame() {
        Application.Quit();
    }

}
