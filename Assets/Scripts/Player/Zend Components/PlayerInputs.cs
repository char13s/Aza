using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInputs : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    private PlayerInput map;
    private Animator anim;
    private Vector2 rotationLook;
    #region Extra attack logic
    bool holdAttack;
    #endregion

    [SerializeField] private DarkPowerSet darkPowers;
    [SerializeField] private EquipmentObj relic;
    private EnemyTimelineTriggers trigger;
    [SerializeField] private AbilityUIHolder relicUp;
    [SerializeField] private AbilityUIHolder relicDown;
    [SerializeField] private AbilityUIHolder relicRight;
    [SerializeField] private AbilityUIHolder relicLeft;
    public DarkPowerSet DarkPowers { get => darkPowers; set => darkPowers = value; }
    public EquipmentObj Relic { get => relic; set => relic = value; }// Need code to create relic on player in specific spot so it can be used or just carry them all will they really take up alot of data?
    public Vector2 RotationLook { get => rotationLook; set => rotationLook = value; }
    #region Events
    public static event UnityAction nextLine;
    public static event UnityAction pause;
    public static event UnityAction close;
    public static event UnityAction<int> turnPage;
    public static event UnityAction rotate;
    public static event UnityAction<bool> transformed;
    public static event UnityAction<bool> energized;
    public static event UnityAction<bool> strenghtened;
    #endregion
    private void OnEnable() {
        DialogueManager.switchControls += SwitchMaps;
        GameManager.switchMap += SwitchMaps;
        EnemyTimelineTriggers.sendTrigger += RecieveTrigger;
    }
    private void OnDisable() {
        DialogueManager.switchControls -= SwitchMaps;
        GameManager.switchMap -= SwitchMaps;
        EnemyTimelineTriggers.sendTrigger -= RecieveTrigger;
    }
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        map = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        
    }

    #region Base Controls
    private void OnMovement(InputValue value) {
        playerMovement.Displacement = value.Get<Vector2>();
    }
    private void OnAttack(InputValue value) {
        if (value.isPressed) {
            if (player.Energized && player.Strenghtened) { 
            //Well dont know what to do with that....
            }
            if (!player.Energized&&player.Strenghtened) {
                player.Anim.SetTrigger("HeavyAttack");
            }
            else if (player.Energized && !player.Strenghtened) {
                player.Anim.SetTrigger("EnergyAttack");
            }
            else {
                print("Square");
                player.Anim.SetTrigger("Attack");
                player.Attack = true;
            }
        }
        /*else if (holdAttack) {
            print("rELEASE");
            player.Anim.SetTrigger("Attack");
            holdAttack = false;
        }*/
    }
    private void OnHoldAttack(InputValue value) {
        /*if (value.isPressed) {
            holdAttack = true;
            print("hOLD");
            player.Anim.SetTrigger("HoldAttack");
        }*/
    }
    private void OnEnergy() {
        if (!player.SkillButton) {
            print("Triangle");
            /*if (!player.Attack) {
                relic.Triangle();
            }*/
            if (trigger != null) {
                trigger.PlayTimeline();
            }
        }
        else {

        }
    }
    private void OnJump(InputValue value) {
        if (!player.SkillButton) {
            if (player.CombatAnimations == 0) {
                playerMovement.IsJumpPressed = true;
            }
        }
        else {

        }
        if (!value.isPressed) {
            playerMovement.IsJumpPressed = false;
        }
    }
    private void OnAbility(InputValue value) {
        if (!player.SkillButton) {
            print("Circle");
            if (value.isPressed) {
                print("Circle has been pressed");
                if (relic != null) {
                    Relic.Circle();
                }
            }
            else {
                print("Circle has been released");
                Relic.CircleReleased();
            }
        }
        else {

        }
        print("ran");
        //transform.position = transform.position+ new Vector3(0,0, 1);
    }
    private void OnLockOn(InputValue value) {
        if (value.isPressed) {
            player.TargetingLogic(true);
            print("Locked or should be anyway");
        }
        else {
            player.TargetingLogic(false);
        }
    }
    private void OnSkillUp(InputValue value) {//R2
        if (value.isPressed) {
            player.Energized = true;
            energized.Invoke(true);
            Debug.Log("energy");
        }
        else {
            player.Energized = false;
            energized.Invoke(false);
        }

    }
    private void OnStrenghtened(InputValue value) {//L2
        if (value.isPressed) {
            player.Strenghtened = true;
            strenghtened.Invoke(true);
            Debug.Log("strength");
        }
        else {
            player.Strenghtened = false;
            strenghtened.Invoke(false);
        }

    }

    private void OnTransform() {
        if (!player.PoweredUp) {
            player.PoweredUp = true;
            player.Effects.Lightning.SetActive(true);
            player.Anim.SetTrigger("Transform");
            print("Super Zend!");
        }
        else {
            player.Effects.Lightning.SetActive(false);
            player.PoweredUp = false;
            print("Oh no");
        }
        transformed.Invoke(player.PoweredUp);
    }
    private void OnDUp() {
        //Relic = relicUp.Relic;
        Debug.Log(Relic); ;
    }
    private void OnDDown() {
        //Relic = relicDown.Relic;
        Debug.Log(Relic); ;
    }
    private void OnDLeft() {
        //Relic = relicLeft.Relic;
        Debug.Log(Relic); ;
    }
    private void OnDRight() {
        //Relic = relicRight.Relic;
        Debug.Log(Relic); ;
    }
    private void OnLook(InputValue value) {
        //print("Looking");
        RotationLook = value.Get<Vector2>();
    }
    #endregion

    #region Dialogue Controls
    private void OnNextLine() {
        nextLine.Invoke();
    }
    #endregion
    #region Pause Controls

    private void OnPause() {
        pause.Invoke();
        print("pause");
    }
    private void OnNextPage() {
        turnPage.Invoke(1);
        print("next page");
    }
    private void OnPreviousPage() {
        turnPage.Invoke(-1);
    }
    private void OnClose() {
        //close.Invoke();

    }
    #endregion
    private void SwitchMaps(int val) {
        switch (val) {
            case 0:
                map.SwitchCurrentActionMap("Default Controls");
                print("Switched to default controls");
                break;
            case 1:
                map.SwitchCurrentActionMap("PauseControls");
                print("Switched to pause controls");
                break;
            case 2:
                map.SwitchCurrentActionMap("Air Controls");
                break;
            case 3:
                map.SwitchCurrentActionMap("Timeline Controls");
                break;
            case 4:
                map.SwitchCurrentActionMap("Dialogue Controls");
                break;
            case 5:
                map.SwitchCurrentActionMap("");
                break;
        }
    }
    private void RecieveTrigger(EnemyTimelineTriggers obj) {
        trigger = obj;
    }
}
