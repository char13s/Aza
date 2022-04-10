using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveThisObject : MonoBehaviour
{
    Scene master;
    // Start is called before the first frame update
    void Start()
    {
        master = SceneManager.GetSceneByBuildIndex(0);
        LevelManager.sendToMain += SendToMainScene;
    }
    void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }
    private void SendToMainScene() { 
        SceneManager.MoveGameObjectToScene(gameObject, master);
    }
    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1) {
        if (arg0.buildIndex > 1) { 
        SceneManager.MoveGameObjectToScene(gameObject, arg0);
        }
        //if (arg0.buildIndex == 1) {
        //    SendToMainScene();
        //}

    }
}
