using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAnimator : MonoBehaviour {


	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hookAnimation()
	{
		anim.SetBool("hook", true);
	}
		public void eelironAnimation()
	{
		anim.SetBool("eelIron", true);
	}




}
