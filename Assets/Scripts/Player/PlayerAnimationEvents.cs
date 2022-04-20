using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerAnimationEvents : MonoBehaviour
{
    public static event UnityAction<float> kickback;

    [SerializeField] private float kickBack;
    [SerializeField] private float forwardStep;
    // Start is called before the first frame update
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
}
