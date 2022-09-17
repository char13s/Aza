using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreeFallMovement : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private CharacterController charCon;

    private void OnEnable() {
        SwitchToFallGame.switchCam += Vcam;
    }
    private void OnDisable() {
        SwitchToFallGame.switchCam -= Vcam;
    }
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = new Quaternion(87, 0, 0, 0);
        charCon = GetComponent<CharacterController>();
    }
    private void FixedUpdate() {
        charCon.Move(new Vector3(0, -gravity, 0) * Time.deltaTime);
    }
    private void Vcam(int val) {
        vcam.m_Priority = val;
    }
}
