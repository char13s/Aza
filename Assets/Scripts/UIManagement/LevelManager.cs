using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
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
        currentLevel = 1;
    }
    public void LevelTransition(int lvl) {
        if (currentLevel != 0) {
            SceneManager.UnloadSceneAsync(currentLevel);
        }
        currentLevel = lvl;
        SceneManager.LoadSceneAsync(lvl, LoadSceneMode.Additive);
    }
    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1) {
        StartCoroutine(ResetActiveScene());
    }
    private IEnumerator ResetActiveScene() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        if (SceneManager.GetSceneByBuildIndex(currentLevel).isLoaded) {
            //CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevel));
        }
    }
}
