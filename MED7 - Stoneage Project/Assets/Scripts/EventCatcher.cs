using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCatcher : MonoBehaviour {

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
		//when you enter a fishing area
		if(other.tag == "TorskArea" || other.tag == "EelArea")
		{
			canFish = true;
			fishingArea = other.tag;
			fishingAreaObject = other.gameObject;
			Debug.Log("you are now in the "+fishingArea);
		}
		//when you go back to ertebølle midden to retrieve tool
		if(other.tag == "ertebølle")
		{
			GameManager.singleton.hook.GetComponent<SelectTool>().ShowTool();
			GameManager.singleton.eeliron.GetComponent<SelectTool>().ShowTool();
		}
		//when you destroy a basket
		if(other.tag == "destroyBasket")
		{
			other.GetComponent<Basket>().DestroyBasket();

		}
		//when you enter tribe territory
		if(other.tag == "tribeTerritory")
		{

			//partner say something
		}
		//when enter torsk territory orca event happens 
		if(other.tag == "torskTerritory")
		{
			//partner says orca thing
			GameManager.singleton.orca.GetComponent<orcaEvent>().startOrcaEvent();
		}
    }

	void OnTriggerExit(Collider other)
	{
		//when you exit a fishing area, you are set to not be able to fish anymore
		if(other.tag == "TorskArea" || other.tag == "EelArea")
		{
			ExitArea();
		}
		//when exit torsk territory pelican event happens 
		if(other.tag == "torskTerritory")
		{
			Debug.Log("YieldInstruction entered");
			//partner says pelican thing
		}

	}

	public void ExitArea()
	{
			canFish = false;
			fishingArea = "";
			Debug.Log("you are now out of fishing area");
			//partner says there are no more fish here.
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
				//Instantiate(torsk,transform.position, transform.rotation);
				//remove a fish from the ocean
				fishingAreaObject.GetComponent<FishContent>().RemoveFish();
			}
			if(tool == "eeliron" && fishingArea == "EelArea")
			{
				Debug.Log("caugth a eel");
				//instatiate a fish in the boay
				//Instantiate(eel,transform.position, transform.rotation);
				//remove a fish from the ocean
				fishingAreaObject.GetComponent<FishContent>().RemoveFish();
			}
			else
			{
				//partner should make a comment
			}
		}

	}
	public bool GetCanFish()
	{
		return canFish;
	}

	public GameObject GetCurrentFishingArea()
	{
		return fishingAreaObject;
	}

}
