﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTimer : MonoBehaviour {

	public float startValue;
	Vector3 startAngle;
	public float endValue;
	Vector3 endAngle;

	Vector3 currentAngle = new Vector3(0,0,0);

	public float totalPlayTime =300;
	float time;
	float timeSpent=0;

	//talking booleans
	bool oneMinLeft=true;
	bool twoMinLeft=true;


	// Use this for initialization
	void Start () {
	time = totalPlayTime;

		Vector3 startAngle = new Vector3(startValue,0,0);	
		Vector3 endAngle = new Vector3(endValue,0,0);
	}
	
	
	// Update is called once per frame
	void Update () {
		time =  time - Time.deltaTime;
		timeSpent = time/totalPlayTime;

		transform.rotation = Quaternion.Euler(startAngle);

		//Debug.Log(timeSpent);
		//Debug.Log(currentAngle.x);
		if(timeSpent > 0)
		{
			currentAngle.x = (timeSpent)*startValue + (1-timeSpent)*endValue;
			GetComponentInChildren<Light>().intensity=timeSpent;
		}
		else
		{
			currentAngle.x = endValue;
			GetComponentInChildren<Light>().intensity=0;
		}

		//transform.rotation = Quaternion.Lerp(Quaternion.Euler(startAngle),Quaternion.Euler(endAngle), timeSpent);
		//transform.eulerAngles = new Vector3(currentAngle.x, 0, 0);
		//Debug.Log(currentAngle.x);
		transform.eulerAngles = new Vector3 (currentAngle.x,0, 0);

		//Quaternion transform.Euler(currentAngle.x,0f,0f);

		if(time < 0)
		{
			//get the amount of fish caught, compare it to required amount and change scene(or something) according to outcome 
		}
		if(!GameManager.singleton.Islinear)
		//when there is two minutes left
		Debug.Log("timer:" + timeSpent);
		if(timeSpent <=0.4 && twoMinLeft)
		{
			twoMinLeft=false;
			GameManager.singleton.partner.
				GetComponent<PartnerSpeech>().PartnerSaysSomething(
				GameManager.singleton.partner.GetComponent<PartnerSpeech>().Time2MinLeft);
		}
		//when there is onr minutes left
		if(timeSpent <=0.2 && oneMinLeft)
		{
			oneMinLeft =  false;
			GameManager.singleton.partner.
				GetComponent<PartnerSpeech>().PartnerSaysSomething(
				GameManager.singleton.partner.GetComponent<PartnerSpeech>().Time1MinLeft);
		}
	}

	public float GetTimeSpent()
	{
		return timeSpent;
	}
}
