using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerSpeech : MonoBehaviour {

	AudioSource audio;

	//for linear condition
	[Header("linear sounds")]
	public AudioClip StartofGameLinear;
	public AudioClip AfterEmptyBasket;
	public AudioClip AfterCodCatch;
	public AudioClip AfterTradingFlint;
	public AudioClip AfterFlaringEel;
	public AudioClip CodEnterArea;
	public AudioClip CodOneMore;
	public AudioClip CodTwoMore;
	public AudioClip EmptyBasket;
	public AudioClip FlaringEel;
	public AudioClip NotThatWay;
	public AudioClip OtherTribeSpotTrade;
	public AudioClip Outcome1Linear;
	public AudioClip Outcome2Linear;

	//for emergent condition
	[Header("Emergent sounds")]
	public AudioClip StartofGameEmergent;
	public AudioClip CheckBasketFish;
	public AudioClip CheckBasketNoFish;
	//public AudioClip EnterCodArea;
	public AudioClip EnterCoastAreaDay;
	public AudioClip EnterCoastAreaNight;
	public AudioClip EnterTribe;
	public AudioClip ExitTribe;
	public AudioClip FishingTribe;
	public AudioClip MeetingTribeCaught;
	public AudioClip MeetingTribeEscaped;
	public AudioClip MeetingTribeSpotTrade;
	public AudioClip MeetingTriibeStoleFish;
	public AudioClip MeeetingTribeTrade;
	public AudioClip FirstTimeEel;
	public AudioClip FirstTimeCod;
	public AudioClip FirstTimeFlatFish;
	public AudioClip SealAppearsEmergent;
	public AudioClip Time1MinLeft;
	public AudioClip Time2MinLeft;
	public AudioClip Outcome1Emergent;
	public AudioClip Outcome2Emergent;
	public AudioClip Outcome3Emergent;
	public AudioClip Outcome4Emergent;
	public AudioClip Outcome5Emergent;

	//for both condition
	[Header("neutral sounds")]
	public AudioClip OrcaAppears;
	public AudioClip PelicanAppears;
	public AudioClip SealAppears;
	public AudioClip StartofGame;
	public AudioClip ThisWay1;
	public AudioClip ThisWay2;

	[Header(" sounds")]

	public Text speech;

	bool donePlaying;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

		speech.text = "it works";
		if(GameManager.singleton.Islinear)
		{
			PartnerSaysSomething(StartofGameLinear, "Lad os sejle over og tømme ålefælden");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (!audio.isPlaying && !donePlaying)
        {
            //Debug.Log(audio.clip.name);
            //audioSource.Play();
			donePlaying = true;
			GetComponent<PartnerAnimator>().StopTalking();
			if(audio.clip.name == "StartofGame_linear")
			{
				GameManager.singleton.pillar1.SetActive(true);
				GameManager.singleton.basket.GetComponent<Collider>().enabled = true;
			}
			else if(audio.clip.name == "AfterEmptyBasket_linear")
			{
				GameManager.singleton.pillar2.SetActive(true);
				GameManager.singleton.pillar1.SetActive(false);
				//Destroy(GameManager.singleton.pillar1);
				Debug.Log("pillar1 is now "+GameManager.singleton.pillar1.activeSelf);
				GameManager.singleton.torskTerritory.GetComponent<Collider>().enabled = true;
				GameManager.singleton.StartCountingTorsk();
				foreach(GameObject area in GameManager.singleton.torskArea)
				{
					Debug.Log("activating area");
					area.SetActive(true);
				}
				//start counting Cod
			}
			else if(audio.clip.name == "AfterCodCatch_linear")
			{
				GameManager.singleton.pillar3.SetActive(true);
				GameManager.singleton.pillar2.SetActive(false);
				Debug.Log("pillar2 is now "+GameManager.singleton.pillar2.activeSelf);
				GameManager.singleton.trading.GetComponent<Collider>().enabled = true;
				GameManager.singleton.torskTerritory.GetComponent<Collider>().enabled = false;

			}
			else if(audio.clip.name == "AfterTradingFlint_linear2")
			{
				Debug.Log("eel event happening");
				GameManager.singleton.pillar4.SetActive(true);
				GameManager.singleton.pillar3.SetActive(false);
				Debug.Log("pillar3 is now "+GameManager.singleton.pillar3.activeSelf);
				GameManager.singleton.eelTerritory.GetComponent<Collider>().enabled = true;
				GameManager.singleton.torskTerritory2.GetComponent<Collider>().enabled = true;
				GameManager.singleton.StartCountingEel();
				GameManager.singleton.tribeBoat.GetComponent<TribeController>().GetInPosition(GameManager.singleton.bjørnsholm.transform.position);
				foreach(GameObject area in GameManager.singleton.eelArea)
				{
					area.SetActive(true);
				}
				//start counting eel
			}
			else if(audio.clip.name == "AfterFlaringEel_linear")
			{
				GameManager.singleton.pillar5.SetActive(true);
				GameManager.singleton.pillar4.SetActive(false);
				Debug.Log("pillar4 is now "+GameManager.singleton.pillar4.activeSelf);
				GameManager.singleton.midden.GetComponent<Collider>().enabled = true;
			}
        }
		else if (audio.isPlaying && donePlaying)
		{
			donePlaying=false;
			Debug.Log("playing again");
			Debug.Break();

		}
	}


	public void PartnerSaysSomething(AudioClip clip, string writtenLine)
	{
		GetComponent<PartnerAnimator>().StartTalking();
		audio.clip = clip;
		speech.text = writtenLine;
		audio.Play();
		//Debug.Break();
	}
		public void PartnerSaysSomething(AudioClip clip)
	{
		GetComponent<PartnerAnimator>().StartTalking();
		audio.clip = clip;
		audio.Play();
		//Debug.Break();
	}

}
