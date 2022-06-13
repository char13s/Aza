using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]private GameObject shadowShot;
    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject fireAura;
    [SerializeField] private GameObject fireTrailR;
    [SerializeField] private GameObject fireTrailL;

    public GameObject ShadowShot { get => shadowShot; set => shadowShot = value; }
    public GameObject Lightning { get => lightning; set => lightning = value; }
}
