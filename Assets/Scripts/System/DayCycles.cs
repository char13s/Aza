using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycles : MonoBehaviour
{
    public float minutesInDay =1.0f;

	private float timer;
	private float percentageDay;
	private float turnSpeed;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		CheckTime ();
		UpdateLights ();
		turnSpeed = 360.0f / (minutesInDay * 60.0f) * Time.deltaTime;
		transform.RotateAround (transform.position, transform.right, turnSpeed);
		//Debug.Log (percentageDay);
	}
	void UpdateLights()
	{
		Light l=GetComponent<Light>();
		if(IsNight())
		{if (l.intensity>0.0f){
				l.intensity-=0.05f;
			}
		}
		else{
			if(l.intensity<1.0f){
				l.intensity+=0.05f;
			}
		}

	}
	bool IsNight(){
		bool c = false;
		if (percentageDay > 0.5f) {

			c = true;
		}
		return c;
	}
	void CheckTime(){
		timer += Time.deltaTime;
		percentageDay = timer / (minutesInDay * 60.0f);
		if (timer > minutesInDay * 60.0f) {
			timer = 0.0f;
		}
	}
}
