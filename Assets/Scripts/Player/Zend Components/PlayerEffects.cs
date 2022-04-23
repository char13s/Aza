using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]private GameObject shadowShot;

    public GameObject ShadowShot { get => shadowShot; set => shadowShot = value; }

}
