using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649
public class ThreeDCamera : CameraLogic
{
    
    //[SerializeField]private GameObject xZOrientationRef;
    private static Transform xZOrientation;
    private static ThreeDCamera instance;
    private Vector3 currentEulerAngles;
    private float maxXRotation=40;
    private float minXRotation = 10;
    public static Transform XZOrientation { get => xZOrientation; set => xZOrientation = value; }
    public static bool IsActive => instance!=null&&instance.isActiveAndEnabled;
    
    private Vector3 displacement;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Did someone put multiple " + GetType().Name + "'s in the scene?");
        instance = this;
        xZOrientation = new GameObject("xZOrienatation").transform;
        xZOrientation.transform.SetParent(transform);
         //xZOrientation= xZOrientationRef;
    }
    public override void Start()
    {
        
        base.Start();
        currentEulerAngles = transform.eulerAngles;
        currentEulerAngles.x = 10;
        transform.eulerAngles = currentEulerAngles;
    }

    public override void  Update()
    {
        base.Update();
        GetInput();
    }
    private void OnEnable()
    {
        //transform.rotation = Player.GetPlayer().transform.rotation; I need to set this when the camera is set active, but not before the player is active on main menu
    }
    
    void GetInput()
    {
        float x = Input.GetAxis("RightStickX");
        float y = Input.GetAxis("RightStickY");


        ApplyRotationOffset(x,y,ref currentEulerAngles);
        RotateCamera(x, y);
    }

    private float EnsureAngleIs0To360(float angle) {
        if (angle < 0)
            return (angle % 360) + 360;
        return angle % 360;
    }
    private void ApplyRotationOffset(float cameraHortizonal, float cameraVertical, ref Vector3 target) {
        if (Time.deltaTime == 0)
            return;
        float deltaFromInputX = cameraHortizonal * 55 * Time.deltaTime;
        float deltaFromInputY = cameraVertical * 55 * Time.deltaTime;

        target.y = EnsureAngleIs0To360(target.y+deltaFromInputX);
        target.x = EnsureAngleIs0To360(target.x + deltaFromInputY);
        //This works when minXRotation is 0 or negative and maxXRotation is 0 or positive
        //if (target.x > maxXRotation && target.x <= 180)
        //    target.x = maxXRotation;
        //else if (target.x < 360 +minXRotation && target.x > 180)
        //    target.x = 360 +minXRotation;
        target.x = Mathf.Clamp(target.x, minXRotation, maxXRotation);
    }
    private void RotateCamera(float x, float y)
    {

        
        transform.eulerAngles=currentEulerAngles;
        transform.position=Calculate3rdPersonCameraPosition(Body.transform.position,5,currentEulerAngles);     
        xZOrientation.eulerAngles = new Vector3(0,transform.eulerAngles.y,0); 
    }
    private Vector3 Calculate3rdPersonCameraPosition(Vector3 focusPosition, float distance, Vector3 eulerAngles) {
        
        float cosx = Mathf.Cos(Mathf.Deg2Rad * eulerAngles.x);
        float siny = Mathf.Sin(Mathf.Deg2Rad * eulerAngles.y);
        float sinx = Mathf.Sin(Mathf.Deg2Rad * eulerAngles.x);

        Vector3 offset = new Vector3(distance * -cosx * siny, -distance * -sinx, 0);

        float args = distance * distance * cosx * cosx;
        args *= 1 - siny * siny;
        offset.z = (eulerAngles.y < 270 && eulerAngles.y > 90) ? Mathf.Sqrt(args) : -Mathf.Sqrt(args);

        return focusPosition + offset;
    }
}
