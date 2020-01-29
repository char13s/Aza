using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.Playables;


public class CinematicManager : MonoBehaviour {
    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera openingSceneCam;
    [SerializeField] private CinemachineVirtualCamera openingSceneCam2;
    [SerializeField] private CinemachineVirtualCamera main;
    [Space]
    [Header("Scene Dialogue")]
    [SerializeField] private GameObject first;
    [SerializeField] private bool skipOpening;

    [SerializeField] private GroupsOfSpawners[] spawnGroups;
    private bool cutscenePlaying;

    public bool CutscenePlaying {
        get => cutscenePlaying; set {
            cutscenePlaying = value;
            if (cutscenePlaying) {
                if (cutsceneIsPlaying != null) {
                    cutsceneIsPlaying();
                }
            }
            if (!cutscenePlaying) {
                if (cutsceneIsOver != null) {
                    cutsceneIsOver();
                }
            }
        }
    }

    public static event UnityAction dialogueUp;
    public static event UnityAction unfade;
    public static event UnityAction gameStart;
    public static event UnityAction cutsceneIsPlaying;
    public static event UnityAction cutsceneIsOver;
    private void Awake() {
        GameController.onNewGame += FUckU;
        GameController.onNewGame += OpeningScene;



    }
    // Start is called before the first frame update
    void Start() {
        gameStart += GameStart;

    }

    // Update is called once per frame
    void Update() {
        if (CutscenePlaying) {
            if (Input.GetButton("Square")) {
                Time.timeScale = 2;
            }
            else {
                Time.timeScale = 1;
            }

        }

    }
    private void NewGameReset() {

        openingSceneCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;

        openingSceneCam.Priority = 11;
        openingSceneCam2.gameObject.SetActive(false);
        openingSceneCam2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        openingSceneCam2.Priority = 10;
    }
    private void FUckU() {
        Debug.Log("New game reset fired");

    }
    private void OpeningScene() {
        if (unfade != null) {
            unfade();
        }
        openingSceneCam.gameObject.SetActive(true);
        NewGameReset();
        Debug.Log("openingSceneFired");
        openingSceneCam.GetComponent<PlayableDirector>().Play();
        //first.SetActive(true);

        CutscenePlaying = true;
        Debug.Log("started coroutines");
        StartCoroutines();



    }
    private void StartCoroutines() {
        StartCoroutine(WaitToSwitchCams(openingSceneCam, openingSceneCam2, 1, null));
        StartCoroutine(WaitToSwitchCams(openingSceneCam2, main, 0, gameStart));
    }
    private void GameStart() {
        CutscenePlaying = false;
    }

    private IEnumerator WaitToSwitchCams(CinemachineVirtualCamera cam1, CinemachineVirtualCamera cam2, int cinemation, UnityAction action) {
        Debug.Log("started");
        yield return new WaitUntil(() => cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 0.98f);
        Debug.Log("done");
        Player.GetPlayer().Cinemations = cinemation;
        //first.SetActive(false);
        cam1.Priority = 0;

        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        if (action != null) {
            action();
        }
    }
}
