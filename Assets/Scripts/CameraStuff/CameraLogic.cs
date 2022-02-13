using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class CameraLogic : MonoBehaviour
{
    [SerializeField] private Camera prespCamPrefab;
    private static Camera prespCam;
    [SerializeField] private Camera overheadCamera;
    [SerializeField] private GameObject canvas;
     
    [SerializeField] private GameObject body;

    private static bool switchable;//write optimization later itll help in other places too

    public static UnityAction overHeadCamActive;
    public static Camera PrespCam { get => prespCam; set => prespCam = value; }
    public GameObject Body { get => body; set => body = value; }
    public static bool Switchable { get => switchable; set => switchable = value; }

    [SerializeField] private GameObject audioMaster;
    public virtual void Start()
    {
        prespCam = prespCamPrefab;

    }
}
