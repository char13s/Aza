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
    [Header("AzaUI")]
    [SerializeField] private Text azaMP;
    [SerializeField] private Slider azaMPBar;
    [Space]
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
    [SerializeField] private GameObject skillMenu;
    [Space]
    [Header("EventSystems")]
    [SerializeField] private GameObject mainEventSystem;
    [SerializeField] private GameObject mainMenuEventSystem;
    [Space]
    [Header("Pocket")]
    [SerializeField] private GameObject pocket;
    [SerializeField] private GameObject pageTitle;
    [SerializeField] private GameObject pageNum;
    [Space]
    [Header("CraftingMenu")]

    [SerializeField] private GameObject craftMenuPrefab;
    private static GameObject craftMenu;
    [SerializeField] private Image itemListPrefab;
    private static Image itemList;
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
    [Header("SkillAssignMenu")]
    [SerializeField] private GameObject skillAssignMenuPrefab;
    private static GameObject skillAssignMenu;
    [SerializeField] private GameObject skillAssignDefaultButton;
    [Space]
    [Header("Dialogue Management")]
    private static GameObject dialogueMenu;
    [SerializeField] private GameObject dialogueMenuPrefab;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text whoseTalking;
    [Space]
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
    [Header("EquipmentWindow")]
    [SerializeField] private ItemSlot weaponSlot;
    [SerializeField] private ItemSlot shieldSlot;
    [SerializeField] private ItemSlot maskSlot;
    [SerializeField] private GameObject weaponInvent;
    [SerializeField] private GameObject shieldInvent;
    [SerializeField] private GameObject maskInvent;
    [SerializeField] private GameObject equipWindowDefaultButton;
    [Space]
    [Header("PauseMenu")]
    [SerializeField] private GameObject invent;
    [SerializeField] private GameObject comboMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject objectiveMenu;
    [SerializeField] private GameObject statusWindow;
    private List<Objective> objectives;
    [Space]
    [Header("Videos")]
    [SerializeField] private VideoClip combo1;
    [SerializeField] private VideoClip combo2;
    [SerializeField] private VideoClip combo3;
    [SerializeField] private VideoClip combo4;
    private int page;
    [Space]
    [Header("Options")]
    [SerializeField] private GameObject optDefaultButton;
    private static UiManager instance;

    StoreManager store = new StoreManager();
    
    //Events
    public static UnityAction notCrafting;
    [SerializeField] private GameObject defaultObject;

    public static GameObject UseMenu { get => useMenu; set => useMenu = value; }
    public static Button UseButton { get => useButton; set => useButton = value; }
    public static Button ItemDescriptionButton { get => itemDescriptionButton; set => itemDescriptionButton = value; }
    public static Button GiveButton { get => giveButton; set => giveButton = value; }
    public static Button DropButton { get => dropButton; set => dropButton = value; }
    public static GameObject CraftMenu { get => craftMenu; set => craftMenu = value; }
    public static Image ItemList { get => itemList; set => itemList = value; }
    public static GameObject StoreMenu { get => storeMenu; set => storeMenu = value; }
    public static GameObject SkillAssignMenu { get => skillAssignMenu; set => skillAssignMenu = value; }
    public Image Black { get => black; set => black = value; }
    public Text DialogueText { get => dialogueText; set => dialogueText = value; }
    public static GameObject DialogueMenu { get => dialogueMenu; set => dialogueMenu = value; }
    public GameObject DefaultObject { get => defaultObject; set => defaultObject = value; }
    public int Page { get => page; set { page = Mathf.Clamp(value, 0, 2); ; PageControl(); } }



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
        CraftMenu = craftMenuPrefab;
        ItemList = itemListPrefab;
        storeMenu = StoreMenuPrefab;
        SkillAssignMenu = skillAssignMenuPrefab;
        dialogueMenu = dialogueMenuPrefab;
        StoreManager.itemWasBought += UpdateMoney;
        //GameController.onGameWasStarted += SetCanvas;
        Npc.dialogueUp += DialogueManagerUp;
        Npc.dialogueDown += DialogueManagerDown;
        ExpConverter.levelMenuUp += LevelUpMenuUp;
        Stats.onBaseStatsUpdate += UpdateBoost;
    }
    void Start()
    {
        Player.GetPlayer().items.Pocket = pocket;
        Player.GetPlayer().items.PageTitle = pageTitle;
        Player.GetPlayer().items.PageNum = pageNum;
        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onMPLeft += MPChange;
        Stats.onHealthChange += HealthChange;
        Enemy.onAnyEnemyDead += EnemyDeath;
        weaponSlot.GetComponent<Button>().onClick.AddListener(WeaponInventUp);
        shieldSlot.GetComponent<Button>().onClick.AddListener(ShieldInventUp);
        maskSlot.GetComponent<Button>().onClick.AddListener(MaskInventUp);
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
            EventSystem.current.SetSelectedGameObject(DefaultObject);
        }
        //SetCanvas();
    }
    private void PauseGame() {
        Player.GetPlayer().Pause = true;
    }

    private void PageControl() {
        switch (page) {
            case 0:
                ClearMenus();
                invent.SetActive(true);
                
                break;
            case 1:
                ClearMenus();
                comboMenu.SetActive(true);
                break;
            case 2:
                ClearMenus();

                options.SetActive(true);
                defaultObject = optDefaultButton;
                break;
            case 3:
                ClearMenus();
                objectiveMenu.SetActive(true);
                break;
            case 4:
                ClearMenus();
                statusWindow.SetActive(true);
                break;
            





        }

    }
    private void ClearMenus() {
        invent.SetActive(false);
        statusWindow.SetActive(false);
        comboMenu.SetActive(false);
        options.SetActive(false);
        objectiveMenu.SetActive(false);
    }
    private void LevelUpMenuUp()
    {
        if (!levelMenuPrefab.activeSelf)
        {
            levelMenuPrefab.SetActive(true);
            PauseGame();
        }
        ViewStatsUpWindow();
        
        DefaultObject = levelMenuDefaultButton;
        EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    public void MenusDown() {
        levelMenuPrefab.SetActive(false);
        storeMenu.SetActive(false);
    }

    public void SkillMenuUp()
    {
        abilities.SetActive(false);
        abilityClose.SetActive(false);
        skillMenu.SetActive(true);
        playerUi.SetActive(false);
        miniMap.SetActive(false);
    }
    public void SkillMenuOff()
    {
        skillMenu.SetActive(false);
        Player.GetPlayer().Pause = false;
        playerUi.SetActive(true);
        miniMap.SetActive(true);
        //abilities.SetActive(true);
    }
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
        Player.GetPlayer().MoveSpeed = 5;

    }
    public void CloseCraftMenu()
    {

        craftMenu.SetActive(false);
        if (notCrafting != null)
        {
            notCrafting();

        }

    }
    public void ClearSkillTutorials()
    {

        SkillMenuUp();
        fireBallTutorial.SetActive(false);
        flameTornadoTutorial.SetActive(false);
        HeavySwingTutorial.SetActive(false);
    }
    public void FireBallTutorialUp()
    {
        fireBallTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
    public void FlameTornadoTutorialUp()
    {
        flameTornadoTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
    public void HeavySwingTutorialUp()
    {
        HeavySwingTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
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
    void StatsUpdate()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MP;
        exp.text = "Souls: " + Player.GetPlayer().stats.Exp;
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
    void ViewStats()
    {
        attack.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defense.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthAb.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        staminaAb.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
        intelligence.text = "Intellect = " + Player.GetPlayer().stats.Intellect.ToString();
    }

    void HealthChange()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;


    }
    void MPChange()
    {
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MPLeft;
        /*azaMP.text="Mp: " + AzaAi.GetAza().stats.MPLeft;
        azaMPBar.maxValue = AzaAi.GetAza().stats.MP;
        azaMPBar.value = AzaAi.GetAza().stats.MPLeft;*/
    }
    void EnemyDeath()
    {
        exp.text = "Souls: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
    }
    void SetCanvas()
    {
        if (!Player.GetPlayer().Loaded)
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
    public void SoulsToAbility()
    {
        if (Player.GetPlayer().stats.Exp > Player.GetPlayer().stats.RequiredExp)
        {
            Player.GetPlayer().stats.Abilitypoints++;
            Player.GetPlayer().stats.Exp -= Player.GetPlayer().stats.RequiredExp;
            Player.GetPlayer().stats.RequiredExp = (int)(Player.GetPlayer().stats.RequiredExp*1.2f);
            lvMenuExp.text = "Souls: " + Player.GetPlayer().stats.Exp;
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
    private void EquipmentInventUp(ItemSlot.ItemSlotType type) {

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
    private void WeaponInventUp() {
        weaponInvent.SetActive(true);
    }
    private void ShieldInventUp()
    {
        shieldInvent.SetActive(true);
    }
    private void MaskInventUp()
    {
        maskInvent.SetActive(true);
    }
    private IEnumerator WaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        SetCanvas();

    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitCoroutine());
    }
}
