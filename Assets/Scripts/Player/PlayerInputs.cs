using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInputs : MonoBehaviour
{
    private Player player;
    private PlayerInput map;
    [SerializeField] private DarkPowerSet darkPowers;
    [SerializeField] private EquipmentObj relic;
    [SerializeField] private AbilityUIHolder relicUp;
    [SerializeField] private AbilityUIHolder relicDown;
    [SerializeField] private AbilityUIHolder relicRight;
    [SerializeField] private AbilityUIHolder relicLeft;
    public DarkPowerSet DarkPowers { get => darkPowers; set => darkPowers = value; }
    public EquipmentObj Relic { get => relic; set => relic = value; }// Need code to create relic on player in specific spot so it can be used or just carry them all will they really take up alot of data?
    #region Events
    public static event UnityAction nextLine;
    public static event UnityAction pause;
    public static event UnityAction close;
    public static event UnityAction<int> turnPage;
    #endregion

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Player>();
        map = GetComponent<PlayerInput>();
        DialogueManager.switchControls += SwitchMaps;
        GameManager.switchMap += SwitchMaps;
    }

    #region Base Controls
    private void OnMovement(InputValue value) {
        player.DisplacementV = value.Get<Vector2>();
    }
    private void OnAttack() {
        if (!player.SkillButton) {
            print("Square");
            player.Anim.SetTrigger("Attack");
        }
        else {
           player.SkillSquare();
        }
    }
    private void OnEnergy() {
        if (!player.SkillButton) {
            print("Triangle");
            darkPowers.Triangle();
        }
        else {
            player.SkillTriangle();
        }
    }
    private void OnJump() {
        if (!player.SkillButton) {
            //player.Jump();
            if (player.Grounded) {
                //player.Anim.SetTrigger("Jump");
            }
        }
        else {
            player.SkillX();
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
            player.SkillCircle();
        }
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
    private void OnSkillUp(InputValue value) {
        if (value.isPressed) {
            player.SkillButton = true;
            print("Locked or should be anyway");
        }
        else {
            player.SkillButton = false;
        }
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
        }
    }
}
