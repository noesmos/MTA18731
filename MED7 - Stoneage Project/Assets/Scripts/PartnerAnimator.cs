﻿using System.Collections;
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
		anim.SetTrigger("hook");
	}
	public void EelironAnimation()
	{
		anim.SetTrigger("eelIron");
	}

	public void BasketAnimation()
	{
		anim.SetTrigger("basket");
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
		for(int i =0; i < 5; i++)
		{
			Debug.Log("putting fish in basket");
			//PutEelInBasket();
			PutFlatFishInBasket();
		}
		Debug.Log("tell player to go to torsk");
		GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().GoToTorsk, "Go To Torsk");

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
