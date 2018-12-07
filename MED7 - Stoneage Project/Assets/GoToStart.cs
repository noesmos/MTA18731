using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStart : MonoBehaviour {

	public float totalTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		totalTime = totalTime-Time.deltaTime;

		if(totalTime <0)
		{
			if( Input.GetButtonDown("Fire1"))
			{
				SceneManager.LoadScene("start", LoadSceneMode.Single);
			}
		}

	}
}
