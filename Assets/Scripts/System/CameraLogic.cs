using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private Camera prespCamPrefab;
    private static Camera prespCam;
    [SerializeField] private Camera overheadCamera;
    [SerializeField] private GameObject canvas;
    private Vector3 delta;
    private Player body;
    private bool playerEnabled;
    private bool buttonOn;
    private static bool switchable;//write optimization later itll help in other places too
    public static UnityAction overHeadCamActive;
    public static Camera PrespCam { get => prespCam; set => prespCam = value; }
    public Player Body { get => body; set => body = value; }
    public static bool Switchable { get => switchable; set => switchable = value; }

    private void Awake()
    {
        //body.Delta = transform.position - body.transform.position;
    }
    public virtual void Start()
    {
        prespCam = prespCamPrefab;
        Player.onPlayerDeath += OverheadCam;
        Body = Player.GetPlayer();
        Player.onPlayerEnabled += CalculateDelta;
        //InvokeRepeating("", 1f, 1f);
    }
    void CalculateDelta()
    {
        playerEnabled = true;
        delta = transform.position - Body.transform.position;

    }
    // Update is called once per frame
    public virtual void Update()
    {
        CameraAi();
        if (switchable)
        {
            GetInput();
        }
    }

    void CameraAi()
    {
        if (playerEnabled)
        {
            transform.position = Body.transform.position + delta;
        }
    }
    void GetInput()
    {

        if (Input.GetAxis("L3") > 0.05 && !buttonOn)//Use AxisButton optimizeeeeeeeeeeeeeeeeeeeeee
        {

            buttonOn = true;
            if (overheadCamera.gameObject.activeSelf)
            {
                PrespheadCam();
            }
            else
            {
                OverheadCam();

            }
        }
        if (Input.GetAxis("L3") < 0.05)
        {
            buttonOn = false;
        }
    }
    private void OverheadCam()
    {
        if (overHeadCamActive != null)
            overHeadCamActive();
        overheadCamera.gameObject.SetActive(true);

        canvas.transform.SetParent(overheadCamera.transform);
        canvas.transform.localPosition = new Vector3(0, 0, 0);
        prespCam.gameObject.SetActive(false);
    }
    private void PrespheadCam()
    {
        prespCam.gameObject.SetActive(true);

        canvas.transform.SetParent(prespCam.transform);
        canvas.transform.localPosition = new Vector3(0, 0, 0);
        overheadCamera.gameObject.SetActive(false);

    }


    private void MoveIt(float x, float y)
    {

    }
}
