using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FloatingObject))]
public class BoatControllerScript : MonoBehaviour {

	public float speed = 0.2f;
	public float rotationSpeed = 160f;

	float verticalInput;
	float horizontalInput;
	float steerFactor;
	
	// Update is called once per frame
	void Update () {
		Movement();
		TestSteer();

		BoatMovement();
	}

	void Movement () {
		verticalInput = Input.GetAxis("Vertical");
		if(Input.GetAxis("Vertical") != 0)
		{
			GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * transform.forward * speed * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 1.0f)));
		}
	}

	void TestSteer () {
		horizontalInput = Input.GetAxis("Horizontal");
		steerFactor = Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime);
		transform.Rotate(0.0f, steerFactor, 0.0f);
	}

	public void BoatMovement () {
		if(Input.GetButton("Fire1"))
		{
			GetComponent<Rigidbody>().AddForce(transform.forward * speed * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 1.0f)));

			if (Vector3.Angle(transform.forward, Camera.main.transform.forward) > 10)
			{
				// The step size is equal to speed times frame time.
				float step = rotationSpeed * Time.deltaTime;

				Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), step, 0.0f);
				
				// calculate the Quaternion for the rotation
				Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDir), rotationSpeed * Time.deltaTime);
				
				//Apply the rotation 
				transform.rotation = rot;
			}
			//Debug.Log(Vector3.Angle(transform.forward, Camera.main.transform.forward));
		}
	}
}