using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAnimator : MonoBehaviour {



	public Animator anim;

	GameObject boat;

	GameObject mostRecentFish;

	bool firstTimeEel =true;
	bool firstTimeCod =true;
	bool firstTimeFlatfish =true;

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
		GameManager.singleton.paddle.SetActive(false);
	}
	public void EelironAnimation()
	{
		anim.SetTrigger("eelIronFishing");
		GameManager.singleton.paddle.SetActive(false);
		GameManager.singleton.aniEelIron.SetActive(true);
		GameManager.singleton.aniTorch.SetActive(true);
	}

	public void BasketAnimation()
	{
		anim.SetTrigger("basketFishing");
	}

	public void trapEmpty()
	{
		if(!GameManager.singleton.Islinear)
		{
			if(GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
			{
				GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().MeetingTriibeStoleFish);
			}
			else
			{
				GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().CheckBasketNoFish);
			}
		}
		anim.SetTrigger("trapEmpty");
	}

	public void trapFull()
	{
		if(!GameManager.singleton.Islinear)
		{
			if(GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
			{
				GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().MeetingTriibeStoleFish);
			}
			else
			{
				GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().CheckBasketFish);
			}

		}
		anim.SetTrigger("trapFull");
	}

	public void StartTalking()
	{
		anim.SetBool("isTalking", true);
	}
	public void StopTalking()
	{
		anim.SetBool("isTalking",false);
	}

	public void codCaught()
	{
		anim.SetTrigger("codCaught");
	}

	public void eelCaught()
	{
		anim.SetTrigger("eelCaught");
	}

	public void noCatch()
	{
		anim.SetTrigger("noCatch");
	}
	public void pointLeft(bool state)
	{
		anim.SetBool("pointLeft", state);
	}

	public void pointRight(bool state)
	{
		anim.SetBool("pointRight", state);
	}

	public void wrongWay(bool state)
	{
		anim.SetBool("wrongWay", state);
	}

	public void paddleAnimation(bool state){
		anim.SetBool("isRowing", state);
	}


	public void HookAniDone(){
		GameManager.singleton.hook.GetComponent<SelectTool>().ShowTool();
		GameManager.singleton.paddle.SetActive(true);
		Debug.Log("Hook ani done");
		if(GameManager.singleton.boat.GetComponent<EventCatcher>().GetCanFish())
		{
			codCaught();
			PutTorskInBasket();	
		} else {
			noCatch();
		}

		//check if there is more fish in the area
		try
		{
			GameManager.singleton.boat.GetComponent<EventCatcher>().GetCurrentFishingArea().GetComponent<FishContent>().DestroyEmptyArea();
		}
		catch{}

	}
	public void PaddleAniDone(){
		ResetToolOnGuide();

	}
	
	public void EelIronAniDone(){
		GameManager.singleton.eeliron.GetComponent<SelectTool>().ShowTool();
		GameManager.singleton.paddle.SetActive(true);
		GameManager.singleton.aniEelIron.SetActive(false);
		GameManager.singleton.aniTorch.SetActive(false);
		Debug.Log("iron ani done");
		if(GameManager.singleton.boat.GetComponent<EventCatcher>().GetCanFish())
		{
			eelCaught();
			PutEelInBasket();
		} else {
			noCatch();
		}

		try
		{
			GameManager.singleton.boat.GetComponent<EventCatcher>().GetCurrentFishingArea().GetComponent<FishContent>().DestroyEmptyArea();
		}
		catch{}

	}
	public void BasketAniDone(){
		bool basketFull = true;
		
		if (basketFull)
		{
			trapFull();
			
			for(int i =0; i < 4; i++)
			{
				Debug.Log("putting fish in basket");
				//PutEelInBasket();
				PutEelInBasket();
			}
			for(int i =0; i < 2; i++)
			{
				Debug.Log("putting fish in basket");
				//PutEelInBasket();
				PutFlatFishInBasket();
			}
			if(GameManager.singleton.Islinear)
			{
				GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().AfterEmptyBasket, "FANG 3 TORSK");
			}
			Debug.Log("Trap Full");
		} else {
			trapEmpty();
			Debug.Log("Trap Empty");
		}

	}
	public void PutFlatFishInBasket()
	{
		if(!GameManager.singleton.Islinear && GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().MeetingTriibeStoleFish);
		}
		if(!GameManager.singleton.Islinear && firstTimeFlatfish && !GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			firstTimeFlatfish = false;
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().FirstTimeFlatFish);
		}
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.flatFish,boat.transform.position+ new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddFlatFish(mostRecentFish);
	}
	public void PutTorskInBasket()
	{
		if(!GameManager.singleton.Islinear && GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().MeetingTriibeStoleFish);
		}
		if(!GameManager.singleton.Islinear && firstTimeCod  && !GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			firstTimeCod = false;
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().FirstTimeCod);
		}
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.torsk,boat.transform.position+ new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddTorsk(mostRecentFish);
	}
	public void PutEelInBasket()
	{
		if(!GameManager.singleton.Islinear && GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().MeetingTriibeStoleFish);
		}
		if(!GameManager.singleton.Islinear && firstTimeEel  && !GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
		{
			firstTimeEel = false;
			GetComponent<PartnerSpeech>().PartnerSaysSomething(GetComponent<PartnerSpeech>().FirstTimeEel);
		}
		//instatiate a fish in the boay
		mostRecentFish = Instantiate(GameManager.singleton.eel,boat.transform.position + new Vector3(0,1,0), boat.transform.rotation, boat.transform);
		GameManager.singleton.AddEel(mostRecentFish);
	}
	void ResetToolOnGuide()
	{
				GameManager.singleton.paddle.SetActive(true);
		GameManager.singleton.aniEelIron.SetActive(false);
		GameManager.singleton.aniTorch.SetActive(false);
	}


}
