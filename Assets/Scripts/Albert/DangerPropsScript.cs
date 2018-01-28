using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerPropsScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			GlobalData.currentInstance.KillingInTheNameOf ();
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
