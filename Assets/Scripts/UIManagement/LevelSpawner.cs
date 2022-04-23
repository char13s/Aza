using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Spawn;

    /*void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }

    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1) {
        
        print("Ran");
    }*/
    private void Start() {
        Player.GetPlayer().InTeleport = true;
        Player.GetPlayer().transform.position = Spawn.transform.position;
        //Player.GetPlayer().InTeleport = false;
    }
}
