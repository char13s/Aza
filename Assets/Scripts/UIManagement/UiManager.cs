using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
#pragma warning disable 0649
public class UiManager : MonoBehaviour
{
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
    [SerializeField] private ItemSlot weaponSlot;
    [SerializeField] private ItemSlot shieldSlot;
    [SerializeField] private ItemSlot maskSlot;
    [SerializeField] private GameObject weaponInvent;
    [SerializeField] private GameObject shieldInvent;
    [SerializeField] private GameObject maskInvent;
    [SerializeField] private GameObject equipWindowDefaultButton;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text attackDisplay;
    [SerializeField] private Text defenseDisplay;
    [SerializeField] private Text mpDisplay;


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
    private static UiManager instance;
    [Header("Skill Menu")]
    [SerializeField] private GameObject skillList;
    private SkillButton lastSelectedSkillSlot;
    StoreManager store = new StoreManager();

    //Events
    public static UnityAction missionCleared;
    
    public static UnityAction <string, Sprite> itemAdded;
    public static UnityAction<Vector3> areaChange;
    

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
    public GameObject DefaultObject { get => defaultObject; set => defaultObject = value; }
    

    public GameObject Invent { get => invent; set => invent = value; }
    public GameObject ShieldInvent { get => shieldInvent; set => shieldInvent = value; }
    public GameObject WeaponInvent { get => weaponInvent; set => weaponInvent = value; }
    public GameObject MaskInvent { get => maskInvent; set => maskInvent = value; }
    public Text DescriptionBox { get => descriptionBox; set => descriptionBox = value; }
    public Font LuckiestGuy { get => luckiestGuy; set => luckiestGuy = value; }
    
	public GameObject MissionListing { get => missionListing; set => missionListing = value; }
    public ItemSlot WeaponSlot { get => weaponSlot; set => weaponSlot = value; }
    public ItemSlot ShieldSlot { get => shieldSlot; set => shieldSlot = value; }
    public ItemSlot MaskSlot { get => maskSlot; set => maskSlot = value; }
    public int MenuState { get => menuState; set { menuState = value; } }
    #endregion


    //public static event UnityAction movementTutorialActive;
    //public static event UnityAction miniMapTutorialActive;
    //public static event UnityAction pauseTutorialActive;
    //public static event UnityAction combatTutorialActive;
    //public static GameObject GetUseMenu() => useMenu;
    public static UiManager GetUiManager() => instance;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        UseMenu = useMenuPrefab;
        UseButton = useButtonPrefab;
        GiveButton = giveButtonPrefab;
        itemDescriptionButton = itemDescriptionButtonPrefab;
        dropButton = dropButtonPrefab;
        
        storeMenu = StoreMenuPrefab;
        
        dialogueMenu = dialogueMenuPrefab;
        weaponSlot.Awake();
        ShieldSlot.Awake();
        MaskSlot.Awake();
        AIKryll.sendDist += DistFromKyrllToZend;
        AIKryll.zend += KryllDown;
        Player.kryll += KryllUp;
        Player.notSleeping += SaveMenuDown;
        Player.cancelPaused += MenusDown;
        StoreManager.itemWasBought += UpdateMoney;
        GameController.onGameWasStarted += GameScreen;
        Npc.dialogueUp += DialogueManagerUp;
        Npc.dialogueDown += DialogueManagerDown;
        ExpConverter.levelMenuUp += LevelUpMenuUp;
        Stats.onBaseStatsUpdate += UpdateBoost;
        Items.onItemClick += UseMenuHandling;
        Objective.onObjectiveClick += ObjectiveDescription;
        Bed.bed += SaveMenuUp;
        GameController.gameWasSaved += SaveGame;
        missionCleared += ObjectiveClear;
        itemAdded += ItemPopUp;
        areaChange += AreaChange;
        Skill.sendSkill += SetSkillToSlot;
        SkillButton.sendSkillSlot += SetLastSkillSlot;
    }
    void Start()
    {

        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onMPLeft += MPChange;
        Stats.onHealthChange += HealthChange;
        Enemy.onAnyEnemyDead += EnemyDeath;
        WeaponSlot.GetComponent<Button>().onClick.AddListener(WeaponInventUp);
        ShieldSlot.GetComponent<Button>().onClick.AddListener(ShieldInventUp);
        MaskSlot.GetComponent<Button>().onClick.AddListener(MaskInventUp);
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //GetSelected();
        }
        if (Player.GetPlayer().Pause) {
            if (MenuState > 0 && Input.GetButtonDown("Circle")) {
                StartCoroutine(WaitForPauseMenu());
                
            }
            
        }
        if (Input.GetButtonDown("Pause")) {
            PauseMenuControl(0);

        }

		if (missionListing.transform.childCount > 0) { 
		ObjectiveDescription(missionListing.transform.GetChild(0).GetComponent<Objective>().Description[missionListing.transform.GetChild(0).GetComponent<Objective>().CurrentDescription]);
		}//SetCanvas();
        //CancelMenu();
	}
    private IEnumerator WaitForPauseMenu() {
        yield return null;
        PauseMenuControl(0);

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
    #endregion
    #region Menus

    public void PauseMenuControl(int num) {
         MenuState= num;
        menus.SetActive(false);
        switch (MenuState) {
            case 0:
                Debug.Log("back to basics");
                menus.SetActive(true);
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
                Debug.Log("wtf");
                skills.SetActive(true);
                break;
            case 4:
                stats.SetActive(true);
                break;
            case 5:
                options.SetActive(true);
                break;
        }

    }
    
private void EquipmentInventUp(ItemSlot.ItemSlotType type) {
        ClearInvents();
        switch (type)
        {
            case ItemSlot.ItemSlotType.Weapon:

                break;
            case ItemSlot.ItemSlotType.Shield:

                break;
            case ItemSlot.ItemSlotType.Mask:

                break;
        }
    }
    public void WeaponInventUp() {

        WeaponInvent.SetActive(true);
    }
    public void ShieldInventUp()
    {
        ShieldInvent.SetActive(true);
    }
    public void MaskInventUp()
    {
        MaskInvent.SetActive(true);
    }
    private void ClearInvents() {
        WeaponInvent.SetActive(false);
        ShieldInvent.SetActive(false);
        MaskInvent.SetActive(false);
    }

    #endregion
    private void CancelMenu() {
        if (howToAttack.activeSelf) {
            if (Input.GetButtonDown("X")) {
                howToAttack.SetActive(false);
            }
        }
        if (howToGuard.activeSelf)
        {
            if (Input.GetButtonDown("X"))
            {
                howToGuard.SetActive(false);
            }
        }
        if (howToRoll.activeSelf)
        {
            if (Input.GetButtonDown("X"))
            {
                howToRoll.SetActive(false);
            }
        }
    }
    private void SaveMenuUp(GameObject loc,GameObject res) {
        saveMenu.SetActive(true);
    }
    private void SaveMenuDown() {
        saveMenu.SetActive(false);
    }
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
    private IEnumerator FadeOutCoroutine(Vector3 travelPoint)
    {
        while (isActiveAndEnabled && black.color.a <= 0.99)
        {
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
    private void NextScene(Vector3 travelPoint)
    {
        StopCoroutine(FadeOutCoroutine(travelPoint));
        Debug.Log("ouchhhhh");
        Player.GetPlayer().transform.position = travelPoint;
        Color color = black.color;
        color.a = 0;
        black.color = color;
    }
    private IEnumerator FadeCoroutine()
    {

        yield return new WaitUntil(() => black.color.a >= 0.98);
        StartCoroutine(WaitToArrangeCoroutine());
    }
    private IEnumerator WaitToArrangeCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(1);

        yield return wait;
        
        

        Player.GetPlayer().Nav.enabled = true;

        Player.GetPlayer().InputSealed = false;
    }
    private void PauseGame() {
        Player.GetPlayer().Pause = true;

    }
    public void FuckU() {

        Debug.Log("fucks i give :0");

    }
    private void GameScreen() {
        StartCoroutine(WaitCoroutine());

    }
    private void ObjectiveMenuHandling() {
        if (MissionListing.transform.childCount>0)
        {
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
    private void GetSelected()
    {
        /*EventSystem.current.SetSelectedGameObject(DefaultObject); */
    }
    
    
    private void LevelUpMenuUp()
    {
        if (!levelMenuPrefab.activeSelf)
        {
            levelMenuPrefab.SetActive(true);
            
        }
        ViewStatsUpWindow();
        
        DefaultObject = levelMenuDefaultButton;
        //EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    private void MenusDown() {
        levelMenuPrefab.SetActive(false);
        storeMenu.SetActive(false);

        if (menuState == 0) {
            Player.GetPlayer().Pause = false;
            pauseMenu.SetActive(false);
        }
    }

    
    
    private void SetCanvas() {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            mainMenuCanvas.SetActive(true);
            mainMenuEventSystem.SetActive(true);
            mainEventSystem.SetActive(false);
            mainCanvas.SetActive(false);
        }
        else
        {
            mainCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            mainMenuEventSystem.SetActive(false);
            mainEventSystem.SetActive(true);
        }

    }

    
    #region Tutorial Logic
    public void MoveTutor()
    {
        Clear();
        movementTutorial.SetActive(true);
    }
    public void MiniMapTutor()
    {
        Clear();
        miniMapTutorial.SetActive(true);
    }
    public void PauseTutor()
    {
        Clear();
        pauseTutorial.SetActive(true);
    }
    public void CombatTutor()
    {
        Clear();
        combatTutorial.SetActive(true);
    }
    public void TutorMenu()
    {
        Clear();
        backButton.SetActive(false);
        tutorMenu.SetActive(true);
    }
    
    public void MainMenu()
    {
        Clear();
        backButton.SetActive(false);
        background.SetActive(false);
    }
    private void Clear()
    {
        backButton.SetActive(true);
        background.SetActive(true);
        tutorMenu.SetActive(false);
        movementTutorial.SetActive(false);
        miniMapTutorial.SetActive(false);
        pauseTutorial.SetActive(false);
        combatTutorial.SetActive(false);
    }
    public void CloseTheStore()
    {

        storeMenu.SetActive(false);
        Player.GetPlayer().MoveSpeed = 6;

    }
    
    public void ClearSkillTutorials()
    {

        
        fireBallTutorial.SetActive(false);
        flameTornadoTutorial.SetActive(false);
        HeavySwingTutorial.SetActive(false);
    }
    public void FireBallTutorialUp()
    {
        fireBallTutorial.SetActive(true);
    }
    public void FlameTornadoTutorialUp()
    {
        flameTornadoTutorial.SetActive(true);
        
    }
    public void HeavySwingTutorialUp()
    {
        HeavySwingTutorial.SetActive(true);
        
    }
    #endregion
    #region UI Updates
    private void DialogueManagerUp()
    {
        if (!dialogueMenu.activeSelf)
        {
            dialogueMenu.SetActive(true);

        }

    }
    private void DialogueManagerDown()
    {
        dialogueMenu.SetActive(false);
    }
    private void StatsUpdate()
    {
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
    private void UpdateMoney()
    {
        money.text = "Munn: " + Player.GetPlayer().Money.ToString();


    }
    private void ViewStats()
    {
        attack.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defense.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthAb.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        staminaAb.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
        intelligence.text = "Intellect = " + Player.GetPlayer().stats.Intellect.ToString();
        attackDisplay.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defenseDisplay.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthDisplay.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        mpDisplay.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
    }

    private void HealthChange()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;


    }
    private void MPChange()
    {
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MPLeft;
        /*azaMP.text="Mp: " + AzaAi.GetAza().stats.MPLeft;
        azaMPBar.maxValue = AzaAi.GetAza().stats.MP;
        azaMPBar.value = AzaAi.GetAza().stats.MPLeft;*/
    }
    private void EnemyDeath()
    {
        exp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
    }
    
    
    public void SoulsToAbility()
    {
        if (Player.GetPlayer().stats.Exp > Player.GetPlayer().stats.RequiredExp)
        {
            Player.GetPlayer().stats.Abilitypoints++;
            Player.GetPlayer().stats.Exp -= Player.GetPlayer().stats.RequiredExp;
            Player.GetPlayer().stats.RequiredExp = (int)(Player.GetPlayer().stats.RequiredExp*1.2f);
            lvMenuExp.text = "Spirits :" + Player.GetPlayer().stats.Exp;
            abilityPointsCost.text = "Cost :" + Player.GetPlayer().stats.RequiredExp;
        }
        
    }

    public void AddAttack()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.AttackBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
        
    }

    public void AddDefense()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.DefenseBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddMp()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.MpBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddHealth()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.HealthBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void MinusAttack()
    {
        if (Player.GetPlayer().stats.AttackBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.AttackBoost--;
    }

    public void MinusDefense()
    {
        if (Player.GetPlayer().stats.DefenseBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.DefenseBoost--;
    }

    public void MinusMp()
    {
        if (Player.GetPlayer().stats.MpBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.MpBoost--;
    }

    public void MinusHealth()
    {
        if (Player.GetPlayer().stats.HealthBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.HealthBoost--;
    }

    private void UpdateBoost()
    {
        attackBoost.text = Player.GetPlayer().stats.AttackBoost.ToString();
        defenseBoost.text = Player.GetPlayer().stats.DefenseBoost.ToString();
        mpBoost.text = Player.GetPlayer().stats.MpBoost.ToString();
        healthBoost.text = Player.GetPlayer().stats.HealthBoost.ToString();
        abilityPoints.text = "Ability Points: "+Player.GetPlayer().stats.Abilitypoints.ToString();
    }
    private void ViewStatsUpWindow()
    {
        baseAttack.text = "Attack = " + Player.GetPlayer().stats.BaseAttack.ToString();
        baseDefense.text = "Defense = " + Player.GetPlayer().stats.BaseDefense.ToString();
        baseHealth.text = "Health = " + Player.GetPlayer().stats.BaseHealth.ToString();
        baseMp.text = "Mp = " + Player.GetPlayer().stats.BaseMp.ToString();
        lvMenuExp.text = "Souls: " + Player.GetPlayer().stats.Exp;


    }
    private void KryllUp() {
        kryllUi.SetActive(true);
    }
    private void KryllDown()
    {
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
    private IEnumerator WindowFade(GameObject window)
    {
        YieldInstruction wait = new WaitForSeconds(1.4f);
		Debug.Log(window+" has faded.");
        yield return wait;
        window.SetActive(false);

    }
	public void DefeatedWindow() {
		defeated.SetActive(true);
		StartCoroutine(WindowFade(defeated));
	}
	public void ItemPopUp(string info,Sprite picture) {
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
	public void SaveGame() {
		savedGame.SetActive(true);
		StartCoroutine(WindowFade(savedGame));
	}
    private void AreYouWorking(GameObject b) {

        //Debug.Log(b == null ? "null" : b.name + ": activeSelf = " + b.activeSelf + ", activeInHierarchy" + b.activeInHierarchy);

    }
    public void AddObjective(Objective o) {

        objectives.Add(o);
        Instantiate(o,MissionListing.transform);
        newObjectiveWindow.SetActive(true);
        StartCoroutine(WindowFade(newObjectiveWindow));
    }

    
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetCanvas();
        AreYouWorking(mainMenuCanvas);
        AreYouWorking(mainCanvas);
        AreYouWorking(pauseMenu);
    }
}
