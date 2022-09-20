using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FallScriptCams : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        Player.onPlayerEnabled += SendCam;
    }
    private void SendCam() {
        Player.GetPlayer().FreeFallMode.FreeCam = cam;
    }
}
