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

	void Movement () {
		verticalInput = Input.GetAxis("Vertical");
		if(Input.GetAxis("Vertical") != 0 && !outOfBounds)
		{
			GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * transform.forward * speed * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 1.0f)));
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

	public void BoatMovement () {
		if(Input.GetButton("Fire1") && !outOfBounds)
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