using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public enum InteractableType {Person,Item, Portal,Podium, Skull,LightBulbHolder,LightBulb }
    [SerializeField] private InteractableType type;

    private bool collected;
    private bool touched;
    [SerializeField]private bool collectible;

    public static event UnityAction<bool> sealJump;
    public static event UnityAction<ItemData> addItem;
    public static event UnityAction<int> skullCollected;
    public static event UnityAction<int> bulbCollected;
    public static event UnityAction<GameObject> checkForBulb;
    public static event UnityAction soundOff;
    public static event UnityAction endDemo;
    public static event UnityAction saveGame;
    private void OnTriggerEnter(Collider other) {
        if (sealJump != null) {
            sealJump(true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetButtonDown("Circle")&&!collected&&!touched) {
            if (collectible) {
                collected = true;
            }
            touched = true;
            StartCoroutine(Untouch());
            Interact();    
        }
    }
    private void OnTriggerExit(Collider other) {
        if (sealJump != null) {
            sealJump(false);
        }
    }
    private void OnDestroy() {
        if (sealJump != null) {
            sealJump(false);
        }
    }
    private void Interact() {
        switch (type) {
            case InteractableType.Person:
                GetComponent<SceneDialogue>().enabled=true;
                break;
            case InteractableType.Item:
                ItemData data=GetComponent<Items>().data;
                if (addItem != null) {
                    addItem(data);
                }
                Destroy(gameObject);
                break;
            case InteractableType.Portal:
                GetComponent<LevelObject>().ActivateLevel();
                if (saveGame != null) {
                    saveGame();
                }
                break;
            case InteractableType.Podium:
                GetComponent<Podium>().CheckToLite();
                break;
            case InteractableType.Skull:
                if (skullCollected != null) {
                    skullCollected(1);
                }
                if (soundOff != null) {
                    soundOff();
                }
                if (endDemo != null) {
                    endDemo();
                }
                Debug.Log("SKull Collected");
                break;
            case InteractableType.LightBulbHolder:
                if (checkForBulb != null) {
                    checkForBulb(gameObject);
                }
                Debug.Log("HolderChecked");
                break;
            case InteractableType.LightBulb:
                if (bulbCollected != null) {
                    bulbCollected(1);
                }
                break;
                
        }
        if (collectible) {
            Destroy(gameObject);
        }
    }
    private IEnumerator Untouch() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        touched = false;
    }
}
