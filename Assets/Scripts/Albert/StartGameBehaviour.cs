using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBehaviour : MonoBehaviour {

	public GameObject idleBedAnimation;

	public GameObject startGameBedAnimation;

	public GameObject playerReference;

	public static StartGameBehaviour currentInstance;
	private bool alreadyStarted = false;

	void Awake()
	{
		currentInstance = this;
	}

	// Use this for initialization
	void Start () 
	{
	
		startGameBedAnimation.SetActive (false);
		idleBedAnimation.SetActive (true);
		playerReference.SetActive (false);

	}

	public void StartGameAnimation()
	{
		idleBedAnimation.SetActive (false);
		startGameBedAnimation.SetActive (true);
		playerReference.SetActive (true);
	}



	// Update is called once per frame
	void Update () 
	{
		
		if (Input.GetMouseButtonDown(0)) 
		{
			if (alreadyStarted == false) 
			{
				StartGameBehaviour.currentInstance.StartGameAnimation ();
				alreadyStarted = true;
			}

		}
	}
}
