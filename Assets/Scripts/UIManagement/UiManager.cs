using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
#pragma warning disable 0649
public class UiManager : MonoBehaviour {
	[Header("Tutorial Stuff")]
	[SerializeField] private GameObject movementTutorial;
	[SerializeField] private GameObject miniMapTutorial;
	[SerializeField] private GameObject pauseTutorial;
	[SerializeField] private GameObject combatTutorial;
	[SerializeField] private GameObject background;
	[SerializeField] private GameObject tutorMenu;
	[SerializeField] private GameObject backButton;
	[SerializeField] private GameObject fireBallTutorial;
	[SerializeField] private GameObject flameTornadoTutorial;
	[SerializeField] private GameObject HeavySwingTutorial;
	[Space]
	#region PlayerUI
	[Header("PlayerUI")]
	[SerializeField] private GameObject playerUi;
	[SerializeField] private Image black;
	[SerializeField] private GameObject miniMap;
	[SerializeField] private Text exp;
	[SerializeField] private Text level;
	[SerializeField] private Text health;
	[SerializeField] private Text money;
	[SerializeField] private GameObject abilities;
	[SerializeField] private Text stamina;
	[SerializeField] private Slider healthBar;
	[SerializeField] private Slider staminaBar;
	[SerializeField] private Slider expBar;
	[SerializeField] private GameObject abilityClose;
	[Space]
	#endregion

	#region Abilities
	[Header("Abilities")]
	[SerializeField] private Text attack;
	[SerializeField] private Text defense;
	[SerializeField] private Text intelligence;
	[SerializeField] private Text healthAb;
	[SerializeField] private Text staminaAb;
	[SerializeField] private Text mpBoost;
	[SerializeField] private Text attackBoost;
	[SerializeField] private Text defenseBoost;
	[SerializeField] private Text healthBoost;
	[SerializeField] private GameObject mainMenuCanvas;
	[SerializeField] private GameObject mainCanvas;
	[SerializeField] private GameObject pauseMenu;

	[Space]
	#endregion
	#region EventSystems
	[Header("EventSystems")]
	[SerializeField] private GameObject mainEventSystem;
	[SerializeField] private GameObject mainMenuEventSystem;
	[Space]
	#endregion


	[Space]

	[Header("StoreMenu")]
	[SerializeField] private GameObject StoreMenuPrefab;
	private static GameObject storeMenu;
	[SerializeField] private GameObject storeMenuDefaultButton;
	[SerializeField] private GameObject quantityWindow;
	[SerializeField] private Text displayedQuantity;
	private int quantity;
	[Space]
	[Header("UseMenu")]
	[SerializeField] private GameObject useMenuPrefab;
	[SerializeField] private Button useButtonPrefab;
	[SerializeField] private Button itemDescriptionButtonPrefab;
	[SerializeField] private Button giveButtonPrefab;
	[SerializeField] private Button dropButtonPrefab;
	[SerializeField] private GameObject useMenuDefaultButton;
	private static GameObject useMenu;
	private static Button useButton;
	private static Button itemDescriptionButton;
	private static Button giveButton;
	private static Button dropButton;
	[Space]



	[Space]
	[Header("Dialogue Management")]
	private static GameObject dialogueMenu;
	[SerializeField] private GameObject dialogueMenuPrefab;
	[SerializeField] private Text dialogueText;
	[SerializeField] private Text whoseTalking;
	[Space]
	#region StatBuildMenu
	[Header("StatBuildMenu")]
	[SerializeField] private GameObject levelMenuPrefab;
	[SerializeField] private Text baseAttack;
	[SerializeField] private Text baseDefense;
	[SerializeField] private Text baseMp;
	[SerializeField] private Text baseHealth;
	[SerializeField] private Text expRequired;
	[SerializeField] private Text lvMenuExp;
	[SerializeField] private Text abilityPoints;
	[SerializeField] private Text abilityPointsCost;
	[SerializeField] private Text itemAbilityPointsCost;
	[SerializeField] private GameObject levelMenuDefaultButton;
	[Space]
	#endregion
	#region Equipment Window
	[Header("EquipmentWindow")]

	[SerializeField] private Text healthDisplay;
	[SerializeField] private Text attackDisplay;
	[SerializeField] private Text defenseDisplay;
	[SerializeField] private Text mpDisplay;
	[SerializeField] private GameObject spellTags;
	private SpellTagSlot lastSpellSlotSelected;

	[Space]
	#endregion
	[Header("OLDPauseMenu")]
	[SerializeField] private GameObject invent;

	//[SerializeField] private GameObject options;
	[SerializeField] private GameObject objectiveMenu;
	[SerializeField] private GameObject statusWindow;

	[Space]
	[Header("Videos")]
	[SerializeField] private VideoClip combo1;
	[SerializeField] private VideoClip combo2;
	[SerializeField] private VideoClip combo3;
	[SerializeField] private VideoClip combo4;


	[Space]
	[Header("Objective menu")]
	[SerializeField] private Text descriptionBox;
	[SerializeField] private GameObject missionListing;
	[SerializeField] private Button activeQuest;
	[SerializeField] private Button completedQuest;
	private List<Objective> objectives = new List<Objective>();

	[Header("Options")]
	[SerializeField] private GameObject optDefaultButton;
	[Header("Pop Up Windows")]
	[SerializeField] private GameObject newObjectiveWindow;
	[SerializeField] private GameObject objectiveClear;
	[SerializeField] private GameObject objectiveUpdated;
	[SerializeField] private GameObject savedGame;
	[SerializeField] private GameObject loadedGame;
	[SerializeField] private GameObject defeated;
	[SerializeField] private GameObject loadingIcon;
	[Header("ItemObtainedPopWindow")]
	[SerializeField] private GameObject itemWindow;
	[SerializeField] private Image imageWindow;
	[SerializeField] private Text itemDescrp;
	[Space]
	[Header("PopUpTutorials")]
	[SerializeField] private GameObject howToAttack;
	[SerializeField] private GameObject howToGuard;
	[SerializeField] private GameObject howToRoll;
	[Header("Fonts")]
	[SerializeField] private Font luckiestGuy;
	[Header("KryllUI")]
	[SerializeField] private GameObject kryllUi;
	[SerializeField] private Text distFromZend;
	[Header("Save Menu")]
	[SerializeField] private GameObject saveMenu;
	[Header("Pause Menu")]
	[SerializeField] private GameObject menus;
	[SerializeField] private GameObject items;
	[SerializeField] private GameObject equipment;
	[SerializeField] private GameObject skills;
	[SerializeField] private GameObject stats;
	[SerializeField] private GameObject options;
	[SerializeField] private GameObject pauseMenuDefaultButton;
	private static UiManager instance;
	[Header("Skill Menu")]
	[SerializeField] private GameObject skillList;
	private SkillButton lastSelectedSkillSlot;

	[Header("Items")]
	[SerializeField] private GameObject itemInvent;
	[SerializeField] private GameObject portalList;
	[Header("Spell Tag Menu")]
	[SerializeField] private GameObject spellTagList;
	[Header("Stats Menu")]
	[SerializeField] private Text swordLevel;
	[SerializeField] private Text swordProficency;
	[Header("Title Screen Menu")]
	[SerializeField] private GameObject newGameButton;
	//[SerializeField]private GameObject quit
    //Events
    public static UnityAction missionCleared;
    public static event UnityAction sealPlayerInput;
    public static event UnityAction unsealPlayerInput;
    public static UnityAction<string, Sprite> itemAdded;
    public static UnityAction<Vector3> areaChange;
    public static UnityAction<int> portal;
    public static UnityAction nullEnemies;
    [SerializeField] private GameObject defaultObject;
    [SerializeField] private GameObject inventDefaultButton;
    private int menuState;
    #region Getters and Setters
    public static GameObject UseMenu { get => useMenu; set => useMenu = value; }
    public static Button UseButton { get => useButton; set => useButton = value; }
    public static Button ItemDescriptionButton { get => itemDescriptionButton; set => itemDescriptionButton = value; }
    public static Button GiveButton { get => giveButton; set => giveButton = value; }
    public static Button DropButton { get => dropButton; set => dropButton = value; }

    public static GameObject StoreMenu { get => storeMenu; set => storeMenu = value; }

    public Image Black { get => black; set => black = value; }
    public Text DialogueText { get => dialogueText; set => dialogueText = value; }
    public static GameObject DialogueMenu { get => dialogueMenu; set => dialogueMenu = value; }
    public GameObject DefaultObject { get => defaultObject; set { defaultObject = value; GetSelected(); } }


    public GameObject Invent { get => invent; set => invent = value; }

    public Text DescriptionBox { get => descriptionBox; set => descriptionBox = value; }
    public Font LuckiestGuy { get => luckiestGuy; set => luckiestGuy = value; }

    public GameObject MissionListing { get => missionListing; set => missionListing = value; }

    public int MenuState { get => menuState; set { menuState = value; } }

    public GameObject ItemInvent { get => itemInvent; set => itemInvent = value; }
    #endregion


    //public static event UnityAction movementTutorialActive;
    //public static event UnityAction miniMapTutorialActive;
    //public static event UnityAction pauseTutorialActive;
    //public static event UnityAction combatTutorialActive;
    //public static GameObject GetUseMenu() => useMenu;
    public static UiManager GetUiManager() => instance;
    public void Awake() {
        if (instance != null && instance != this) {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        black.gameObject.SetActive(true);
        UseMenu = useMenuPrefab;
        UseButton = useButtonPrefab;
        GiveButton = giveButtonPrefab;
        itemDescriptionButton = itemDescriptionButtonPrefab;
        dropButton = dropButtonPrefab;

        storeMenu = StoreMenuPrefab;

        dialogueMenu = dialogueMenuPrefab;

        AIKryll.sendDist += DistFromKyrllToZend;
        AIKryll.zend += KryllDown;
        Player.kryll += KryllUp;
        Player.notSleeping += SaveMenuDown;
        Player.cancelPaused += MenusDown;
        StoreManager.itemWasBought += UpdateMoney;
        GameController.onGameWasStarted += GameScreen;
        Npc.dialogueUp += DialogueManagerUp;
        Npc.dialogueDown += DialogueManagerDown;
        ExpConverter.levelMenuUp += MenuManager;
        Stats.onBaseStatsUpdate += UpdateBoost;
        Items.onItemClick += UseMenuHandling;
        Objective.onObjectiveClick += ObjectiveDescription;
        Bed.bed += SaveMenuUp;
        PortalConnector.portalListUp += MenuManager;
        GameController.gameWasSaved += SaveGame;
        GameController.onQuitGame += OnQuit;
        missionCleared += ObjectiveClear;
        itemAdded += ItemPopUp;
        areaChange += AreaChange;
        Skill.sendSkill += SetSkillToSlot;
        SkillButton.sendSkillSlot += SetLastSkillSlot;
        CinematicManager.unfade += UnFade;
        CinematicManager.gameStart += GameStart;
        Cauldron.potionMaking += MenuManager;
        SpellTagSlot.spellInvent += SpellTagListUp;
        SpellTag.spellListDown += SpellTagListDown;
        SpellTagSlot.sendThisSlot += SetLastSelectedSpellTagSlot;
        SpellTag.sendThisSpell += SetSpell;
    }
    void Start() {

        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onMPLeft += MPChange;
        Stats.onHealthChange += HealthChange;
        Enemy.onAnyEnemyDead += EnemyDeath;
		DefaultObject = newGameButton;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    void OnDisable() {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
            GetSelected();
        }
        if (Player.GetPlayer().Pause) {
            if (MenuState > 0 && Input.GetButtonDown("Circle")) {
                StartCoroutine(WaitForPauseMenu());

            }

        }
        if (Input.GetButtonDown("Pause")) {
            PauseMenuControl(0);

        }


        //if (missionListing.transform.childCount > 0) { 
        //ObjectiveDescription(missionListing.transform.GetChild(0).GetComponent<Objective>().Description[missionListing.transform.GetChild(0).GetComponent<Objective>().CurrentDescription]);
        //}SetCanvas();
        CancelMenu();
    }
    private IEnumerator WaitForPauseMenu() {
        yield return null;
        PauseMenuControl(0);

    }
    private IEnumerator JustWait(int connector) {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        if (portal != null) {
            portal(connector);
        }
        MenusDown();
        StartCoroutine(WaitToLoad());
    }
    public void Portal(int connector) {
        
        portalList.SetActive(false);
        StartCoroutine(JustWait(connector));
        StartFade();
        
    }
    private IEnumerator WaitToLoad() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        UnFade();
    }
    #region Event Handlers
    private void SetSkillToSlot(Skill skill) {
        lastSelectedSkillSlot.SkillAssigned = skill;
        skillList.SetActive(false);
    }
    private void SetLastSkillSlot(SkillButton slot) {
        lastSelectedSkillSlot = slot;
        skillList.SetActive(true);

    }
    //private void DialogueManagement(string lines) {
    //dialogueText.text = lines;
    //}
    #endregion
    #region Menus
    public void StoreUp() {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        storeMenu.SetActive(true);
    }
    public void PauseMenuControl(int num) {
        MenuState = num;
        menus.SetActive(false);

        switch (MenuState) {
            case 0:
                Debug.Log("back to basics");

                menus.SetActive(true);
                DefaultObject = pauseMenuDefaultButton;

                items.SetActive(false);
                equipment.SetActive(false);
                skills.SetActive(false);
                stats.SetActive(false);
                options.SetActive(false);
                break;
            case 1:
                items.SetActive(true);
                break;
            case 2:
                equipment.SetActive(true);
                break;
            case 3:

                skills.SetActive(true);
                break;
            case 4:
                stats.SetActive(true);
                ViewStats();
                break;
            case 5:
                options.SetActive(true);
                break;
        }

    }
    private void GameStart() {
        StartCoroutine(WaitToShowHowToAttack());

    }
    private IEnumerator WaitToShowHowToAttack() {
        YieldInstruction wait = new WaitForSeconds(1.3f);
        yield return wait;
        PauseGame();
        howToAttack.SetActive(true);

    }
    private void MenuManager(int menu) {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        PauseGame();
        switch (menu) {
            case 0:
                break;
            case 1:
                storeMenu.SetActive(true);
                break;
            case 2:
                LevelUpMenuUp();
                break;
            case 3:
                quantityWindow.SetActive(true);
                displayedQuantity.text = quantity.ToString();
                break;
            case 4:
                portalList.SetActive(true);
                break;
        }
    }
    private void OnQuit() {
        MenuManager(0);
        MenusDown();
    }


    #endregion
    private void CancelMenu() {
        if (howToAttack.activeSelf) {
            if (Input.GetButtonDown("Circle")) {
                howToAttack.SetActive(false);
            }
        }
        if (howToGuard.activeSelf) {
            if (Input.GetButtonDown("X")) {
                howToGuard.SetActive(false);
            }
        }
        if (howToRoll.activeSelf) {
            if (Input.GetButtonDown("X")) {
                howToRoll.SetActive(false);
            }
        }
        if (portalList.activeSelf) {
            if (Input.GetButtonDown("Circle")) {
                portalList.SetActive(false);
            }
        }
    }
    private void SaveMenuUp(GameObject loc, GameObject res) {
        saveMenu.SetActive(true);
    }
    private void SaveMenuDown() {
        saveMenu.SetActive(false);
    }
    #region spellTag Manangement
    private void SpellTagListUp() {
        spellTagList.SetActive(true);
    }
    private void SpellTagListDown() {
        spellTagList.SetActive(false);
    }
    private void SetLastSelectedSpellTagSlot(SpellTagSlot slot) {
        lastSpellSlotSelected = slot;
    }
    private void SetSpell(SpellTag spell) {
        lastSpellSlotSelected.Spell = spell;
    }
    #endregion

    private void AccessHowTos(int s) {

        switch (s) {

            case 0:
                howToAttack.SetActive(true);
                break;
            case 1:
                howToGuard.SetActive(true);
                break;
            case 2:
                howToRoll.SetActive(true);
                break;
        }


    }
    private void AreaChange(Vector3 travelPoint) {

        StartCoroutine(FadeOutCoroutine(travelPoint));
    }
    private IEnumerator FadeOutCoroutine(Vector3 travelPoint) {
        while (isActiveAndEnabled && black.color.a <= 0.99) {
            yield return null;
            FadeToBlack();
        }
        NextScene(travelPoint);
    }
    private void FadeToBlack() {
        Color color = black.color;
        color.a += 0.03f;
        black.color = color;

    }
    private void StartFade() {
        Debug.Log("start fade was called");
        StartCoroutine(FadeScreen());
    }
    private IEnumerator WaitTilFaded() {
        yield return new WaitUntil(() => black.color.a > 0.98);
        MenusDown();
    }
    private IEnumerator FadeScreen() {
        while (isActiveAndEnabled && black.color.a <= 0.99) {
            yield return null;
            FadeToBlack();
        }
        MenusDown();
        loadingIcon.SetActive(true);
        if (nullEnemies != null) {
            nullEnemies();
        }
    }
    private void NextScene(Vector3 travelPoint) {
        StopCoroutine(FadeOutCoroutine(travelPoint));
        Debug.Log("ouchhhhh");
        Player.GetPlayer().transform.position = travelPoint;
        Color color = black.color;
        color.a = 0;
        black.color = color;
    }
    private IEnumerator FadeCoroutine() {

        yield return new WaitUntil(() => black.color.a >= 0.98);
        StartCoroutine(WaitToArrangeCoroutine());
    }
    private void UnFade() {
        StartCoroutine(FadeBackIn());
    }
    private IEnumerator FadeBackIn() {
        while (isActiveAndEnabled && black.color.a >= 0) {
            yield return null;
            Color color = black.color;
            color.a -= 0.003f;
            black.color = color;
        }
        loadingIcon.SetActive(false);
    }
    private IEnumerator WaitToArrangeCoroutine() {
        YieldInstruction wait = new WaitForSeconds(3);

        yield return wait;



        Player.GetPlayer().Nav.enabled = true;

        Player.GetPlayer().InputSealed = false;
    }
    private void PauseGame() {
        Player.GetPlayer().Pause = true;

    }
    public void FuckU() {

        Debug.Log("fucks i give :o 0");

    }
    private void GameScreen() {
        StartCoroutine(WaitCoroutine());

    }
    private void ObjectiveMenuHandling() {
        if (MissionListing.transform.childCount > 0) {
            defaultObject = MissionListing.transform.GetChild(0).gameObject;
            MissionListing.transform.GetChild(0).GetComponent<Objective>().IconClick();
            Debug.Log("this should work???");
        }
        else {

            defaultObject = null;
        }
    }

    private void ObjectiveDescription(string objective) {
        DescriptionBox.text = objective;

    }
    private void UseMenuHandling() {

        defaultObject = useButton.gameObject;
        GetSelected();
    }
    private void GetSelected() {
        EventSystem.current.SetSelectedGameObject(DefaultObject);
    }


    private void LevelUpMenuUp() {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        if (!levelMenuPrefab.activeSelf) {
            levelMenuPrefab.SetActive(true);

        }
        ViewStatsUpWindow();

        DefaultObject = levelMenuDefaultButton;
        //EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    private void MenusDown() {
        levelMenuPrefab.SetActive(false);
        storeMenu.SetActive(false);
        if (unsealPlayerInput != null) {
            unsealPlayerInput();
        }
        if (menuState == 0) {
            Player.GetPlayer().Pause = false;
            pauseMenu.SetActive(false);
        }
    }



    private void SetCanvas() {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) {
            mainMenuCanvas.SetActive(true);
            mainMenuEventSystem.SetActive(true);
            mainEventSystem.SetActive(false);
            mainCanvas.SetActive(false);
        }
        else {
            mainCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            mainMenuEventSystem.SetActive(false);
            mainEventSystem.SetActive(true);
        }

    }


    #region Tutorial Logic
    public void MoveTutor() {
        Clear();
        movementTutorial.SetActive(true);
    }
    public void MiniMapTutor() {
        Clear();
        miniMapTutorial.SetActive(true);
    }
    public void PauseTutor() {
        Clear();
        pauseTutorial.SetActive(true);
    }
    public void CombatTutor() {
        Clear();
        combatTutorial.SetActive(true);
    }
    public void TutorMenu() {
        Clear();
        backButton.SetActive(false);
        tutorMenu.SetActive(true);
    }

    public void MainMenu() {
        Clear();
        backButton.SetActive(false);
        background.SetActive(false);
    }
    private void Clear() {
        backButton.SetActive(true);
        background.SetActive(true);
        tutorMenu.SetActive(false);
        movementTutorial.SetActive(false);
        miniMapTutorial.SetActive(false);
        pauseTutorial.SetActive(false);
        combatTutorial.SetActive(false);
    }
    public void CloseTheStore() {

        storeMenu.SetActive(false);
        Player.GetPlayer().MoveSpeed = 6;

    }

    public void ClearSkillTutorials() {


        fireBallTutorial.SetActive(false);
        flameTornadoTutorial.SetActive(false);
        HeavySwingTutorial.SetActive(false);
    }
    public void FireBallTutorialUp() {
        fireBallTutorial.SetActive(true);
    }
    public void FlameTornadoTutorialUp() {
        flameTornadoTutorial.SetActive(true);

    }
    public void HeavySwingTutorialUp() {
        HeavySwingTutorial.SetActive(true);

    }
    #endregion
    #region UI Updates
    private void DialogueManagerUp() {
        if (!dialogueMenu.activeSelf) {
            dialogueMenu.SetActive(true);

        }

    }
    private void DialogueManagerDown() {
        dialogueMenu.SetActive(false);
    }
    private void StatsUpdate() {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MP;
        exp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
        money.text = "Munn: " + Player.GetPlayer().Money.ToString();
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        expBar.maxValue = Player.GetPlayer().stats.CalculateExpNeed();

        level.text = "LV. " + Player.GetPlayer().stats.Level;
    }
    private void UpdateMoney() {
        money.text = "Munn: " + Player.GetPlayer().Money.ToString();


    }
    private void ViewStats() {
        attack.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defense.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthAb.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        staminaAb.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
        //intelligence.text = "Intellect = " + Player.GetPlayer().stats.Intellect.ToString();
        attackDisplay.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defenseDisplay.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthDisplay.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        mpDisplay.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
        swordLevel.text = "Lv. " + Player.GetPlayer().stats.SwordLevel.ToString();
        swordProficency.text = Player.GetPlayer().stats.SwordProficency.ToString() + "/1000";
    }

    private void HealthChange() {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;


    }
    private void MPChange() {
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MPLeft;
        /*azaMP.text="Mp: " + AzaAi.GetAza().stats.MPLeft;
        azaMPBar.maxValue = AzaAi.GetAza().stats.MP;
        azaMPBar.value = AzaAi.GetAza().stats.MPLeft;*/
    }
    private void EnemyDeath() {
        exp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
    }


    public void SoulsToAbility() {
        if (Player.GetPlayer().stats.Exp > Player.GetPlayer().stats.RequiredExp) {
            Player.GetPlayer().stats.Abilitypoints++;
            Player.GetPlayer().stats.Exp -= Player.GetPlayer().stats.RequiredExp;
            Player.GetPlayer().stats.RequiredExp = (int)(Player.GetPlayer().stats.RequiredExp * 1.2f);
            lvMenuExp.text = "Spirits :" + Player.GetPlayer().stats.Exp;
            abilityPointsCost.text = "Cost :" + Player.GetPlayer().stats.RequiredExp;
        }

    }

    public void AddAttack() {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.AttackBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }

    }

    public void AddDefense() {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.DefenseBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddMp() {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.MpBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddHealth() {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.HealthBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void MinusAttack() {
        if (Player.GetPlayer().stats.AttackBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.AttackBoost--;
    }

    public void MinusDefense() {
        if (Player.GetPlayer().stats.DefenseBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.DefenseBoost--;
    }

    public void MinusMp() {
        if (Player.GetPlayer().stats.MpBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.MpBoost--;
    }

    public void MinusHealth() {
        if (Player.GetPlayer().stats.HealthBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.HealthBoost--;
    }

    private void UpdateBoost() {
        attackBoost.text = Player.GetPlayer().stats.AttackBoost.ToString();
        defenseBoost.text = Player.GetPlayer().stats.DefenseBoost.ToString();
        mpBoost.text = Player.GetPlayer().stats.MpBoost.ToString();
        healthBoost.text = Player.GetPlayer().stats.HealthBoost.ToString();
        abilityPoints.text = "Ability Points: " + Player.GetPlayer().stats.Abilitypoints.ToString();
    }
    private void ViewStatsUpWindow() {
        baseAttack.text = "Attack = " + Player.GetPlayer().stats.BaseAttack.ToString();
        baseDefense.text = "Defense = " + Player.GetPlayer().stats.BaseDefense.ToString();
        baseHealth.text = "Health = " + Player.GetPlayer().stats.BaseHealth.ToString();
        baseMp.text = "Mp = " + Player.GetPlayer().stats.BaseMp.ToString();
        lvMenuExp.text = "Spirits: " + Player.GetPlayer().stats.Exp;


    }
    public void UpQuantity() {

        quantity++;
        displayedQuantity.text = quantity.ToString();
    }
    public void LowerQuantity() {
        quantity--;
        displayedQuantity.text = quantity.ToString();
    }
    private void KryllUp() {
        kryllUi.SetActive(true);
    }
    private void KryllDown() {
        kryllUi.SetActive(false);

    }
    private void DistFromKyrllToZend(string dist) {
        distFromZend.text = dist;

    }
    #endregion


    private IEnumerator WaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(0.4f);
        yield return wait;


    }
    #region WindowPopUps 
    private IEnumerator WindowFade(GameObject window) {
        YieldInstruction wait = new WaitForSeconds(1.4f);
        Debug.Log(window + " has faded.");
        yield return wait;
        window.SetActive(false);

    }
    public void DefeatedWindow() {
        defeated.SetActive(true);
        StartCoroutine(WindowFade(defeated));
    }
    public void ItemPopUp(string info, Sprite picture) {
        imageWindow.sprite = picture;
        itemDescrp.text = info;
        Debug.Log("ItemPopUp occured");
        itemWindow.SetActive(true);
        StartCoroutine(WindowFade(itemWindow));
    }
    public void ObjectUpdate() {

        objectiveUpdated.SetActive(true);
        StartCoroutine(WindowFade(objectiveUpdated));
        Debug.Log("object update");
    }
    public void ObjectiveClear() {

        objectiveClear.SetActive(true);
        StartCoroutine(WindowFade(objectiveClear));
        Debug.Log("object cleared");
    }


    #endregion

    public void SaveGame() {
        savedGame.SetActive(true);
        StartCoroutine(WindowFade(savedGame));
    }

    public void AddObjective(Objective o) {

        objectives.Add(o);
        Instantiate(o, MissionListing.transform);
        newObjectiveWindow.SetActive(true);
        StartCoroutine(WindowFade(newObjectiveWindow));
    }


    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        SetCanvas();

    }
}
