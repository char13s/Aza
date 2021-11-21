using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ThreeDCamera : CameraLogic
{ 
    //[SerializeField]private GameObject xZOrientationRef;
    private static Transform xZOrientation;
    private static GameObject retical;
    private static ThreeDCamera instance;
    //[SerializeField] private GameObject reticalObject;
    [SerializeField] private GameObject bodyTarget;
    private Vector3 currentEulerAngles;
    [SerializeField]private float maxXRotation;
    [SerializeField]private float minXRotation;
    private float distanceFromZend=5;
	private Vector3 target;
    private Vector3 offset;
	private bool aiming;
    private AudioSource audio;
    [SerializeField] private Vector3 aimingPosition;

    private AxisButton R3 = new AxisButton("R3");

    public Transform XZOrientation { get => xZOrientation; set => xZOrientation = value; }
    public static bool IsActive => instance!=null&&instance.isActiveAndEnabled;

    public static GameObject Retical { get => retical; set => retical = value; }

    // Start is called before the first frame update
   
}
