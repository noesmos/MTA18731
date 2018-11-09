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

        //prefabs
        public GameObject torsk;
	    public GameObject eel;
	    public GameObject flatFish;




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
        } 
        
        //Update is called every frame.
        void Update()
        {
            
        }

        public void AddEel(GameObject eel)
        {
            caughtEel.Add(eel);
            AccumulateFish();
        }
        public void AddTorsk(GameObject torsk)
        {
            caughtTorsk.Add(torsk);
            AccumulateFish();
        }
        public void AddFlatFish(GameObject flat)
        {
            caughtEel.Add(flat);
            AccumulateFish();
        }

        public void AccumulateFish()
        {
            caughtTotal.Clear();
            caughtTotal.AddRange(caughtEel);
            caughtTotal.AddRange(caughtTorsk);
            caughtTotal.AddRange(caughtFlatfish);
        }

        //not sure about this one
        public void RemoveAnyFish(int amount)
        {
            for (int i = 0; i >amount; i++) 
            {
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
                Destroy(caughtTotal[0].gameObject);
			    caughtTotal.RemoveAt(0);
                }
                catch
                {
                    Debug.Log("no more eels");
                }

            }
            AccumulateFish();
        }

}
