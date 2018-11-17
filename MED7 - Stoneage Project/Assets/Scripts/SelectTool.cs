using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTool : MonoBehaviour {

public bool IsTribeBasket;

EventCatcher EC;
PartnerAnimator PA;

	string tool ="";

	// Use this for initialization
	void Start () {
		EC = GameManager.singleton.boat.GetComponent<EventCatcher>();
		PA = GameManager.singleton.partner.GetComponent<PartnerAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
	}


	//
	public void Select(){
		//selecting which tool to use 
		if(tag == "hook")
		{
			Debug.Log("selected hook");
			tool = tag;
			//play animation
			HideTool();
			PA.HookAnimation();
			//wait for animaion to end to show tool again
			//ShowTool();

			//StartCoroutine("OnAnimationComplete");

		}
		else if (tag == "eeliron")
		{
			Debug.Log("selected eeliron");
			tool = tag;
			//play animation
			HideTool();
			PA.EelironAnimation();
			
			//wait for animaion to end to show tool again
			//ShowTool();
			
		}
		//admit to fish with the selected tool
		EC.startFishing(tool);
	}

	public void EmptyBasket()
	{
		if(tag =="emptyBasket")
		{
			PA.BasketAnimation();
			Debug.Log("emptying basket");
			GetComponent<Collider>().enabled=false;
			if(IsTribeBasket)
			{
				GameManager.singleton.tribeBoat.GetComponent<TribeController>().SetFollowPlayer(true);
				
			}
		}
	}

	void HideTool()
	{
		//disable collider to not select it again and disable mesh renderer to make it invisible
		GetComponent<Collider>().enabled = false;
		GetComponentInChildren<MeshRenderer>().enabled = false;
	}

	public void ShowTool()
	{
		//enable collider to make tool selectable and enable mesh renderer to make it visible
		GetComponent<Collider>().enabled = true;
		GetComponentInChildren<MeshRenderer>().enabled = true;
	}

}
