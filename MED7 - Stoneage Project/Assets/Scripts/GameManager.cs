using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
        public static GameManager singleton = null;              //Static instance of GameManager which allows it to be accessed by any other script.                            //Current level number, expressed in game as "Day 1".

        //instanses in the scene
        
        public GameObject timer;
        public GameObject boat;
        public GameObject tribeBoat;
        public GameObject partner;
        public GameObject hook;
        public GameObject eeliron;
        public GameObject orca;
        public GameObject bjørnsholm;
        public GameObject PelicanEvent;
        
        //prefabs
        public GameObject torsk;
	    public GameObject eel;
	    public GameObject flatFish;
        public GameObject flint;
        public GameObject tradingObject;


        List<GameObject> caughtTotal = new List<GameObject>();
        List<GameObject> caughtEel = new List<GameObject>();
        List<GameObject> caughtTorsk =new List<GameObject>();
        List<GameObject> caughtFlatfish =new List<GameObject>();

        //tools held by guide + the paddle with audio source
        public GameObject aniTorch;
        public GameObject aniEelIron;

        // Audio Sources

        public GameObject paddle;


        //linear stuff
        bool isCountingTorsk = false;
        int startTorskAmount;
        int currentTorskAmount=0;
        bool isCountingEel = false;
        int startEelAmount;
        int currentEelAmount=0;


        public bool Islinear;
        public List<GameObject> torskArea, eelArea = new List<GameObject>();
        public GameObject trading;
        public GameObject pillar1,pillar2,pillar3,pillar4,pillar5, currentPillar;

        public GameObject torskTerritory, torskTerritory2, eelTerritory, tribeTerritory;
        public GameObject basket,tribeBasket;
        public GameObject midden;


        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (singleton == null)
                
                //if not, set instance to this
                singleton = this;
            
            //If instance already exists and it's not this:
            else if (singleton != this)
                
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);    
            
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            timer = GameObject.FindGameObjectWithTag("timer");
            //Debug.Log(boat.gameObject.name); 
            boat = GameObject.FindGameObjectWithTag("boat");
            //Debug.Log(boat.gameObject.name);      
            bjørnsholm = GameObject.FindGameObjectWithTag("bjørnholm");
            //Debug.Log(boat.gameObject.name);            
            tribeBoat = GameObject.FindGameObjectWithTag("tribeBoat");
            //Debug.Log(tribeBoat.gameObject.name);
            partner = GameObject.FindGameObjectWithTag("partner");
            //Debug.Log(partner.gameObject.name);
            hook = GameObject.FindGameObjectWithTag("hook");
            //Debug.Log(hook.gameObject.name);
            eeliron = GameObject.FindGameObjectWithTag("eeliron");
            //Debug.Log(eeliron.gameObject.name);
            paddle = GameObject.FindGameObjectWithTag("paddle");
            orca = GameObject.FindGameObjectWithTag("orca");
            //Debug.Log(orca.gameObject.name);
            PelicanEvent = GameObject.FindGameObjectWithTag("flyingPelican");
            //Debug.Log(PelicanEvent.gameObject.name);
            PelicanEvent.SetActive(false);


            //what should be turned of initially for the linear condition
            if(Islinear)
            {
                //disable the collider on following game objects
                foreach(GameObject area in torskArea)
                {
                    area.SetActive(false);
                }
                foreach(GameObject area in eelArea)
                {
                    area.SetActive(false);
                }
                torskTerritory.GetComponent<Collider>().enabled = false;
                eelTerritory.GetComponent<Collider>().enabled = false;
                tribeTerritory.GetComponent<Collider>().enabled = false;
                midden.GetComponent<Collider>().enabled = false;
                basket.GetComponent<Collider>().enabled = false;
                tribeBasket.GetComponent<Collider>().enabled = false;

                //totally disable the following game objects

                tribeBasket.SetActive(false);
            }
            //what should be turned of initially for both condition
                pillar1.SetActive(false);
                pillar2.SetActive(false);
                pillar3.SetActive(false);
                pillar4.SetActive(false);
                pillar5.SetActive(false);
                torskTerritory2.GetComponent<Collider>().enabled = false;
        } 
        
        //Update is called every frame.
        void Update()
        {
            
        }

        public void AddEel(GameObject eel)
        {
            //caughtEel.Add(eel);
            caughtTotal.Add(eel);
            currentEelAmount++; 
            Debug.Log("caught a eel. now we have "+currentEelAmount);
            AccumulateFish();
            if(isCountingEel)
            {
                Debug.Log("checking for eel caught: " +(currentEelAmount-startEelAmount));
                
                if(currentEelAmount-startEelAmount>=3)
                {
                    partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(partner.GetComponent<PartnerSpeech>().AfterFlaringEel, "Lad os tage hjem igen");
                }
            }

        }
        public void AddTorsk(GameObject torsk)
        {
            //caughtTorsk.Add(torsk);
            caughtTotal.Add(torsk);
            currentTorskAmount++;
            Debug.Log("caught a torsk. now we have "+currentTorskAmount);
            AccumulateFish();
            if(isCountingTorsk)
            {
                Debug.Log("checking for torsk caught");
                if(currentTorskAmount-startTorskAmount==1)
                {
                    partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(partner.GetComponent<PartnerSpeech>().CodTwoMore);
                }
                else if(currentTorskAmount-startTorskAmount==2)
                {
                    partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(partner.GetComponent<PartnerSpeech>().CodOneMore);
                }
                else if(currentTorskAmount-startTorskAmount>=3)
                {

                    partner.GetComponent<PartnerSpeech>().PartnerSaysSomething(partner.GetComponent<PartnerSpeech>().AfterCodCatch, "Lad os Bytte nogen fisk for flint");
                }
            }
        }
        public void AddFlatFish(GameObject flat)
        {
            //caughtFlatfish.Add(flat);
            caughtTotal.Add(flat);
            AccumulateFish();
        }

        public void AccumulateFish()
        {
            //commented out for debugging
            //caughtTotal.Clear();
            //caughtTotal.AddRange(caughtEel);
            //caughtTotal.AddRange(caughtTorsk);
            //caughtTotal.AddRange(caughtFlatfish);
        }

        public int GetFishCount()
        {
            return caughtTotal.Count;
        }

        //not sure about this one
        public void RemoveAnyFish(int amount)
        {
            Debug.Log("entered");
            for (int i = 0; i < amount; i++) 
            {
                Debug.Log(caughtTotal[0].gameObject.name + " is deleted. there are now " +caughtTotal.Count + " fish");
                Destroy(caughtTotal[0].gameObject);
			    caughtTotal.RemoveAt(0);

            }
        }
        public void RemoveEels(int amount)
        {
            for (int i = 0; i >amount; i++) 
            {
                try
                {
                Destroy(caughtFlatfish[0].gameObject);
			    caughtFlatfish.RemoveAt(0);
                }
                catch
                {
                    Debug.Log("no more eels");
                }

            }
            AccumulateFish();
        }
        public void StartCountingTorsk()
        {
            Debug.Log("we are counting torsk");
            isCountingTorsk=true;
            startTorskAmount = currentTorskAmount;
        } 
        public void StartCountingEel()
        {
            Debug.Log("we are counting eel");
            isCountingEel=true;
            startEelAmount = currentEelAmount;
        } 
}
