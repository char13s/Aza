using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.UI;
#pragma warning disable 0649
public class SavePoint : InteractableBase
{
    public static event UnityAction saveUp;
    public static event UnityAction<bool> pauseGame;

    [SerializeField] private GameObject particles;
    public override void Interact() {
        if (saveUp != null) {
            saveUp();
        }
        if (pauseGame != null) {
            pauseGame(true);
        }
    }

}
