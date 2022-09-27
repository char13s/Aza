using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyObjects : MonoBehaviour
{
    /// <summary>
    /// For objects parented to player object mesh
    /// </summary>
    [Header("Blast points")]
    [SerializeField] private GameObject rightHandBlastPoint;
    [Header("General points")]
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject literalBody;
    [SerializeField] private GameObject hair;
    [SerializeField] private GameObject eyes;
    //[SerializeField] private Game
    
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    

    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject farHitPoint;
    [SerializeField] private GameObject battleCamTarget;

    [SerializeField] private GameObject centerPoint;
    [SerializeField] private GameObject centerBodyPoint;
    [SerializeField] private GameObject highPoint;
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private GameObject jumpPoint;
    [SerializeField] private SlashSpawner slashSpawner;

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject demonScabbard;
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject angelScabbard;
    [SerializeField] private GameObject angelSwordSide;

    [SerializeField] private Material normalState;
    [SerializeField] private Material energizedState;
    public GameObject Body { get => Body1; set => Body1 = value; }
    public GameObject Body1 { get => body; set => body = value; }
    public GameObject BattleCamTarget { get => battleCamTarget; set => battleCamTarget = value; }
    public GameObject FarHitPoint { get => farHitPoint; set => farHitPoint = value; }
    public GameObject LeftHand { get => leftHand; set => leftHand = value; }
    public GameObject RightHand { get => rightHand; set => rightHand = value; }
    public SlashSpawner SlashSpawner { get => slashSpawner; set => slashSpawner = value; }
    public GameObject Eyes { get => eyes; set => eyes = value; }
    public GameObject LiteralBody { get => literalBody; set => literalBody = value; }
    public GameObject Hair { get => hair; set => hair = value; }
    public GameObject Shield { get => shield; set => shield = value; }
    public GameObject DemonSword { get => demonSword; set => demonSword = value; }
    public GameObject RightHandBlastPoint { get => rightHandBlastPoint; set => rightHandBlastPoint = value; }

    private void Vanish(bool val) {
        body.SetActive(val);
    }
    private void OnEnable() {
        PlayerInputs.transformed += TransformControl;
    }
    private void OnDisable() {
        PlayerInputs.transformed -= TransformControl;
    }
    private void TransformControl(bool val) {
        if (val) {
            LiteralBody.GetComponent<SkinnedMeshRenderer>().material = energizedState;
        }
        else {
            LiteralBody.GetComponent<SkinnedMeshRenderer>().material = normalState;
        }
    }
}
