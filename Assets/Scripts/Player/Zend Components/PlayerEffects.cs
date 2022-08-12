using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]private GameObject shadowShot;
    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject swordAura;
    [SerializeField] private GameObject swordAura2;
    [SerializeField] private GameObject fireTrailR;
    [SerializeField] private GameObject fireTrailL;

    public GameObject ShadowShot { get => shadowShot; set => shadowShot = value; }
    public GameObject Lightning { get => lightning; set => lightning = value; }
    public GameObject SwordAura { get => swordAura; set => swordAura = value; }
    public GameObject SwordAura2 { get => swordAura2; set => swordAura2 = value; }

    private void OnEnable() {
        PlayerInputs.energized += SwordAuraControl;
        PlayerInputs.strenghtened += SwordAuraControl2;
    }
    private void OnDisable() {
        PlayerInputs.energized -= SwordAuraControl;
        PlayerInputs.strenghtened -= SwordAuraControl2;
    }
    private void Start() {

    }
    private void SwordAuraControl(bool val) {
        SwordAura.SetActive(val);
        print("Touched");
    }
    private void SwordAuraControl2(bool val) {
        SwordAura2.SetActive(val);
    }
}
