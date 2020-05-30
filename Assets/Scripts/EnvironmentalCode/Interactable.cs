using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public enum InteractableType {Person,Item, Portal,Podium, Skull,LightBulbHolder,LightBulb }
    [SerializeField] private InteractableType type;

    private bool collected;
    [SerializeField]private bool collectible;

    public static event UnityAction<bool> sealJump;
    public static event UnityAction<ItemData> addItem;
    public static event UnityAction<int> skullCollected;
    public static event UnityAction<int> bulbCollected;
    public static event UnityAction<GameObject> checkForBulb;

    private void OnTriggerEnter(Collider other) {
        if (sealJump != null) {
            sealJump(true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetButtonDown("Circle")&&!collected) {
            if (collectible) {
                collected = true;
            }
            
            Interact();    
        }
    }
    private void OnTriggerExit(Collider other) {
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
                break;
            case InteractableType.Podium:
                GetComponent<Podium>().CheckToLite();
                break;
            case InteractableType.Skull:
                if (skullCollected != null) {
                    skullCollected(1);
                }
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
                
                Destroy(gameObject);
                break;
        }
    }
}
