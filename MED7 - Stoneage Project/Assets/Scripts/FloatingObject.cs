using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class FloatingObject : MonoBehaviour {

	public float waterLevel = 0.0f;
	public float floatThreshold = 2.0f;
	public float waterDensity = 0.125f;
	public float downForce = 5.0f;
	
	float forceFactor;
	Vector3 floatForce;

	// Update is called once per frame
	void FixedUpdate () {
		if(Time.time < 7)
		{
			forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

			if (forceFactor > 0.0f)
			{
				floatForce = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
				floatForce += new Vector3(0.0f, -downForce, 0.0f);
				GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
			}
		}
		else
		{
			GetComponent<Rigidbody>().constraints = 
				RigidbodyConstraints.FreezePositionY | 
				RigidbodyConstraints.FreezeRotationX | 
				RigidbodyConstraints.FreezeRotationY | 
				RigidbodyConstraints.FreezeRotationZ;
		}

		
	}
}
