using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Podium : MonoBehaviour
{
    [SerializeField] private bool lit;
    [SerializeField] private GameObject skull;
    private Player pc;

    public static event UnityAction lite;
    public static event UnityAction<int> skullUsed;
    private void Awake() {
        pc = Player.GetPlayer();
    }

    public void CheckToLite() {
        if (!lit && pc.SkullMask > 0) {
            lit = true;
            if (lite != null) {
                lite();
            }
            if (skullUsed != null) {
                skullUsed(-1);
            }
            skull.SetActive(true);
        }
    }
}
