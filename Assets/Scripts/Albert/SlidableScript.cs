﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidableScript : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if (other.gameObject.GetComponent<PlayerMovementScript> ().m_AnimationState == PlayerMovementScript.AnimationStates.SLIDE
				|| other.gameObject.GetComponent<PlayerMovementScript>().m_IsDashing &&
				other.gameObject.GetComponent<PlayerMovementScript> ().m_IsSliding) 
			{
				DeactivateSlidable ();
			}
		}

	}

	void DeactivateSlidable()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
		//print ("SE DESACTIVA");
		Invoke ("ActivateSlidable", 2.0f);
	}

	void ActivateSlidable()
	{
		//print ("SE ACTIVA");
		this.GetComponent<BoxCollider2D> ().enabled = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
				
	}
}
