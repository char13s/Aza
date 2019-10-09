using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableAza : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 displacement;
    private int animations;
    private Animator anim;
    private static PlayableAza instance;
    [SerializeField] private GameObject azaBow;
    internal Stats stats = new Stats();
    public int Animations { get => animations; set { animations = value;anim.SetInteger("Animations",animations); } }
    public static PlayableAza GetAza() => instance.GetComponent<PlayableAza>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Attack();
    }
    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        //RotatePlayer(x, y);
        //displacement = Vector3.Normalize(new Vector3(x, 0, y));
        //displacement = ThreeDCamera.XZOrientation.TransformDirection(displacement);
        
        MoveIt(x, y);
    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            
            Animations = 1;
            //transform.position += displacement * moveSpeed * Time.deltaTime;
            //if (Vector3.SqrMagnitude(displacement) > 0.01f)
            //{
                //transform.forward = displacement;
            //}
        }
        else
        {
            Animations = 0;
            
        }
    }
    private void Attack() {

        if (Input.GetButton("Square")) {
            Animations = 4;
        }
        if (Input.GetButtonUp("Square"))
        {
            Animations = 0;
        }
    }
}
