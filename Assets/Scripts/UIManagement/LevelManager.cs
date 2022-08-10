using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    public static event UnityAction off;
    public static event UnityAction sendToMain;
    public static event UnityAction<bool> levelFinished;
    public static event UnityAction<bool> levelTransition;
    public static event UnityAction<bool> gameMode;
    public static event UnityAction newScene;
    private int nextLevel;
    private int currentLevel;
    // Start is called before the first frame update
    void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }
    void Start() {
        nextLevel = 1;
        LevelTransition(1);
        LoadingCanvas.changeScene += WaitToChange;

    }
    public void LevelTransition(int lvl) {
        print("new level requested");
        if (lvl == 1) {
            //sendToMain.Invoke();
            gameMode.Invoke(false);
        }
        else {

        }

        off.Invoke();

        nextLevel = lvl;
        if (levelTransition != null) {
            levelTransition(false);
        }
    }
    private void WaitToChange() {
        if (nextLevel != 0) {
            print("new level cuz it wasnt 0");
            SceneManager.UnloadSceneAsync(currentLevel);
            currentLevel = nextLevel;
            SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);

        }


    }
    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1) {
        StartCoroutine(ResetActiveScene());
        if (levelFinished != null) {
            levelFinished(false);
        }
        StartCoroutine(waitForScene());
    }
    private IEnumerator ResetActiveScene() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        if (SceneManager.GetSceneByBuildIndex(currentLevel).isLoaded) {
            //CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevel));
        }
    }
    private IEnumerator waitForScene() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        if (levelTransition != null) {
            levelTransition(true);
        }
        gameMode.Invoke(true);
    }
    private void RepositionPlayer() { 
    
    }
}
