using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerAnimationEvents : MonoBehaviour
{
    #region Events
public static event UnityAction<float> kickback;
    #endregion
    #region variables
[SerializeField] private float kickBack;
    [SerializeField] private float forwardStep;
    #endregion
    #region Outside Scripts
    PlayerBodyObjects bodyObjects;
    #endregion



    // Start is called before the first frame update
    private void Start() {
        bodyObjects = GetComponent<PlayerBodyObjects>();
    }
    #region MOvement
    public void KickBack() {//code for quick back up
        kickback.Invoke(kickBack);
    }
    public void RollForward() {
        kickback.Invoke(-forwardStep);
    }
    #endregion

    #region Attack related
    public void SetAttackDelay() { 
        
    }
    public void CanSwitchToShoot() { 
    
    }
    public void SetNextAttack() { 
    
    }
    #endregion
    #region Effects
    public void BodyOn() {
        bodyObjects.Body.gameObject.SetActive(true);
    }
    public void BodyOff() {
        bodyObjects.Body.gameObject.SetActive(false);
    }
    #endregion
}
