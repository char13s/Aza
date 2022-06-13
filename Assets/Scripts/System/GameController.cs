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
    [SerializeField] private GameObject player;
    [Space]
    [SerializeField] private GameObject cinematicManager;
    [SerializeField]private GameObject spawn;
    [SerializeField] private GameObject forestSpawn;
    [SerializeField] private GameObject demoSpawn;

    [Space]

    private int currentLevel;
    private int nextLevel;
    private bool load;
    private bool dontLoad;
    private static GameController instance;
    private Coroutine loadCoroutine;
    private Coroutine deadCoroutine;
    private int gameMode;
    List<Scene> openScenes = new List<Scene>();

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
    public static event UnityAction returnToSpawn;
    // Start is called before the first frame update
    public static Player Zend => (instance == null) ? null : instance.pc;
    public static PlayableAza Aza => (instance == null) ? null : instance.aza;

    public GameObject Spawn { get => spawn; set => spawn = value; }
    public int GameMode { get => gameMode; set { gameMode = value;  }  }

    public int NextLevel { get => nextLevel; set => nextLevel = value; }
    public GameObject Player { get => player; set => player = value; }

    public static GameController GetGameController() => instance.GetComponent<GameController>();
    public void Awake() {
        if (instance != null && instance != this) {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        LevelObject.setSpawn += SetSpawner;
        SpawnSetters.setSpawner += SetSpawner;
        EventManager.setSpawner += SetSpawner;
        
        //onNewGame += TheBeginningOfTheGame;      
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
        EventManager.demoRestart += DemoRestart;
        EventManager.unloadDeathScene += UnloadDeathScene;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //onNewGame -= OnNewGame;
    }
    void Start() {
        
        global::Player.onPlayerDeath += OnPlayerDead;
        SpawnSetters.saveGame += SaveGame;
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
       // if (!global::Player.GetPlayer().Pause) { 
        //if (Input.GetKey(KeyCode.F9)) {
        //    Time.timeScale = 4;
       // }
        //Debug.Log(SceneManager.);
    }
    private void SetSpawner(GameObject newSpawn) {
        Spawn = newSpawn;
    }
    private void BackToLevelSelection() {
        StartCoroutine(WaitToUnload());
    }
    private IEnumerator WaitToUnload() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        SceneManager.UnloadSceneAsync(currentLevel);

    }
    private void DemoRestart() {
       // Application.Quit();
        StartCoroutine(WaitToUnloadDemo());
    }
    private IEnumerator WaitToUnloadDemo() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        if(SceneManager.GetSceneByBuildIndex(4).isLoaded) {
            SceneManager.UnloadSceneAsync(4);
        }
       
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
            //SceneManager.UnloadSceneAsync(4);
            spawn = demoSpawn;
        }
        
        Debug.Log(lvl);
        SceneManager.LoadSceneAsync(lvl,LoadSceneMode.Additive);
        //if (lvl > 1&&SceneManager.GetSceneByBuildIndex(lvl-1).isLoaded) {
        //    SceneManager.UnloadSceneAsync(lvl - 1);
        //}

        foreach (Scene level in openScenes) {

            SceneManager.UnloadSceneAsync(level);
        }
        openScenes.Clear();
            //SceneManager.UnloadSceneAsync(lvl-1);
        GameMode = 1;
        currentLevel = lvl;
        if (respawn != null) {
                respawn();
        }
        StartCoroutine(ResetActiveScene());
        
    }
    private void UnloadDeathScene() {
        SceneManager.UnloadSceneAsync(5);
    }
    private void SceneManagement(Scene scene, LoadSceneMode mode) {

        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) {

            //pc.gameObject.SetActive(false);
            
            //eventSystem.gameObject.SetActive(false);
        }
        else {
            
            //if (respawn != null) {
            //    respawn();
            //}
            

            eventSystem.gameObject.SetActive(true);
        }
        if (currentLevel > 2&& currentLevel!=5) {
            pc.gameObject.SetActive(true);
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
        if (returnToSpawn != null) {
            returnToSpawn();
        }
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
        yield return new WaitForSeconds(2f);
        BackToMainMenu();

    }
    private void OnPlayerDeath() {
        //deadCoroutine = StartCoroutine(DeadCoroutine());
        StartCoroutine(LoadGameOverScreen());
        Debug.Log("The player has died");
    }

    private IEnumerator LoadGameOverScreen() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        SceneManager.UnloadSceneAsync(4);
    
    SetNextLevel(5);
        LevelManagement();
        pc.gameObject.SetActive(false);
       
    
    }
    private void BackToLevelSelect() {
        spawn = loadRespawn;
        GameMode = 0;
        if (returnToLevelSelect != null) {
            returnToLevelSelect();
        }

    }
    private void SaveGame() {
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
        
        if (scene.buildIndex > 1){
            openScenes.Add(scene);
        }
        if (SceneManager.GetSceneByBuildIndex(nextLevel).isLoaded) {
            //CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextLevel));
            pc.Loaded = true;
            foreach (GameObject b in pc.items.Buttons) {
                b.GetComponent<Items>().data.Quantity = 0;
            }

        }
        if (instance.load) {
            if (onLoadGame != null) {
                onLoadGame();
            }
            
            if (onGameWasStarted != null) {
                onGameWasStarted();
                Debug.Log("Game was reloaded");
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
    private IEnumerator ResetActiveScene() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        if (SceneManager.GetSceneByBuildIndex(nextLevel).isLoaded) {
            //CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextLevel));
        }
    }
    private void OnNewGame() {

        pc.stats.Start();
   
        pc.transform.position = Spawn.transform.position;
        pc.transform.rotation = Spawn.transform.rotation;
        pc.items.Items = new List<ItemData>();
        global::Player.GetPlayer().Pause = false;
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
    private void LoadShit() {
        Game data = SaveLoad.Load();
        pc.transform.position = new Vector3(data.Position[0],data.Position[1],data.Position[2]);
        pc.stats = data.Stats;
        pc.items.Items = new List<ItemData>();
        foreach (ItemData it in data.Items) {
            pc.items.Items.Add(it);
            pc.items.ButtonCreation(it);
            Debug.Log("ouchie ouch");
        }
        foreach (ItemData it in data.Items) {
            pc.items.Items.Add(it);
            pc.items.ButtonCreation(it);
            Debug.Log("ouchie ouch");
        }

        foreach (GameObject b in pc.items.Buttons) {
            b.GetComponent<Items>().data.Quantity = 0;
        }
    }
    public void LoadGame() {
        pc.Pause = false;
        //Game data = SaveLoad.Load();
        LoadShit();
        //foreach (GameObject b in pc.items.Buttons) {
        //    Destroy(b);
        //}
        //pc.items.Buttons.Clear();
        //pc.items.Items =;

        if (onGameWasStarted != null) {
            onGameWasStarted();

            Debug.Log("Game was reloaded");
        }
        //foreach (ItemData it in data.Items) {
        //
        //    pc.items.Items.Add(it);
        //    pc.items.ButtonCreation(it);
        //    Debug.Log("ouchie ouch");
        //}
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
