using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if (other.gameObject.GetComponentInParent<PlayerMovement> ().m_IsDashing) 
			{
				Explosion ();
			}
		}
		
	}

	void Explosion()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
		Invoke ("Reactivate", 2.0f);
	}

	void Reactivate()
	{
		this.GetComponent<BoxCollider2D> ().enabled = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
