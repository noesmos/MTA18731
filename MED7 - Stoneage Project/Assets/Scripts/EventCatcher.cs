﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCatcher : MonoBehaviour {

	bool canFish;
	string fishingArea;

	bool firstTimeInTorskArea = true;
	bool firstTimeInEelArea = true;

	GameObject fishingAreaObject;

	bool hasFlint=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
		
		if(other.tag == "pillar1" ||other.tag == "pillar2" ||other.tag == "pillar3"||other.tag == "pillar4"||other.tag == "pillar5")
		{
			other.gameObject.SetActive(false);
		}
		//when you enter a fishing area
		if(other.tag == "TorskArea" || other.tag == "EelArea" || other.tag == "FlatfishArea")
		{
			canFish = true;
			fishingArea = other.tag;
			fishingAreaObject = other.gameObject;
			Debug.Log("you are now in the "+fishingArea);
			
			if(other.tag == "TorskArea" && firstTimeInTorskArea)
			{
				//firstTimeInTorskArea = false;
				if(GameManager.singleton.Islinear)
				{
					GameManager.singleton.
						partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
						GameManager.singleton.partner.GetComponent<PartnerSpeech>().CodEnterArea);
				}
				else
				{
					GameManager.singleton.
						partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
						GameManager.singleton.partner.GetComponent<PartnerSpeech>().EnterCodAreaEmergent);
				}	
			}
			if( other.tag == "EelArea" && firstTimeInEelArea)
			{
				//firstTimeInEelArea = false;
				if(GameManager.singleton.Islinear)
				{
					GameManager.singleton.
						partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
						GameManager.singleton.partner.GetComponent<PartnerSpeech>().FlaringEel);
				}
				else
				{
					float time =GameManager.singleton.timer.GetComponent<playTimer>().GetTimeSpent();
					if(time > 0.4)
					{
						GameManager.singleton.
							partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
							GameManager.singleton.partner.GetComponent<PartnerSpeech>().EnterCoastAreaDay);
					}

					else
					{
						GameManager.singleton.
							partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
							GameManager.singleton.partner.GetComponent<PartnerSpeech>().EnterCoastAreaNight);
					}
				}

			}
		}
        //when you go back to ertebølle midden to retrieve tool
		if(other.tag == "ertebølle")
		{
			//GameManager.singleton.hook.GetComponent<SelectTool>().ShowTool();
			//GameManager.singleton.eeliron.GetComponent<SelectTool>().ShowTool();
		
			//change scene
			CheckForEnding();

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
			GameManager.singleton.
				partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
				GameManager.singleton.partner.GetComponent<PartnerSpeech>().EnterTribe);
        }
        //when enter torsk territory orca event happens 
        if(other.tag == "torskTerritory")
        {
            //partner says orca thing
            GameManager.singleton.orca.GetComponent<orcaEvent>().startOrcaEvent();
        }
        if(other.tag == "sealTerritory")
		{
			if(GameManager.singleton.Islinear)
			{
				GameManager.singleton.
					partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().SealAppears);
			}
			else
			{
				GameManager.singleton.
					partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().SealAppearsEmergent);
			}
		}

		//when the tribe is chasing you to get fish back
		if(other.tag == "tribeTrigger")
		{
			//stop following
			GameManager.singleton.tribeBoat.GetComponent<TribeController>().SetFollowPlayer(false);
			//remove fish
			GameManager.singleton.RemoveAnyFish(5);
			//go back to their own midden in bjørnsholm
			GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetInPosition(GameManager.singleton.bjørnsholm.transform.position);
			GameManager.singleton.partner.
				GetComponent<PartnerSpeech>().PartnerSaysSomething(
				GameManager.singleton.partner.GetComponent<PartnerSpeech>().MeetingTribeCaught);
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
        if(other.tag == "pelicanTrigger")
        {
			GameManager.singleton.PelicanEvent.SetActive(true);
			GameManager.singleton.PelicanEvent.transform.SetParent(null);
			GameManager.singleton.PelicanEvent.GetComponentInChildren<orcaEvent>().startOrcaEvent();
            //partner says pelican thing
        }
        if(other.tag == "tribeTerritory")
        {
            //partner say something
			if(GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetFollowPlayer())
			{
				GameManager.singleton.
					partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().MeetingTribeEscaped);
				GameManager.singleton.tribeBoat.GetComponent<TribeController>().SetFollowPlayer(false);
			}
			else
			{
				GameManager.singleton.
					partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().ExitTribe);	
			}



        }

    }

	public void CheckForEnding()
	{
		if(GameManager.singleton.GetFishCount() >=7)
			{
				if(hasFlint)
				{
					//enought fish 7 and flint
					GameManager.singleton.PrepareForEndScene(GameManager.singleton.partner.GetComponent<PartnerSpeech>().Outcome4Emergent, hasFlint);
					SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
				}
				else
				{
					//enough fish 7
					GameManager.singleton.PrepareForEndScene(GameManager.singleton.partner.GetComponent<PartnerSpeech>().Outcome3Emergent, hasFlint);
					SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
				}
			}
			else 
			{
				if(hasFlint)
				{
					//not enough fish and has flint
					GameManager.singleton.PrepareForEndScene(GameManager.singleton.partner.GetComponent<PartnerSpeech>().Outcome5Emergent, hasFlint);
					SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
				}
				else
				{
					//not enough fish 7
					GameManager.singleton.PrepareForEndScene(GameManager.singleton.partner.GetComponent<PartnerSpeech>().Outcome1Emergent, hasFlint);
					SceneManager.LoadScene("End Scene", LoadSceneMode.Single);

				}
			}
	}

	public void ExitArea()
	{
		canFish = false;
			fishingArea = "";
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


	public void DisableTrading()
	{
		GameManager.singleton.tradingObject.GetComponent<Collider>().enabled = false;
	}

	public void TradeFishForFlint()
	{
		Debug.Log("you want to trade");
		if(GameManager.singleton.GetFishCount() >= 5)
		{
			Debug.Log("You have enough fish");
			GameManager.singleton.RemoveAnyFish(5);
			Instantiate(GameManager.singleton.flint,transform.position+ transform.up*2 - 1.5f*transform.forward, transform.rotation, transform);
			hasFlint = true;
			DisableTrading();
			//Debug.Break();
			if(GameManager.singleton.Islinear)
			{
				GameManager.singleton.partner.
					GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().AfterTradingFlint, "FANG 3 ÅL");
			}
			else
			{
				GameManager.singleton.partner.
					GetComponent<PartnerSpeech>().PartnerSaysSomething(
					GameManager.singleton.partner.GetComponent<PartnerSpeech>().MeeetingTribeTrade);
			}
		}

	}

	public bool GetHasFlint()
	{
		return hasFlint;
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
