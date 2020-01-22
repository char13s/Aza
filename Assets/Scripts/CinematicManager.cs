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

    public bool CutscenePlaying { get => cutscenePlaying; set { cutscenePlaying = value;if (cutsceneIsPlaying != null) { cutsceneIsPlaying(); } } }

    public static event UnityAction dialogueUp;
	public static event UnityAction unfade;
    public static event UnityAction gameStart;
    public static event UnityAction cutsceneIsPlaying;
    public static event UnityAction cutsceneIsOver;
	private void Awake() {
		GameController.onNewGame += OpeningScene;
        gameStart += GameStart;
	}
	// Start is called before the first frame update
	void Start() {
        
	}

	// Update is called once per frame
	void Update() {

	}
	private void OpeningScene() {
		if (unfade != null) {
			unfade();
		}
        
        openingSceneCam.GetComponent<PlayableDirector>().Play();
        first.SetActive(true);
		if (skipOpening) {
			openingSceneCam.gameObject.SetActive(false);
			openingSceneCam2.gameObject.SetActive(false);
		} else {
			CutscenePlaying = true;
			StartCoroutine(WaitToSwitchCams(openingSceneCam, openingSceneCam2,1,null));
			StartCoroutine(WaitToSwitchCams(openingSceneCam2, main,0,gameStart));

		}

	}
    private void GameStart() {
        CutscenePlaying = false;
    }
    
	private IEnumerator WaitToSwitchCams(CinemachineVirtualCamera cam1, CinemachineVirtualCamera cam2,int cinemation,UnityAction action) {
		yield return new WaitUntil(() => cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 0.98f);
		Player.GetPlayer().Cinemations = cinemation;
        first.SetActive(false);
        cam1.Priority = 0;
		cam2.gameObject.SetActive(true);
        if (action != null) {
            action();
        }
	}
}
