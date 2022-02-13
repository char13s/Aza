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
    [SerializeField] private GameObject souls;
    [SerializeField] private Text soulCount;
    [SerializeField] private GameObject soulUpPosition;
    [SerializeField] private GameObject soulDownPosition;
    [SerializeField] private int soulsObtained;
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
    [Header("Stat Build Menu")]
    [SerializeField] private GameObject MeditationMenu;
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
    [SerializeField] private Text kryllLevel;
    [SerializeField] private GameObject skillTree;
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
	[SerializeField] private GameObject optionMenu;
	[SerializeField] private GameObject soundSettings;
	[SerializeField] private GameObject gameSettings;
	[SerializeField] private Slider masterVolume;
	[SerializeField] private Slider sfxVolume;
    //[SerializeField] private 

    [Header("Pop Up Windows")]
    [SerializeField] private GameObject newObjectiveWindow;
    [SerializeField] private GameObject objectiveClear;
    [SerializeField] private GameObject objectiveUpdated;
    [SerializeField] private GameObject savedGame;
    [SerializeField] private GameObject loadedGame;
    [SerializeField] private GameObject defeated;
    [SerializeField] private GameObject loadingIcon;
    [SerializeField] private GameObject endScreen;

    [Header("ItemObtainedPopWindow")]
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private Image imageWindow;
    [SerializeField] private Text itemDescrp;
    [Space]

    [Header("PopUpTutorials")]
    [SerializeField] private GameObject howToAttack;
    [SerializeField] private GameObject howToGuard;
    [SerializeField] private GameObject howToMove;
    [SerializeField] private GameObject cameraControls;
    [Space]

    [SerializeField] private GameObject howToDash;
    [SerializeField] private GameObject howToJump;
    [SerializeField] private GameObject howToLockOn;
    [SerializeField] private GameObject howToUseSkills;
    [SerializeField] private GameObject howToUseRelics;
    [SerializeField] private GameObject howToUseBow;


    [Header("Fonts")]
    [SerializeField] private Font luckiestGuy;

    [Header("KryllUI")]
    [SerializeField] private GameObject kryllUi;
    [SerializeField] private Text distFromZend;

    [Header("Save Menu")]
    [SerializeField] private GameObject saveMenu;
    [SerializeField] private GameObject saveMenuDefault;

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
    [SerializeField] private GameObject skillListDefault;
    [SerializeField] private GameObject skillDefaultButton;
    [SerializeField] private SkillButton triangle;
    [SerializeField] private SkillButton circle;
    [SerializeField] private SkillButton square;
    [SerializeField] private SkillButton x;
    private SkillButton lastSelectedSkillSlot;

    [Header("QuickSkillMenu")]
    [SerializeField] private GameObject quickSkillMenu;
    [SerializeField] private Text skillslot1;
    [SerializeField] private Text skillslot2;
    [SerializeField] private Text skillslot3;
    [SerializeField] private Text skillslot4;
    [SerializeField] private Image currentWeapon;

    [Header("Skin Menu")]
    [SerializeField] private GameObject skinMenu;

    [Space]
    [Header("QuickAccessMenu")]
    [SerializeField] private GameObject itemInvent;
    [SerializeField] private Image mainItem;
    [SerializeField] private Text mainItemQuantity;
    [SerializeField] private Text mainItemName;
    [SerializeField] private Image leftItem;
    [SerializeField] private Image rightItem;
    [SerializeField] private GameObject itemsList;
    [SerializeField] private GameObject relicList;
    [Space]

    [Header("PortalList")]
    [SerializeField] private GameObject portalList;
    [SerializeField] private GameObject portalDefaultObject;

    [Space]
    [Header("Spell Tag Menu")]
    [SerializeField] private GameObject spellTagList;

    [Header("Stats Menu")]
    [SerializeField] private Text swordLevel;
    [SerializeField] private Text swordProficency;

    [Header("Title Screen Menu")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject endOfDemoScreen;
    [Header("Dialogues")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject landed;
    [SerializeField] private GameObject enemyAppeared;

    [Header("Level Selection Window")]
    [SerializeField] private GameObject missionDetails;
    [SerializeField] private Image levelPicture;
    [SerializeField] private Text bossInfo;
    [SerializeField] private Text objective;
    [SerializeField] private Sprite[] rewards;
    [SerializeField] private Button loadLevel;
    [SerializeField] private Button goBack;
    [SerializeField] private GameObject levelSelectWindow;
    [SerializeField] private GameObject levelSelectionDefault;
    [Space]
    [SerializeField] private GameObject choiceDefaultButton;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject demoOverScreen;
	#region Events
	public static UnityAction missionCleared;
    public static event UnityAction sealPlayerInput;
    public static event UnityAction unsealPlayerInput;
    public static UnityAction<string, Sprite> itemAdded;
    public static UnityAction<Vector3> areaChange;
    public static UnityAction<int> portal;
    public static UnityAction nullEnemies;
    public static UnityAction bedTime;
    public static UnityAction outaBed;
    public static UnityAction disablePlayer;
    public static UnityAction onNewGame;
    public static event UnityAction load;
    public static event UnityAction<int> nextLevel;
    public static event UnityAction demonSword;
    public static event UnityAction angelSword;
    public static event UnityAction bothSwords;
	public static event UnityAction<float> sendMasterVolume;
	public static event UnityAction<float> sendSfxVolume;
    public static event UnityAction pause;
    public static event UnityAction<bool> killAll;
	#endregion

	[SerializeField] private GameObject defaultObject;
    [SerializeField] private GameObject inventDefaultButton;
    private int menuState;
    private Player pc;
    private SpriteAssign sprites;
    private bool tutorialUp;
    private bool dialogue;
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
        SetButton();

        missionCleared += ObjectiveClear;
        itemAdded += ItemPopUp;
        areaChange += AreaChange;
        bedTime += SaveMenuUp;
        outaBed += SaveMenuDown;
    }
    void Start() {
        #region outside events
        Player.weaponSwitch += WeaponSwitch;
        StoreManager.itemWasBought += UpdateMoney;

        GameController.onGameWasStarted += GameScreen;
        GameController.gameWasSaved += SaveGame;
        GameController.onQuitGame += OnQuit;
        GameController.continueGame += LevelSelect;
        GameController.continueGame += SelectLevelButton;
        GameController.setCanvas += SetCanvas;
        //GameController.returnToLevelSelect += LevelSelect;
        GameController.returnToLevelSelect += UnFade;
        GameController.onoLevelLoaded += UnFade;
        GameController.respawn += SetPlayerUI;
        ExpConverter.levelMenuUp += MenuManager;

        Stats.onBaseStatsUpdate += UpdateBoost;

        Items.onItemClick += UseMenuHandling;
        Inventory.mainItemSet += SetImage;
        Inventory.menuSet += QuickAccessMenu;
        Objective.onObjectiveClick += ObjectiveDescription;

        Bed.bed += Sleep;

        PortalConnector.portalListUp += MenuManager;
        PortalConnector.backToLevelSelect += Portal;

        Skill.sendSkill += SetSkillToSlot;
        SkillButton.sendSkillSlot += SetLastSkillSlot;

        //CinematicManager.unfade += UnFade;
        CinematicManager.gameStart += GameStart;

        Cauldron.potionMaking += MenuManager;

        SpellTagSlot.spellInvent += SpellTagListUp;
        SpellTag.spellListDown += SpellTagListDown;
        SpellTagSlot.sendThisSlot += SetLastSelectedSpellTagSlot;
        SpellTag.sendThisSpell += SetSpell;

        DialogueTrigger.triggered += DialogueManagement;
        DialogueTrigger.close += CloseDialogue;

        LevelObject.selectLevel += SetMissionDetails;
        EndGameTrigger.end += EndScreen;

        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onMPLeft += MPChange;
        Stats.onHealthChange += HealthChange;

        Enemy.onAnyEnemyDead += EnemyDeath;

        Player.onPlayerDeath += OnPlayerDeath;

        EventManager.sceneChanger += LoadLevelHelper;
        EventManager.chooseSword += ChoicePanelUp;
        TutorialTriggers.requestTutorial += TutorialUpProcessor;
        EventManager.demoRestart += ChangeSword;

        Souls.soulCount += UpdateUI;
        DialogueTrigger1.dialogueUp += DialogueUp;
        SceneDialogue.turnOffDialogue += DialogueUp;
        Interactable.endDemo += DemoOver;
		#endregion
		masterVolume.onValueChanged.AddListener(OnMasterVolumeChange);
		sfxVolume.onValueChanged.AddListener(OnSFXVolumeChange);
        OnMasterVolumeChange(0.3f);
        OnSFXVolumeChange(0.3f);
        pc = Player.GetPlayer();
        sprites = SpriteAssign.GetSprite();
        WeaponSwitch();
        //GameController.returnToLevelSelect += SetPlayerUIOff;
        //playerUi.SetActive(false);
        //UnFade();
        //DefaultObject = newGameButton;
        //GetSelected();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    void OnDisable() {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetButtonDown("Pause")) {
            //GetSelected();
        }
        if (Player.GetPlayer().Pause) {
            if (MenuState > 0 && Input.GetButtonDown("Circle")) {
                StartCoroutine(WaitForPauseMenu());
            }
        }
        if (Input.GetButtonDown("Pause")) {
            PauseMenuControl(0);
            DefaultObject = pauseMenuDefaultButton;
            if (pause != null) {
                pause();
            }
            GetSelected();
        }
        if (tutorialUp||pc.Pause) {
            if (Input.GetButtonDown("Circle")) {
                ClearTutorials();
            }
        }
        if (GameController.GetGameController().GameMode < 1) {
            Inputs();
        }
        if (Input.GetKeyDown(KeyCode.Equals)) {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
    }
    
    #region Soul Shit
    private void UpdateUI() {
        soulsObtained++;
        soulCount.text = soulsObtained.ToString();
        SoulsUp();
        StopCoroutine(DropSoulCount());
        StartCoroutine(DropSoulCount());
    }
    private void SoulsUp() {
        souls.transform.position = soulUpPosition.transform.position;
    }
    private IEnumerator DropSoulCount() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;

        souls.transform.position = soulDownPosition.transform.position;
    }
    #endregion
    #region Level Selection shit
    private void SetButton() {

        loadLevel.onClick.AddListener(LoadLevel);
    }
    private void LevelSelect() {
        levelSelectWindow.SetActive(true);

        //playerUi.SetActive(false);

        DefaultObject = levelSelectionDefault.gameObject;

    }
    private void SelectLevelButton() {
        DefaultObject = levelSelectionDefault;
    }
    private void LoadLevelHelper(int lvl) {
        if (nextLevel != null) {
            nextLevel(lvl);
        }
        LoadLevel();
    }
    public void LoadLevel() {
        Player.GetPlayer().Dead = false;
        missionDetails.SetActive(false);
        StartFade(load);
    }
    private void SetMissionDetails(Sprite[] rewards, string details, int lvl) {
        if (nextLevel != null) {
            nextLevel(lvl);
        }
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        levelSelectWindow.SetActive(false);
        LoadLevel();
        //missionDetails.SetActive(true);
        //GameController.GetGameController().GameMode = 2;
        DefaultObject = loadLevel.gameObject;
        Debug.Log("Set Mission Details");
    }
    #endregion
    #region Quick menu methods
    private void QuickSkillmenu(bool val) {
        quickSkillMenu.SetActive(val);
        skillslot1.text = triangle.SkillName.text;
        skillslot2.text = circle.SkillName.text;
        skillslot3.text = square.SkillName.text;
        skillslot4.text = x.SkillName.text;
    }
    private void WeaponSwitch() {

        switch (pc.Weapon) {
            case 0:
                currentWeapon.sprite = sprites.Sword;
                break;
            case 1:
                currentWeapon.sprite = sprites.Bow;
                break;
        }

    }

	#endregion
	#region Event Handlers
	private void OnMasterVolumeChange(float val) {
		if (sendMasterVolume != null) {
			sendMasterVolume(val);
		}
	}
	private void OnSFXVolumeChange(float val) {
		if (sendSfxVolume != null) {
			sendSfxVolume(val);
		}
	}
	private void SetPlayerUI() {
        playerUi.SetActive(true);
    }
    private void SetPlayerUIOff() {
        playerUi.SetActive(false);
    }
    private void QuickAccessMenu(int menu) {
        switch (menu) {
            case 0:
                itemsList.SetActive(false);
                //relicList.SetActive(false);
                break;
            case 1:
                itemsList.SetActive(true);
                //relicList.SetActive(true);
                break;
        }
    }
    private void SetSkillToSlot(Skill skill) {
        lastSelectedSkillSlot.SkillAssigned = skill;
        skillList.SetActive(false);
        DefaultObject = skillDefaultButton;
    }
    private void SetLastSkillSlot(SkillButton slot) {
        lastSelectedSkillSlot = slot;
        DefaultObject = skillListDefault;
        skillList.SetActive(true);

    }
    private void SetImage(Sprite i, string name, string quantity) {
        mainItem.sprite = i;
        mainItemName.text = name;
        mainItemQuantity.text = quantity;
    }
    private void OnPlayerDeath() {
        SetPlayerUIOff();
        int num = SceneManager.GetActiveScene().buildIndex;
        if (nextLevel != null) {
            nextLevel(num);
        }
        StartFade(load);

    }
    private void DemoOver() {
        SetPlayerUIOff();
        StartFade(null);
        demoOverScreen.SetActive(true);
    }
    //private void DialogueManagement(string lines) {
    //dialogueText.text = lines;
    //}
    #endregion
    #region Menus
    private void Inputs() {

        if (levelSelectWindow.activeSelf) {
            if (Input.GetButtonDown("Triangle")) {
                levelSelectWindow.SetActive(false);
                saveMenu.SetActive(true);
                DefaultObject = saveMenuDefault;
            }

            if (Input.GetButtonDown("Square")) {
                levelSelectWindow.SetActive(false);
                skills.SetActive(true);
                DefaultObject = skillDefaultButton;
            }
        }
        else {
            if (Input.GetButtonDown("Circle") && SceneManager.sceneCount == 1) {
                saveMenu.SetActive(false);
                missionDetails.SetActive(false);
                skills.SetActive(false);
                skillList.SetActive(false);
                //levelSelectWindow.SetActive(true);
                DefaultObject = levelMenuDefaultButton;
            }

        }

    }
    private void TutorialUpProcessor(int num) {
        tutorialUp = true;
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        switch (num) {
            case 0:
                howToMove.SetActive(true);
                break;
            case 1:
                howToAttack.SetActive(true);
                break;
            case 2:
                howToGuard.SetActive(true);
                break;
            case 3:
                howToUseRelics.SetActive(true);
                break;
            case 4:
                howToUseSkills.SetActive(true);
                break;
            case 5:
                howToUseBow.SetActive(true);
                break;
        }
    }
    private void ClearTutorials() {
        tutorialUp = false;
        if (unsealPlayerInput != null) {
            unsealPlayerInput();
        }
        howToMove.SetActive(false);

        howToAttack.SetActive(false);

        howToGuard.SetActive(false);

        howToUseRelics.SetActive(false);

        howToUseSkills.SetActive(false);

        howToUseBow.SetActive(false);
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

    public void Portal() {

        //portalList.SetActive(false);
        //StartCoroutine(JustWait(connector));
        StartFade(null);

    }
    private IEnumerator WaitToLoad() {
        YieldInstruction wait = new WaitForSeconds(3);
        yield return wait;
        UnFade();
    }
    public void OptionsMenu() {
        PauseMenuControl(5);
	}
	public void SoundSettingsUp() {
		soundSettings.SetActive(true);
	}
    public void StoreUp() {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        storeMenu.SetActive(true);
    }
    //public void OpenSkillTree() {
    //    skillTree.SetActive(true);
    //    levelMenuPrefab.SetActive(false);
    //}
    public void GameSettingsUp() {
        gameSettings.SetActive(true);
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
                soundSettings.SetActive(false);
                break;
            case 1:
                items.SetActive(true);
                break;
            case 2:
                equipment.SetActive(true);
                break;
            case 3:

                skills.SetActive(true);
                DefaultObject = skillDefaultButton;
                break;
            case 4:
                stats.SetActive(true);
                ViewStats();
                break;
            case 5:
                soundSettings.SetActive(true);
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
    public void MenuManager(int menu) {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        //PauseGame();
        switch (menu) {
            case 0:

                break;
            case 1:
                storeMenu.SetActive(true);
                break;
            case 2:
                if (disablePlayer != null) {
                    disablePlayer();
                }
                MeditationMenu.SetActive(true);

                break;
            case 3:
                quantityWindow.SetActive(true);
                displayedQuantity.text = quantity.ToString();
                break;
            case 4:
                portalList.SetActive(true);
                DefaultObject = portalDefaultObject;
                break;
            case 5:
                skillTree.SetActive(true);
                break;
            case 6:
                LevelUpMenuUp();
                break;
        }
    }
    private void LevelUpMenuUp() {
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
        levelMenuPrefab.SetActive(true);
        ViewStatsUpWindow();
        DefaultObject = levelMenuDefaultButton;
        //EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    private void MenusDown() {
        //levelMenuPrefab.SetActive(false);
        //storeMenu.SetActive(false);
        //if (unsealPlayerInput != null) {
        //    unsealPlayerInput();
        //}
        //portalList.SetActive(false);
        optionMenu.SetActive(false);
        MeditationMenu.SetActive(false);
        if (menuState == 0) {
            Player.GetPlayer().Pause = false;
            pauseMenu.SetActive(false);
        }
    }
    private void OnQuit() {
        MenuManager(0);
        MenusDown();
    }


    #endregion
    #region Choice shit
    private void ChoicePanelUp() {
        choicePanel.SetActive(true);

        StartCoroutine(WaitToSeal());
        DefaultObject = choiceDefaultButton;
    }
    private IEnumerator WaitToSeal() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
    }
    public void DemonSword() {
        if (demonSword != null) {
            demonSword();
        }
        
        LoadLevelHelper(4);
        
        choicePanel.SetActive(false);
    }
    private void ChangeSword() {
        LoadLevelHelper(1);
        newGameButton.enabled = true;
    }
    public void AngelSword() {
        if (angelSword != null) {
            angelSword();
        }
       
        LoadLevelHelper(4);
        
        choicePanel.SetActive(false);
    }
    public void BothSwords() {
        if (bothSwords != null) {
            bothSwords();
        }
        LoadLevelHelper(4);

        choicePanel.SetActive(false);
    }
    private void FirstMission() {

    }
    #endregion
    #region Sleep Management
    private void SaveMenuUp() {
        saveMenu.SetActive(true);
        playerUi.SetActive(false);
    }
    private void SaveMenuDown() {
        saveMenu.SetActive(false);
        playerUi.SetActive(true);
    }
    private void Sleep() {
        StartCoroutine(FadeRealQuick(bedTime));
    }
    private void NotSleep() {
        StartCoroutine(FadeRealQuick(outaBed));
    }
    private IEnumerator FadeRealQuick(UnityAction action) {
        YieldInstruction wait = new WaitForSeconds(1);
        while (isActiveAndEnabled && black.color.a <= 0.99) {
            yield return null;
            FadeToBlack();
        }
        if (action != null) {
            action();
        }

        //UnFade();
    }

    #endregion

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
        Debug.Log(spell);
        Debug.Log(spell.SpellName);
        Debug.Log(lastSpellSlotSelected);
        //Debug.Log(lastSpellSlotSelected.SpellName.text);

        lastSpellSlotSelected.SpellName.text = spell.SpellName;
    }
    #endregion

    #region Dialogue Management
    private void DialogueManagement(int text) {
        dialogueBox.SetActive(true);
        switch (text) {
            case 0:
                intro.SetActive(true);
                break;
            case 1:
                landed.SetActive(true);
                break;
            case 2:
                enemyAppeared.SetActive(true);
                break;
        }
    }
    private void CloseDialogue(int text) {
        dialogueBox.SetActive(false);
        switch (text) {
            case 0:
                intro.SetActive(false);
                break;
            case 1:
                landed.SetActive(false);
                break;
            case 2:
                enemyAppeared.SetActive(false);
                break;
        }
    }
    private void Dashu() {

    }
    #endregion
    #region
    private void EndScreen() {
        endScreen.SetActive(true);
    }
    #endregion

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
    public void NewGame() {
        newGameButton.enabled = false;
        ClearScreen();
        SetMissionDetails(null, null, 6);
        StartFade(onNewGame);
        //LoadLevel();

    }
    private IEnumerator WaitToReturnButton() {
        YieldInstruction wait = new WaitForSeconds(5);
        yield return wait;
        newGameButton.enabled = true;
    }
    private void StartFade(UnityAction action) {

        StartCoroutine(FadeScreen(action));
    }
    private IEnumerator WaitTilFaded() {
        yield return new WaitUntil(() => black.color.a > 1);
        MenusDown();
    }
    private IEnumerator FadeScreen(UnityAction action) {
        while (isActiveAndEnabled && black.color.a <= 0.99) {
            yield return null;
            FadeToBlack();
        }
        Debug.Log("Fuck that fade");
        MenusDown();
        //levelSelectWindow.SetActive(false);
        loadingIcon.SetActive(true);
        if (action != null) {
            action();
        }
        
        if (killAll != null) {
            killAll(true);
        }
    }
    private void ClearScreen() {
        Color color = black.color;
        color.a = 0;
        black.color = color;
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

        yield return new WaitUntil(() => black.color.a >= 1);
        StartCoroutine(WaitToArrangeCoroutine());
    }
    private void UnFade() {
        StartCoroutine(WaitToUnFade());
    }
    private IEnumerator WaitToUnFade() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        StartCoroutine(FadeBackIn());
    }
    private IEnumerator FadeBackIn() {
        while (isActiveAndEnabled && black.color.a >= 0) {
            yield return null;
            Color color = black.color;
            color.a -= 0.01f;
            black.color = color;
        }
        loadingIcon.SetActive(false);
        if (!dialogue) {
            if (unsealPlayerInput != null) {
                unsealPlayerInput();
            }
        }
        
        if (killAll != null) {
            killAll(false);
        }
    }
    private IEnumerator WaitToArrangeCoroutine() {
        YieldInstruction wait = new WaitForSeconds(3);

        yield return wait;
        //Player.GetPlayer().Nav.enabled = true;
        Player.GetPlayer().InputSealed = false;
    }
    #region shit I dont use
    public void FuckU() {

        Debug.Log("fucks i give : 0");

    }
    private void ObjectiveMenuHandling() {
        if (MissionListing.transform.childCount > 0) {
            DefaultObject = MissionListing.transform.GetChild(0).gameObject;
            MissionListing.transform.GetChild(0).GetComponent<Objective>().IconClick();
            Debug.Log("this should work???");
        }
        else {

            DefaultObject = null;
        }
    }

    private void ObjectiveDescription(string objective) {
        DescriptionBox.text = objective;

    }

    public void AddObjective(Objective o) {

        objectives.Add(o);
        Instantiate(o, MissionListing.transform);
        newObjectiveWindow.SetActive(true);
        StartCoroutine(WindowFade(newObjectiveWindow));
    }
    #endregion
    private void PauseGame() {
        Player.GetPlayer().Pause = true;
    } 
    private void GameScreen() {
        StartCoroutine(WaitCoroutine());
    }
    private void DialogueUp(bool val) {
        dialogue = val;
    }
    private void UseMenuHandling() {

        DefaultObject = useButton.gameObject;
        //GetSelected();
    }
    private void GetSelected() {

        StartCoroutine(FindSelected());
        //mainMenuEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(DefaultObject);
    }
    private IEnumerator FindSelected() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    private void SetCanvas() {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded) {
            mainMenuCanvas.SetActive(true);

            mainEventSystem.SetActive(false);
            mainCanvas.SetActive(false);
        }
        else {
            mainCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);

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
            Player.GetPlayer().stats.KryllLevel++;
            Player.GetPlayer().stats.Exp -= Player.GetPlayer().stats.RequiredExp;
            Player.GetPlayer().stats.RequiredExp = (int)(Player.GetPlayer().stats.RequiredExp * 1.2f);
            kryllLevel.text = "Lv. " + Player.GetPlayer().stats.KryllLevel;
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
        abilityPointsCost.text = "Cost :" + Player.GetPlayer().stats.RequiredExp;
    }
    private void ViewStatsUpWindow() {
        baseAttack.text = "Attack = " + Player.GetPlayer().stats.BaseAttack.ToString();
        baseDefense.text = "Defense = " + Player.GetPlayer().stats.BaseDefense.ToString();
        baseHealth.text = "Health = " + Player.GetPlayer().stats.BaseHealth.ToString();
        baseMp.text = "Mp = " + Player.GetPlayer().stats.BaseMp.ToString();
        lvMenuExp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        UpdateBoost();
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
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        SetCanvas();

    }
}
