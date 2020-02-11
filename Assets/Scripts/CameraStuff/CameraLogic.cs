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
     
    private Vector3 delta;
    [SerializeField] private GameObject body;
    private bool playerEnabled;
    private bool buttonOn;
    private static bool switchable;//write optimization later itll help in other places too

    public static UnityAction overHeadCamActive;
    public static Camera PrespCam { get => prespCam; set => prespCam = value; }
    public GameObject Body { get => body; set => body = value; }
    public static bool Switchable { get => switchable; set => switchable = value; }
    AxisButton L3 = new AxisButton("R3");
    [SerializeField] private GameObject audioMaster;
    private AudioSource mainAudio;
    private void Awake()
    {
		Player.aiming += Aiming;
        //body.Delta = transform.position - body.transform.position;
    }
    public virtual void Start()
    {
        mainAudio = GetComponentInChildren<AudioSource>();
        prespCam = prespCamPrefab;
        //Player.onPlayerDeath += OverheadCam;
        //Body = Player.GetPlayer();
        
        Player.onPlayerEnabled += CalculateDelta;
        Player.onCharacterSwitch += SwitchTarget;
        //InvokeRepeating("", 1f, 1f);
    }
    void CalculateDelta()
    {
       
        //playerEnabled = true;
        //delta = transform.position - Body.transform.position;
    }
    
    // Update is called once per frame
    public virtual void Update()
    {
        //CameraAi();
        /*if(switchable)
            GetInput();*/
        
    }
    private void ShowNavi()
    {

        NavMeshTriangulation nav = NavMesh.CalculateTriangulation();
        Mesh mesh = new Mesh();
        mesh.vertices = nav.vertices;
        mesh.triangles = nav.indices;

    }
    private void CameraAi()
    {
        if (playerEnabled)
        {
            transform.position = Body.transform.position + delta;
        }
    }
    private void SwitchTarget() {
        //Body = PlayableAza.GetAza();

    }
	private void Aiming() {

		//OverheadCam();
	}
	private void NotAiming() {

		//PrespheadCam();
	}
    void GetInput()
    {
        if (Input.GetButtonDown("L3"))
        {


            if (overheadCamera.gameObject.activeSelf)
            {
                PrespheadCam();
            }
            else
            {
                OverheadCam();
            }
        }
    }
    private void OverheadCam()
    {
        overheadCamera.gameObject.SetActive(true);
        audioMaster.transform.SetParent(overheadCamera.transform);
        canvas.transform.SetParent(overheadCamera.transform);
        canvas.transform.localPosition = new Vector3(0, 0, 0);
        prespCam.gameObject.SetActive(false);
    }
    private void PrespheadCam()
    {
        prespCam.gameObject.SetActive(true);
        audioMaster.transform.SetParent(prespCam.transform);
        canvas.transform.SetParent(prespCam.transform);
        canvas.transform.localPosition = new Vector3(0, 0, 0);
        overheadCamera.gameObject.SetActive(false);

    }
}
