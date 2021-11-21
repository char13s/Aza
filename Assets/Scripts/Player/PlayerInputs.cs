using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInputs : MonoBehaviour
{
    private Player player;
    private PlayerInput map;

    public static event UnityAction nextLine;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        map = GetComponent<PlayerInput>();
        DialogueManager.switchControls += SwitchMaps;
    }

    #region Base Controls
    #endregion
    private void OnMovement(InputValue value) {
        player.DisplacementV = value.Get<Vector2>();
    }
    #region Dialogue Controls
    private void OnNextLine() {
        nextLine.Invoke();
    }

    #endregion
    private void OnAttack() {
        print("Square");
        player.Anim.SetTrigger("Attack");
    }
    private void OnEnergy() {
        print("Triangle");
    }
    private void OnJump() {
        player.Jump();
    }
    private void OnAbility() {
        print("Circle");
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
