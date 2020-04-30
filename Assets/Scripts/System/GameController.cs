using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.AI;
#pragma warning disable 0649
public class GameController : MonoBehaviour {
    //private Player pc;
    private PlayableAza aza;


    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject loadRespawn;
    [SerializeField] private GameObject normalCamera;
    [SerializeField] private Player pc;
    [Space]
    [SerializeField] private GameObject cinematicManager;
    [SerializeField]private GameObject spawn;
    [SerializeField] private GameObject forestSpawn;
    [Space]

    private int currentLevel;
    private int nextLevel;
    private bool load;
    private bool dontLoad;
    private static GameController instance;
    private Coroutine loadCoroutine;
    private Coroutine deadCoroutine;
    private int gameMode;

    public static event UnityAction onNewGame;
    public static event UnityAction onGameWasStarted;
    public static event UnityAction onQuitGame;
    public static event UnityAction onLoadGame;
    public static event UnityAction continueGame;
    public static event UnityAction update;
    public static event UnityAction awake;
    public static event UnityAction<int> titleScreen;
    public static event UnityAction gameWasSaved;
    public static event UnityAction onoLevelLoaded;
    public static event UnityAction respawn;
    public static event UnityAction setCanvas;
    public static event UnityAction readyDeathCam;
    public static event UnityAction returnToLevelSelect;
    // Start is called before the first frame update
    public static Player Zend => (instance == null) ? null : instance.pc;
    public static PlayableAza Aza => (instance == null) ? null : instance.aza;

    public GameObject Spawn { get => spawn; set => spawn = value; }
    public int GameMode { get => gameMode; set { gameMode = value;  }  }

    public int NextLevel { get => nextLevel; set => nextLevel = value; }

    public static GameController GetGameController() => instance.GetComponent<GameController>();
    public void Awake() {
        if (instance != null && instance != this) {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        SpawnSetters.setSpawner += SetSpawner;
        EventManager.setSpawner += SetSpawner;
        onNewGame += TheBeginningOfTheGame;      
    }

    void OnEnable() {
        //pc = Player.GetPlayer();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.sceneLoaded += SceneManagement;
        if (awake != null)
            awake();
        onNewGame += OnNewGame;

        UiManager.onNewGame += NewGame;
        UiManager.nextLevel += SetNextLevel;
        UiManager.load += LevelManagement;
        PortalConnector.backToLevelSelect += OnPlayerDeath;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }
    void Start() {
        
        Player.onPlayerDeath += OnPlayerDeath;

        EventManager.sceneChanger += SetNextLevel;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        

        //Input.Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {

        if (update != null)
            update();
        //SceneManagement();
        if (!Player.GetPlayer().Pause) { 
        if (Input.GetKey(KeyCode.F9)) {
            Time.timeScale = 4;
        }
        else {
            Time.timeScale = 1;
        }}
        //Debug.Log(SceneManager.);
    }
    private void SetSpawner(GameObject newSpawn) {
        spawn = newSpawn;
    }
    private void BackToLevelSelection() {
        StartCoroutine(WaitToUnload());
    }
    private IEnumerator WaitToUnload() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        
        SceneManager.UnloadSceneAsync(currentLevel);

    }
    private void ShowNavi() {

        //NavMeshTriangulation nav = NavMesh.CalculateTriangulation();
        //Mesh mesh = new Mesh();
        //mesh.vertices = nav.vertices;
        //mesh.triangles = nav.indices;
    }
    private void SetNextLevel(int nextLvl) {
        NextLevel = nextLvl;
    }
    private void LevelManagement() {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        Debug.Log(SceneManager.GetSceneByBuildIndex(0).name);

        //if (SaveLoad.DoesFileExist()) {
        //    StartCoroutine(WaitToLoadScene(2));
        //
        //}
        //else {
        //    StartCoroutine(WaitToLoadScene(2));
        //}
        StartCoroutine(WaitToLoadScene(nextLevel));
    }
    private IEnumerator WaitToLoadScene(int lvl) {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        if (lvl==4) {//write some bs to handle spawns based on what lvl yatta yatta 
            //SceneManager.UnloadSceneAsync(currentLevel);
            spawn = forestSpawn;
        }
        Debug.Log(lvl); if (!Player.GetPlayer().Dead) { 
        SceneManager.LoadSceneAsync(lvl,LoadSceneMode.Additive);
        GameMode = 1;
        currentLevel = lvl;
        if (respawn != null) {
                respawn();
        }
        }
        
    }
    private void SceneManagement(Scene scene, LoadSceneMode mode) {

        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) {
            Debug.Log("Scene 1 up");
            pc.gameObject.SetActive(false);
            
            //eventSystem.gameObject.SetActive(false);
        }
        else {
            
            //if (respawn != null) {
            //    respawn();
            //}
            pc.gameObject.SetActive(true);

            eventSystem.gameObject.SetActive(true);
        }
        //if (SceneManager.GetSceneByBuildIndex(2).isLoaded && instance != null) {
        //
        //    SceneManager.MoveGameObjectToScene(pc.gameObject, SceneManager.GetSceneByBuildIndex(0));
        //}


    }
    private void LevelLoader(int level) {
        switch (level) {
            case 2:
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                break;

        }
    }
    private void OnPlayerDead() {
        StartCoroutine(EndGame());
    }
    private void OnPlayerWin() {
        StartCoroutine(EndGame());
    }
    private IEnumerator EndGame() {
        YieldInstruction wait = new WaitForSeconds(2f);
        yield return wait;
        OnQuit();
    }
    private IEnumerator LoadCoroutine() {

        yield return null;

        StopCoroutine(loadCoroutine);

    }
    private IEnumerator DeadCoroutine() {
        yield return new WaitForSeconds(1.5f);
        //BackToMainMenu();

    }
    private void OnPlayerDeath() {
        //deadCoroutine = StartCoroutine(DeadCoroutine());
        StartCoroutine(LoadGameOverScreen());
    }

    private IEnumerator LoadGameOverScreen() {
        YieldInstruction wait = new WaitForSeconds(5);
        yield return wait;
        BackToLevelSelect();
    
    }
    private void BackToLevelSelect() {
        spawn = loadRespawn;
        GameMode = 0;
        if (currentLevel != 0) { 
        SceneManager.UnloadSceneAsync(currentLevel);}
        //if (respawn != null)
        //    respawn();
        if (returnToLevelSelect != null) {
            returnToLevelSelect();
        }

    }
    public void SaveGame() {
        SaveLoad.Save(instance.pc);
        if (gameWasSaved != null) {
            gameWasSaved();
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (onoLevelLoaded != null) {
            onoLevelLoaded();
        }
        if (titleScreen != null) {
            titleScreen(scene.buildIndex);
        }
        
        if (SceneManager.GetSceneByBuildIndex(nextLevel).isLoaded) {
            //CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextLevel));
            pc.Loaded = true;
            
            //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 0);

            foreach (GameObject b in pc.items.Buttons) {
                b.GetComponent<Items>().data.Quantity = 0;
            }

        }
        //normalCamera.transform.position = new Vector3(80.92751f, 8.582001f, -47.71f);

        Vector3 position;

        //pc.Pause = false;

        if (instance.load) {
            if (onLoadGame != null) {
                onLoadGame();
            }

            Game data = SaveLoad.Load();
            position.x = data.PlayerPosition[0];
            position.y = data.PlayerPosition[1];
            position.z = data.PlayerPosition[2];
            pc.transform.position = position;
            pc.stats = data.Stats;
            pc.items.Items = new List<ItemData>();
            foreach (GameObject b in pc.items.Buttons) {
                b.GetComponent<Items>().data.Quantity = 0;
            }
            if (onGameWasStarted != null) {
                onGameWasStarted();
                Debug.Log("Game was reloaded");
            }
            foreach (ItemData it in data.Items) {
                pc.items.Items.Add(it);
                pc.items.ButtonCreation(it);
                Debug.Log("ouchie ouch");
            }
            instance.load = false;
        }
    }
    private void NewGame() {
        StartGame();

        if (onGameWasStarted != null) {
            onGameWasStarted();
        }
        if (onNewGame != null)
            onNewGame();

    }
    private void TheBeginningOfTheGame() {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);//Cinematic Scene
        if (readyDeathCam != null) {
            readyDeathCam();
        }
    }
    private void OnNewGame() {
        //Vector3 position;
        pc.stats.Start();
        //position.x = 80.83f;
        //position.y = -1.918f;
        //position.z = -30.16f;
        //pc.Grounded = false;
        //normalCamera.transform.position = new Vector3(80.92751f, 8.582001f, -47.71f);
        //pc.transform.position = position;
        pc.transform.position = Spawn.transform.position;
        pc.transform.rotation = Spawn.transform.rotation;
        pc.items.Items = new List<ItemData>();
        Player.GetPlayer().Pause = false;
    }
    public void MenuLoadGame() {
        instance.load = true;
        StartGame();
    }
    public void ContinueGame() {
        if (continueGame != null) {
            continueGame();
        }
        Debug.Log("Continue");
        StartGame();
    }
    public void LoadGame() {

        
        pc.Pause = false;
        Game data = SaveLoad.Load();
        Vector3 position;
        position.x = data.PlayerPosition[0];
        position.y = data.PlayerPosition[1];
        position.z = data.PlayerPosition[2];
        pc.transform.position = position;
        pc.stats = data.Stats;
        pc.items.Items.Clear();

        foreach (GameObject b in pc.items.Buttons) {
            Destroy(b);
        }
        pc.items.Buttons.Clear();
        //pc.items.Items =;

        if (onGameWasStarted != null) {
            onGameWasStarted();

            Debug.Log("Game was reloaded");
        }
        foreach (ItemData it in data.Items) {

            pc.items.Items.Add(it);
            pc.items.ButtonCreation(it);
            Debug.Log("ouchie ouch");
        }
        Debug.Log(pc.items.Items.Count);
    }

    public void StartGame() {
        
        SceneManager.UnloadSceneAsync(1);
        if(setCanvas != null){
            setCanvas();
        }
        //SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);


    }

    public void BackToMainMenu() {
        GameMode = 0;
        if (onQuitGame != null) {
            onQuitGame();
        }
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded && !SceneManager.GetSceneByBuildIndex(1).isLoaded) {
            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        }
        pc.Loaded = false;
        pc.stats.Start();

    }

    public void OnQuit() {
        Application.Quit();
    }

}
