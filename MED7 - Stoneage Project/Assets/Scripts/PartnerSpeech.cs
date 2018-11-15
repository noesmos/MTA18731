using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerSpeech : MonoBehaviour {

	AudioSource audio;

	public AudioClip GoToBasket;
	public AudioClip GoToTorsk;
	public AudioClip GoToTribe;
	public AudioClip GoToEel;
	public AudioClip GoToMidden;
	public Text speech;

	bool donePlaying;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

		speech.text = "it works";
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!audio.isPlaying && !donePlaying)
        {
            Debug.Log(audio.clip.name);
            //audioSource.Play();
			donePlaying = true;
			if(audio.clip.name == "GoToBasket")
			{
				GameManager.singleton.pillar1.SetActive(true);
				GameManager.singleton.basket.GetComponent<Collider>().enabled = true;
			}
			else if(audio.clip.name == "GoToTorsk")
			{
				GameManager.singleton.pillar2.SetActive(true);
				GameManager.singleton.pillar1.SetActive(false);
				GameManager.singleton.torskTerritory.GetComponent<Collider>().enabled = true;
				foreach(GameObject area in GameManager.singleton.torskArea)
				{
					area.GetComponent<Collider>().enabled = true;
				}
				//start counting Cod
			}
			else if(audio.clip.name == "GoToTribe")
			{
				GameManager.singleton.pillar3.SetActive(true);
				GameManager.singleton.pillar2.SetActive(false);
				GameManager.singleton.trading.GetComponent<Collider>().enabled = true;
				GameManager.singleton.torskTerritory.GetComponent<Collider>().enabled = false;
				GameManager.singleton.torskTerritory2.GetComponent<Collider>().enabled = true;
			}
			else if(audio.clip.name == "GoToEel")
			{
				GameManager.singleton.pillar4.SetActive(true);
				GameManager.singleton.pillar3.SetActive(false);
				GameManager.singleton.eelTerritory.GetComponent<Collider>().enabled = true;
				foreach(GameObject area in GameManager.singleton.eelArea)
				{
					area.GetComponent<Collider>().enabled = true;
				}
				//start counting eel
			}
			else if(audio.clip.name == "GoToMidden")
			{
				GameManager.singleton.pillar5.SetActive(true);
				GameManager.singleton.pillar4.SetActive(false);
				GameManager.singleton.midden.GetComponent<Collider>().enabled = true;
			}
        }
		else if (audio.isPlaying && donePlaying)
		{
			donePlaying=false;
			Debug.Log("playing again");
		}
	}


	public void PartnerSaysSomething(AudioClip clip, string writtenLine)
	{
		audio.clip = clip;
		speech.text = writtenLine;
		audio.Play();
	}

}
