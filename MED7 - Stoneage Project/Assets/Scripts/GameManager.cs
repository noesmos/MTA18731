using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
        public static GameManager singleton = null;              //Static instance of GameManager which allows it to be accessed by any other script.                            //Current level number, expressed in game as "Day 1".

        public GameObject boat;
        public GameObject partner;

        public GameObject hook;
        public GameObject eeliron;


        List<GameObject> caughtEel = new List<GameObject>();
        List<GameObject> caughtHook =new List<GameObject>();
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
            partner = GameObject.FindGameObjectWithTag("partner");
            Debug.Log(partner.gameObject.name);
            hook = GameObject.FindGameObjectWithTag("hook");
            Debug.Log(hook.gameObject.name);
            eeliron = GameObject.FindGameObjectWithTag("eeliron");
            Debug.Log(eeliron.gameObject.name);
        } 
        
        //Update is called every frame.
        void Update()
        {
            
        }

        public void AddEel(GameObject eel)
        {
            caughtEel.Add(eel);
        }
        public void AddTorsk(GameObject torsk)
        {
            caughtEel.Add(torsk);
        }
        public void AddFlatFish(GameObject flat)
        {
            caughtEel.Add(flat);
        }
}
