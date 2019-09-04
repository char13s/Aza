using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private float scale = 0.001f;
    private float zoom = 0;
    private Vector3 delta;
    private Player body;
    private bool playerEnabled;
    // Start is called before the first frame update
    private void Awake()
    {
        //body.Delta = transform.position - body.transform.position;
    }
    void Start()
    {

        body = Player.GetPlayer();
        Player.onPlayerEnabled += CalculateDelta;
        //InvokeRepeating("", 1f, 1f);
    }
    void CalculateDelta()
    {
        playerEnabled = true;
        delta = transform.position - body.transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {
        CameraAi();



        //Zoom();
        //FollowPlayer();
        //RotateCamera();

    }
    public void Zoom()
    {
        zoom += Input.mouseScrollDelta.y * scale * 10 * Time.deltaTime;
        if ((Mathf.Abs(body.transform.position.z - transform.position.z)) > 0.95 && zoom > 0)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zoom);
        }
        if (((Mathf.Abs(body.transform.position.z - transform.position.z))) < 1.65 && zoom < 0)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zoom);
        }



    }
    void CameraAi()
    {
        if (playerEnabled)
        {
            
                transform.position = body.transform.position + delta;
            
        }
    }
    void RotateCamera()
    {
        float h = Input.GetAxis("Mouse X");
        transform.RotateAround(body.transform.position, body.transform.up, h * 30 * Time.deltaTime);
    }
    void FollowPlayer()
    {
        if (!body.Attacking)
        {
            float x = Input.GetAxisRaw("Horizontal") * 5 * Time.deltaTime;
            float y = Input.GetAxisRaw("Vertical") * 5 * Time.deltaTime;
            MoveIt(x, y);
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal") * 20 * Time.deltaTime;
            float y = Input.GetAxisRaw("Vertical") * 60 * Time.deltaTime;
        }



    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 && !body.Wall || y != 0 && !body.Wall)
        {

            //body.Moving = true;

        }
        else
        {
            //body.Moving = false;
        }
        if (!body.Wall)
        {
            transform.position += new Vector3(x, 0, y);
        }
    }
}
