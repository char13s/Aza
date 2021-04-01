using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject riseHitBox;
    [SerializeField] private GameObject knockAwayBox;
    [SerializeField] private GameObject slamHitBox;

    // Start is called before the first frame update
    void Start()
    {
        SummonSword.activate += Sword;
        //BasicAttack.hitBox += HitBox;
        KnockAwayHitBox.hitBox += KnockAwayBox;
        RiseHitBox.hitBox += RiseBox;
        SlamHitBox.hitBox += SlamBox;
    }

    private void Sword(bool val) {
        demonSword.SetActive(val);
    }
    #region HitBoxes
    private void RiseBox(bool val) {
        riseHitBox.SetActive(val);
    }
    private void KnockAwayBox(bool val) {
        knockAwayBox.SetActive(val);
    }
    private void SlamBox(bool val) {
        slamHitBox.SetActive(val);
    }
    /*private void HitBox(bool val) {
        hitBox.SetActive(val);
    }*/
    #endregion
}
