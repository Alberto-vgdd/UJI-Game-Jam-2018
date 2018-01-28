using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBehaviour : MonoBehaviour {

	public GameObject idleBedAnimation;

	public GameObject startGameBedAnimation;

	public GameObject playerReference;

	public static StartGameBehaviour currentInstance;
	private bool alreadyStarted = false;

	public Text coinTextReference;

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

	public void ActualizarUIDinero()	{	coinTextReference.text = ((int)GlobalData.currentInstance.expPlayer).ToString();	}

	// Update is called once per frame
	void Update () 
	{
		ActualizarUIDinero ();	
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
