using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveThisObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }

    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1) {
        if (arg0.buildIndex > 1) { 
        SceneManager.MoveGameObjectToScene(gameObject, arg0);
        }
        print("Ran");
    }
}
