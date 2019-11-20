using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
#pragma warning disable 0649
public class GameController : MonoBehaviour
{
    private Player pc;
    private PlayableAza aza;
    [SerializeField] private GameObject ability;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject pocket;
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject normalCamera;
    [Space]
    
    [SerializeField] private GameObject spawn;
    [Space]
    private bool load;
    private static GameController instance;
    private Coroutine loadCoroutine;
    private Coroutine deadCoroutine;
    

    public static event UnityAction onNewGame;
    public static event UnityAction onGameWasStarted;
    public static event UnityAction onQuitGame;
    public static event UnityAction update;
    // Start is called before the first frame update
    public static Player Zend => (instance == null) ? null : instance.pc;
    public static PlayableAza Aza => (instance == null) ? null : instance.aza;

    

    public static GameController GetGameController() => instance.GetComponent<GameController>();
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        onNewGame += OnNewGame;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        onNewGame -= OnNewGame;
    }
    void Start()
    {
        Player.onPlayerDeath += OnPlayerDeath;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
        pc = Player.GetPlayer();
        
        //Input.Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (update != null)
            update();
        SceneManagement();
        
    }
    private void SceneManagement() {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            CameraLogic.Switchable = false;
            pc.gameObject.SetActive(false);
            
            eventSystem.gameObject.SetActive(false);
        }
        else {
            
            pc.gameObject.SetActive(true);
            
            eventSystem.gameObject.SetActive(true);
        }
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded && instance != null)
        {
            
            SceneManager.MoveGameObjectToScene(pc.gameObject, SceneManager.GetSceneByBuildIndex(0));
        }


    }
    private IEnumerator LoadCoroutine()
    {

        yield return null;
        
        StopCoroutine(loadCoroutine);

    }
    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        BackToMainMenu();
        
    }
    private void OnPlayerDeath()
    {
        deadCoroutine = StartCoroutine(DeadCoroutine());

    }
    public void Pocket()
    {
        pc.items.DisplayInventory();
        MenuOff();
    }
    public void Ability()
    {
        ability.SetActive(true);
        pc.stats.DisplayAbilities();
        MenuOff();
    }
    private void MenuOff()
    {
        close.SetActive(true);
        pauseMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(close, null);
    }
    private void MenuOn()
    {
        close.SetActive(false);
        pauseMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(pocket, null);
    }
    public void BackToMenu()
    {
        if (ability.activeSelf)
        {
            ability.SetActive(false);

            Debug.Log("ok");

        }
        else
        {
            pc.items.InventOff();
        }
        MenuOn();
    }
    public void UseItem()
    {
        
        
        
    }
    public void SaveGame() => SaveLoad.Save(instance.pc);
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            CameraLogic.Switchable = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
            pc.Loaded = true;
            
            //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 0);

            foreach (GameObject b in pc.items.Buttons)
            {
                b.GetComponent<Items>().data.Quantity = 0;
            }

        }
        normalCamera.transform.position = new Vector3(80.92751f, 8.582001f, -47.71f);

        Vector3 position;
        pauseMenu.SetActive(false);
        //pc.Pause = false;

        if (instance.load)
        {

            Game data = SaveLoad.Load();
            position.x = data.PlayerPosition[0];
            position.y = data.PlayerPosition[1];
            position.z = data.PlayerPosition[2];
            pc.transform.position = position;
            pc.stats = data.Stats;
            pc.items.Items = new List<ItemData>();
            foreach (GameObject b in pc.items.Buttons)
            {
                b.GetComponent<Items>().data.Quantity = 0;
            }
            if (onGameWasStarted != null)
            {
                onGameWasStarted();
                Debug.Log("Game was reloaded");
            }
            foreach (ItemData it in data.Items)
            {
                pc.items.Items.Add(it);
                pc.items.ButtonCreation(it);
                Debug.Log("ouchie ouch");
            }
            instance.load = false;
        }
    }
    public void NewGame()
    {
        StartGame();

        if (onGameWasStarted != null)
        {
            onGameWasStarted();
        }
        if (onNewGame != null)
            onNewGame();
    }
    void OnNewGame()
    {
        //Vector3 position;
        pc.stats.Start();
        //position.x = 80.83f;
        //position.y = -1.918f;
        //position.z = -30.16f;
        //pc.Grounded = false;
        normalCamera.transform.position = new Vector3(80.92751f, 8.582001f, -47.71f);
        //pc.transform.position = position;
        pc.transform.position = spawn.transform.position;
        pc.items.Items = new List<ItemData>();
        Player.GetPlayer().Pause = false;
    }
    public void MenuLoadGame()
    {
        instance.load = true;
        StartGame();
    }

    public void LoadGame()
    {

        pauseMenu.SetActive(false);
        pc.Pause = false;
        Game data = SaveLoad.Load();
        Vector3 position;
        position.x = data.PlayerPosition[0];
        position.y = data.PlayerPosition[1];
        position.z = data.PlayerPosition[2];
        pc.transform.position = position;
        pc.stats = data.Stats;
        pc.items.Items.Clear();

        foreach (GameObject b in pc.items.Buttons)
        {
            Destroy(b);
        }
        pc.items.Buttons.Clear();
        //pc.items.Items =;

        if (onGameWasStarted != null)
        {
            onGameWasStarted();
           
            Debug.Log("Game was reloaded");
        }
        foreach (ItemData it in data.Items)
        {

            pc.items.Items.Add(it);
            pc.items.ButtonCreation(it);
            Debug.Log("ouchie ouch");
        }
        Debug.Log(pc.items.Items.Count);
    }

    public void StartGame()
    {
        SceneManager.UnloadSceneAsync(1);

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);


    }
    
    public void BackToMainMenu()
    {
        
        if (onQuitGame != null)
        {
            onQuitGame();
        }
        if (SceneManager.GetSceneByBuildIndex(2).isLoaded && !SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        }
        pc.Loaded = false;
        pc.stats.Start();

    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
