using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCatcher : MonoBehaviour {


	public GameObject torsk;
	public GameObject eel;

	bool canFish;
	string fishingArea;

	GameObject fishingAreaObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
		if(other.tag == "TorskArea" || other.tag == "EelArea")
		{
			canFish = true;
			fishingArea = other.tag;
			fishingAreaObject = other.gameObject;
			Debug.Log("you are now in the "+fishingArea);
		}
		if(other.tag == "ertebølle")
		{
			GameManager.singleton.hook.GetComponent<SelectTool>().ShowTool();
			GameManager.singleton.eeliron.GetComponent<SelectTool>().ShowTool();
		}
    }

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "TorskArea" || other.tag == "EelArea")
		{
			canFish = false;
			fishingArea = "";
		}
		Debug.Log("you are now out of fishing area");
	}

	public void startFishing(string tool)
	{
		Debug.Log("trying to fish");
		if(canFish)
		{
			if(tool == "hook" && fishingArea == "TorskArea")
			{
				Debug.Log("caugth a torsk");
				//instatiate a fish in the boay
				Instantiate(torsk,transform.position, transform.rotation);
				//remove a fish from the ocean
				fishingAreaObject.GetComponent<FishContent>().RemoveFish();
			}
			if(tool == "eeliron" && fishingArea == "EelArea")
			{
				Debug.Log("caugth a eel");
				//instatiate a fish in the boay
				Instantiate(eel,transform.position, transform.rotation);
				//remove a fish from the ocean
				fishingAreaObject.GetComponent<FishContent>().RemoveFish();
			}
			else
			{
				//partner should make a comment
			}
		}

	}


}
