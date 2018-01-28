using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour {

	//Dinero total acumulado por el jugador: se actualizará 
	//cada vez que el jugador llegue a un checkpoint.
	public static int experienciaTotal;
	public static float metros;
	public static bool saltoComprado;
	public static bool dobleSaltoComprado;
	public static bool dashComprado;
	public static bool girarComprado;
	public static bool agacharseComprado;
	public static bool secretoComprado;

	public static GlobalData currentInstance;

	//Dinero por run del jugador, que se reinicia cada vez que muere.
	public float expPlayer;
	public float currentStreakExpPlayer = 1f;

	public static GameObject playerGameObjectReference;

	void Awake()
	{
		
		if (currentInstance == null) 
		{
			DontDestroyOnLoad (gameObject);
			currentInstance = this;

			experienciaTotal = 0;
			expPlayer = 0f;

		}
		else if (currentInstance != this) 
		{
			Destroy (this);
		}
			

		playerGameObjectReference = GameObject.FindGameObjectWithTag ("Player");
	}

	public void RestartExpPlayer()	{	expPlayer = 0f;	}
	public void RestartMultiplierExpPlayer() {currentStreakExpPlayer = 1f;	}

	public void ResetStats() 
	{
		RestartExpPlayer (); 
		RestartMultiplierExpPlayer ();
	}	

	public void ReturnToBeginning()
	{
		playerGameObjectReference.transform.position = new Vector3 (-3.49f, 0.47f, 0f);
	}

	public void KillingInTheNameOf()
	{
		/*
		 * Aqui poner animacion de muerte del personaje.
		 * Y carteles de "has perdido lol git gud"
		 * 
		 */
		ResetStats ();
		ResetScene ();
	}

	public void ResetScene()
	{
		SceneManager.LoadScene (0); //Carga la escena situada en el 0 del orden de build.
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
