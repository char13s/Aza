using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class FreeFallZend : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float MoveSpeed;
    private float normalSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private CinemachineStateDrivenCamera vcam;
    private bool cantGoUp;
    private bool cantGoDown;
    private bool cantGoLeft;
    private bool cantGoRight;

    private int animations;
    private bool hit;

    private bool falling;
    private bool preparingToLand;
    private Vector3 displacement;

    private Rigidbody rbody;
    private Animator anim;
    private static FreeFallZend instance;

    public static UnityAction<Vector3,bool> landed;
    public static event UnityAction<int> diving;
    public GameObject Body { get => body; set => body = value; }
    public GameObject CenterPoint { get => centerPoint; set => centerPoint = value; }
    public bool HitGetter { get => hit; set { hit = value;anim.SetBool("Hit", hit); } }

    public int Animations { get => animations; set { animations = value;anim.SetInteger("Animations", animations); } }

    public bool Falling { get => falling; set { falling = value; if (falling) { if (diving != null) { diving(1); } } } }

    public static FreeFallZend GetFreeFallingZend() => instance.GetComponent<FreeFallZend>();
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        rbody =GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //WallDetector.up += UpDetector;
        //WallDetector.down += DownDetector;
        //WallDetector.left += LeftDetector;
        //WallDetector.right += RightDetector;
        GameController.onNewGame += StartGame;

    }
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (preparingToLand) {
            transform.position += transform.up * Time.deltaTime * -MoveSpeed;
        }
    }
    private void StartGame() {
        Falling = true;
    }
    private void ReadyToLand() {
        Animations = 1;
        transform.rotation = new Quaternion(0,0,0,0);
        Falling = false;
        preparingToLand = true;
        MoveSpeed = 30;
        
    }
    private void GetInput() {
        Movement();
        if (Falling) { 
        transform.position += transform.forward*Time.deltaTime*-MoveSpeed;
        if (Input.GetButton("Circle")) {
            MoveSpeed = boostSpeed;
                Animations = 2;
        }
        else { MoveSpeed = normalSpeed;
                Animations = 0;
            }
        }
    }
    private void UpDetector(bool val) {
        cantGoUp = val;
    }
    private void DownDetector(bool val) {
        cantGoDown = val;
    }
    private void RightDetector(bool val) {
        cantGoRight = val;
    }
    private void LeftDetector(bool val) {
        cantGoLeft = val; 
    }

    private void Movement() {
        float x = Input.GetAxisRaw("Horizontal") * 0.05f * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * 0.05f * Time.deltaTime;
        if (x > 0) {
            if (cantGoRight) {x = 0;

            }
            
            Animations = 3;
        }
        if (x < 0) {
            if (cantGoLeft) {x = 0;

            }
            
            Animations = 4;
        }
        if (x == 0) {
            Animations = 0;
        }
        if (y < 0) {
            if (cantGoDown) { y = 0;

            }
           
        }
        if (y > 0) {
            if (cantGoUp) {y = 0;

            }
            
        }

        displacement = Vector3.Normalize(new Vector3(-x,0 ,-y));
        transform.position += displacement * speed * Time.deltaTime;
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wall")&&!HitGetter) {
            Player.GetPlayer().stats.HealthLeft -= 5;
            HitGetter = true;
            Instantiate(hitEffect,CenterPoint.transform);
            StartCoroutine(Hit());
        }
        if (other.gameObject.layer==11) {
            //Instantiate(hitEffect, transform.position,Quaternion.identity);
            
            vcam.Priority = 0;
            if (landed != null) {
                landed(transform.position,false);
            }
            gameObject.SetActive(false);
        }
    }
    private IEnumerator Hit() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        HitGetter = false;
    }
}
