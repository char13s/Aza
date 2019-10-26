using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingCamera : CameraLogic {
	private static Transform xZOrientation;
	private static Transform retical;

	public static Transform XZOrientation { get => xZOrientation; set => xZOrientation = value; }
	public static Transform Retical { get => retical; set => retical = value; }

	// Start is called before the first frame update
	private void Awake() {
		XZOrientation = new GameObject("xZOrienatation").transform;
		XZOrientation.transform.SetParent(transform);
		Retical = new GameObject("retical").transform;
		Retical.transform.SetParent(transform);
	}
	public override void Start() {
		base.Start();
	}

	// Update is called once per frame
	public override void Update() {
		base.Update();
		GetInput();
	}
	private void GetInput() {
		float y = Input.GetAxis("RightStickX") ;
		float x = Input.GetAxis("RightStickY") ;
		RotateCamera(x,y);
	}
	private void RotateCamera(float x, float y) {
		Vector3 rotation = new Vector3(x,y,0);
		transform.Rotate(rotation,Space.Self);
		xZOrientation.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

	}
}
