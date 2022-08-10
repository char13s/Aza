using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool pause;
    public static event UnityAction<bool> pauseScreen;
    public static event UnityAction<int> switchMap;
    [SerializeField] private GameObject camera;
    private int orbAmt;
    public enum GameState {Paused, PlayMode }
    private GameState currentState;

    public GameState CurrentState { get => currentState; set => currentState = value; }
    public GameObject Camera { get => camera; set => camera = value; }
    public int OrbAmt { get => orbAmt; set => orbAmt = value; }

    public static GameManager GetManager() => instance;
    // Start is called before the first frame update
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        CurrentState = GameState.Paused;
    }
    void Start() {
        PlayerInputs.pause += PauseGame;
        LevelManager.gameMode += GameStateControl;
        Stats.onOrbGain += Collect;
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
    private void GameStateControl(bool val) {
        if (val) {
            CurrentState = GameState.PlayMode;
        }
        else {
            CurrentState = GameState.Paused;
        }
    }
    private void Collect(int amt) {
        orbAmt += amt;
    }
    public void QuitGame() {
        Application.Quit();
    }

}
