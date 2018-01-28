using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour {

	//Dinero total acumulado por el jugador: se actualizará 
	//cada vez que el jugador llegue a un checkpoint.
	public static int experienciaTotal = 0;
	public static float metros;
	public static bool saltoComprado;
	public static bool dobleSaltoComprado;
	public static bool dashComprado;
	public static bool girarComprado;
	public static bool agacharseComprado;
	public static bool secretoComprado;

	public static GlobalData currentInstance;

	//Dinero por run del jugador, que se reinicia cada vez que muere.
	public float expPlayer = 0;
	public float currentStreakExpPlayer = 1f;

	public Text coinTextReference;

	public GameObject playerGameObjectReference;

	void Awake()
	{
		if (currentInstance != null) 
		{
			Destroy (this);
		}
		else
		{
			currentInstance = this;
		}
	}

	public void RestartExpPlayer()	{	expPlayer = 0f;	}
	public void RestartMultiplierExpPlayer() {currentStreakExpPlayer = 1f;	}

	public void ResetStats() 
	{
		RestartExpPlayer (); 
		RestartMultiplierExpPlayer ();
	}
		
	public void ActualizarUIDinero()	{	coinTextReference.text = ((int)expPlayer).ToString();	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		ActualizarUIDinero ();	
	}
}
