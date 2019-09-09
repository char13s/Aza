using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableAza : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 displacement;
    private int animations;
    private Animator anim;
    public int Animations { get => animations; set { animations = value;anim.SetInteger("Animations",animations); } }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        //RotatePlayer(x, y);
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        displacement = ThreeDCamera.XZOrientation.TransformDirection(displacement);
        
        MoveIt(x, y);
    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            
            Animations = 1;
            transform.position += displacement * moveSpeed * Time.deltaTime;
            if (Vector3.SqrMagnitude(displacement) > 0.01f)
            {
                transform.forward = displacement;
            }
        }
        else
        {
            Animations = 0;
            
        }
    }
}
