using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAnimator : MonoBehaviour {



	public Animator anim;

	GameObject boat;

	GameObject mostRecentFish;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		boat = GameManager.singleton.boat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HookAnimation()
	{
		anim.SetTrigger("hookFishing");
	}
	public void EelironAnimation()
	{
		anim.SetTrigger("eelIronFishing");
	}

	public void BasketAnimation()
	{
		anim.SetTrigger("basketFishing");
	}

	public void trapEmpty()
	{
		anim.SetTrigger("trapEmpty");
	}

	public void trapFull()
	{
		anim.SetTrigger("trapFull");
	}


	public void StartPaddleAnimation(){
		anim.SetBool("isPaddling", true);
	}
		public void StopPaddleAnimation(){
		anim.SetBool("isPaddling", false);
	}

	public void HookAniDone(){
		GameManager.singleton.hook.GetComponent<SelectTool>().ShowTool();
		Debug.Log("Hook ani done");
		if(GameManager.singleton.boat.GetComponent<EventCatcher>().GetCanFish())
		{
			PutTorskInBasket();
		}
		//check if there is more fish in the area
		GameManager.singleton.boat.GetComponent<EventCatcher>().GetCurrentFishingArea().GetComponent<FishContent>().DestroyEmptyArea();
	}

	
	public void EelIronAniDone(){
		GameManager.singleton.eeliron.GetComponent<SelectTool>().ShowTool();
		Debug.Log("iron ani done");
		if(GameManager.singleton.boat.GetComponent<EventCatcher>().GetCanFish())
		{
			PutEelInBasket();
		}
		GameManager.singleton.boat.GetComponent<EventCatcher>().GetCurrentFishingArea().GetComponent<FishContent>().DestroyEmptyArea();

	}
	public void BasketAniDone(){
		bool basketFull = true;
		
		if (basketFull)
		{
			trapFull();
			for(int i =0; i < 5; i++)
			{
				Debug.Log("putting fish in basket");
				//PutEelInBasket();
				PutFlatFishInBasket();
			}
			Debug.Log("Trap Full");
		} else {
			trapEmpty();
			Debug.Log("Trap Empty");
		}

	}
	public void PutFlatFishInBasket()
	{
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.flatFish,boat.transform.position+ new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddFlatFish(mostRecentFish);
	}
	public void PutTorskInBasket()
	{
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.torsk,boat.transform.position+ new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddTorsk(mostRecentFish);
	}
		public void PutEelInBasket()
	{
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.eel,boat.transform.position + new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddEel(mostRecentFish);
	}


}
