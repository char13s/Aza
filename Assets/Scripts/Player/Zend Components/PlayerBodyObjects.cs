using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyObjects : MonoBehaviour
{
    /// <summary>
    /// For objects parented to player object mesh
    /// </summary>
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject literalBody;
    [SerializeField] private GameObject hair;
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

    [SerializeField] private GameObject demonScabbard;
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
            literalBody.GetComponent<SkinnedMeshRenderer>().material = energizedState;
        }
        else {
            literalBody.GetComponent<SkinnedMeshRenderer>().material = normalState;
        }
    }
}
