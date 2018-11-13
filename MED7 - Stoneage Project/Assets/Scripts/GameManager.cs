using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
        public static GameManager singleton = null;              //Static instance of GameManager which allows it to be accessed by any other script.                            //Current level number, expressed in game as "Day 1".

        //instanses in the scene
        public GameObject boat;
        public GameObject tribeBoat;
        public GameObject partner;
        public GameObject hook;
        public GameObject eeliron;
        public GameObject orca;

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

            boat = GameObject.FindGameObjectWithTag("boat");
            Debug.Log(boat.gameObject.name);
            tribeBoat = GameObject.FindGameObjectWithTag("tribeBoat");
            Debug.Log(tribeBoat.gameObject.name);
            partner = GameObject.FindGameObjectWithTag("partner");
            Debug.Log(partner.gameObject.name);
            hook = GameObject.FindGameObjectWithTag("hook");
            Debug.Log(hook.gameObject.name);
            eeliron = GameObject.FindGameObjectWithTag("eeliron");
            Debug.Log(eeliron.gameObject.name);
            orca = GameObject.FindGameObjectWithTag("orca");
            Debug.Log(orca.gameObject.name);
            PelicanEvent = GameObject.FindGameObjectWithTag("flyingPelican");
            Debug.Log(PelicanEvent.gameObject.name);
            PelicanEvent.SetActive(false);
        } 
        
        //Update is called every frame.
        void Update()
        {
            
        }

        public void AddEel(GameObject eel)
        {
            //caughtEel.Add(eel);
            caughtTotal.Add(eel);
            AccumulateFish();
        }
        public void AddTorsk(GameObject torsk)
        {
            //caughtTorsk.Add(torsk);
            caughtTotal.Add(torsk);
            AccumulateFish();
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

}
