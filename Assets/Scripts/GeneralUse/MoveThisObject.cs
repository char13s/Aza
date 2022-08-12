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
        
    }
    void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        LevelManager.sendToMain += SendToMainScene;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        LevelManager.sendToMain -= SendToMainScene;
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
