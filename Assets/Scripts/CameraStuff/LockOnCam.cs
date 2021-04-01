using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LockOnCam : MonoBehaviour
{

    private void Awake()
    {
        //Player.lockOn += ChangeTarget;
        Player.notAiming += BackToPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ChangeTarget()
    {
        if (Player.GetPlayer().BattleMode.EnemyTarget != null) { 
            GetComponent<CinemachineVirtualCamera>().m_LookAt = Player.GetPlayer().BattleMode.EnemyTarget.transform;
            
        }
    }
    private void BackToPlayer() {
        GetComponent<CinemachineVirtualCamera>().m_LookAt = Player.GetPlayer().transform;

    }
    
}
