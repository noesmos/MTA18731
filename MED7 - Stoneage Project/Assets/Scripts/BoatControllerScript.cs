﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FloatingObject))]
public class BoatControllerScript : MonoBehaviour {

	public float speed = 15f;
	public float rotationSpeed = 0.1f;
	public Collider[] ignoreCollision;

	float verticalInput;
	float horizontalInput;
	float steerFactor;

	private bool outOfBounds = false;


	
	// Update is called once per frame
	void Update () {
		Movement();
		TestSteer();

		BoatMovement();
	}

	void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Bounds")
		{
			outOfBounds = true;
			StartCoroutine(forceMove());
			Debug.Log("You're out of bounds!!!");
		}
    }

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Bounds")
		{
			outOfBounds = false;
			Debug.Log("You're back in bounds!");
		}
	}
	void TestSteer () {
		if(!outOfBounds)
		{
			horizontalInput = Input.GetAxis("Horizontal");
			steerFactor = Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime);
			transform.Rotate(0.0f, steerFactor, 0.0f);
		}
	}
	void Movement () {
		verticalInput = Input.GetAxis("Vertical");
		if(Input.GetAxis("Vertical") != 0 && !outOfBounds)
		{
			float sinusoid = (Mathf.Sin(Time.time * 2) + 1) / 2;
			if(sinusoid < 0.2f)
			{
				sinusoid = 0.2f;
			}
			GetComponent<Rigidbody>().AddForce(transform.forward * speed * sinusoid);
			//Debug.Log(sinusoid);
		}
	}

	public void BoatMovement () {
		if(Input.GetButtonDown("Fire1") && !outOfBounds && !GameManager.singleton.pointingAtInteractable)
		{
			GameManager.singleton.paddle.GetComponent<AudioSource>().Play();
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().paddleAnimation(true);
		} else if(Input.GetButtonUp("Fire1") && !outOfBounds)
		{
			GameManager.singleton.paddle.GetComponent<AudioSource>().Stop();
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().paddleAnimation(false);
		}
		
		if(Input.GetButton("Fire1") && !outOfBounds)
		{
			float sinusoid = (Mathf.Sin(Time.time * 2.175f) + 1) / 2;
			if(sinusoid < 0.2f)
			{
				sinusoid = 0.2f;
			}

			if (Vector3.Distance(GameManager.singleton.currentPillar.transform.position, transform.position) > 150)
			{
				float wrongWayAngle = Vector3.SignedAngle(transform.forward, new Vector3(GameManager.singleton.currentPillar.transform.position.x, 0, GameManager.singleton.currentPillar.transform.position.z) - transform.position, Vector3.up);
				if(wrongWayAngle > 90)
				{
					// Right
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().wrongWay(true);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointRight(true);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointLeft(false);
				} else if (wrongWayAngle < -90) {
					// Left
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().wrongWay(true);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointLeft(true);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointRight(false);
				} else {
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().wrongWay(false);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointLeft(false);
					GameManager.singleton.partner.GetComponent<PartnerAnimator>().pointRight(false);
				}
			
			}

			if((!GameManager.singleton.pointingAtInteractable) && 
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().anim.GetCurrentAnimatorStateInfo(0).IsTag("default") || 
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().anim.GetCurrentAnimatorStateInfo(0).IsTag("paddling") ||
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().anim.GetCurrentAnimatorStateInfo(0).IsTag("pointLeft") ||
			GameManager.singleton.partner.GetComponent<PartnerAnimator>().anim.GetCurrentAnimatorStateInfo(0).IsTag("pointRight"))
			{

				GetComponent<Rigidbody>().AddForce(transform.forward * speed * sinusoid*Time.deltaTime);

				float rotationAngle = Vector3.Angle(transform.forward, Camera.main.transform.forward);

				if (rotationAngle > 5)
				{

					Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), Time.deltaTime * rotationAngle, 0.0f);
					
					// calculate the Quaternion for the rotation
					Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), rotationSpeed);
					
					//Apply the rotation 
					transform.rotation = rot;

				}
			}
			//Debug.Log(Vector3.Angle(transform.forward, Camera.main.transform.forward));
		}
	}

		IEnumerator forceRotate()
		{
			// The step size is equal to speed times frame time.
			float rotStep = rotationSpeed * Time.deltaTime;

			while (transform.forward != (new Vector3(0,transform.position.y,0) - transform.position).normalized)
			{
				Vector3 newDir = Vector3.RotateTowards(transform.forward, (new Vector3(0,transform.position.y,0) - transform.position), rotStep, 0.0f);
					
				// calculate the Quaternion for the rotation
				Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), rotationSpeed * Time.deltaTime);
				
				//Apply the rotation 
				transform.rotation = rot;

				Debug.Log(transform.forward + " ---- " + (new Vector3(0,transform.position.y,0) - transform.position).normalized);

				yield return null;
			}
			
			yield return null;
		}

	IEnumerator forceMove()
    {
		// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;
		float rotStep = rotationSpeed * Time.deltaTime;
		float dist = Vector3.Distance(transform.position, new Vector3(0,transform.position.y,0));

		

		while (transform.position != new Vector3(0,transform.position.y,0))
		{
			// Move our position a step closer to the target.
        	transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,transform.position.y,0), step);
			
			Vector3 newDir = Vector3.RotateTowards(transform.forward, (new Vector3(0,transform.position.y,0) - transform.position), rotStep, 0.0f);
				
			// calculate the Quaternion for the rotation
			Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), rotationSpeed * Time.deltaTime);
			
			//Apply the rotation 
			transform.rotation = rot;

			if((Vector3.Distance(transform.position, new Vector3(0,transform.position.y,0)) < (dist - 40)))
			{
				StopAllCoroutines();
			}
			//Debug.Log(dist + " ---- " + (new Vector3(0,transform.position.y,0) - transform.position).normalized);
			
			yield return null;
		}
    }
}