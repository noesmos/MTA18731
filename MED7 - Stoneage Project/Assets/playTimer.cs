using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTimer : MonoBehaviour {

	public float startAngle;
	public float endAngle;

	Vector3 currentAngle = new Vector3(0,0,0);

	public float totalPlayTime =300;
	float time;
	float timeSpent=0;

	// Use this for initialization
	void Start () {
	time = totalPlayTime;	
	}
	
	// Update is called once per frame
	void Update () {
		time =  - Time.deltaTime;

		timeSpent = time/totalPlayTime;
		Debug.Log(timeSpent);
		currentAngle.x = (timeSpent * startAngle) + ((1-timeSpent)*endAngle);
		//Debug.Log(currentAngle.x);
		transform.eulerAngles = currentAngle;

		if(time < 0)
		{
			//get the amount of fish caught, compare it to required amount and change scene(or something) according to outcome 
		}
	}
}
