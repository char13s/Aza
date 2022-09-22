using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    public static event UnityAction off;
    public static event UnityAction sendToMain;
    public static event UnityAction turnOnMain;
    public static event UnityAction<bool> levelFinished;
    public static event UnityAction<bool> levelTransition;
    public static event UnityAction<bool> gameMode;
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
        GameStart();
        LoadingCanvas.changeScene += WaitToChange;
        GameManager.gameOver += GameStart;
        EndOfPortal.loadInLevel += ChangeSceneDiscretely;
        SwitchToFallGame.unloadLevel += UnLoadSceneDiscretely;
        VoidSettings.resetVoid += ResetLevel;
    }
    private void GameStart() {
        LevelTransition(1);
    }
    public void LevelTransition(int lvl) {
        if (lvl == 1) {
            sendToMain.Invoke();
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
    private void ChangeSceneDiscretely(int val) {
        
        currentLevel = val;
        SceneManager.LoadSceneAsync(val, LoadSceneMode.Additive);
        
    }
    private void UnLoadSceneDiscretely(int val) {
        RepositionPlayer();
        SceneManager.UnloadSceneAsync(val);
    }
    private void WaitToChange() {
        if (nextLevel == 1) {
            turnOnMain.Invoke();
        }
        if (nextLevel != 0) {
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
        //if (levelTransition != null) {
        //    levelTransition(true);
        //}
        if(currentLevel>1)
            gameMode.Invoke(true);
    }
    private void RepositionPlayer() {
        if(Player.GetPlayer().gameObject!=null)
            SceneManager.MoveGameObjectToScene(Player.GetPlayer().gameObject, SceneManager.GetSceneByBuildIndex(currentLevel));
    }
    public void ResetLevel() {
        LevelTransition(currentLevel);
    }
}
