using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour {

	public ParticleSystem shardSystem;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if (other.gameObject.GetComponentInParent<PlayerMovementScript> ().m_IsDashing) 
			{
				Explosion ();
			}
		}
		
	}

	void Explosion()
	{
		shardSystem.gameObject.SetActive (true);
		shardSystem.transform.position = this.gameObject.transform.position;
		shardSystem.Emit (100);
		this.GetComponent<BoxCollider2D> ().enabled = false;
		Invoke ("Reactivate", 0.5f);
	}

	void Reactivate()
	{
		//this.GetComponent<BoxCollider2D> ().enabled = true;
		//shardSystem.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
