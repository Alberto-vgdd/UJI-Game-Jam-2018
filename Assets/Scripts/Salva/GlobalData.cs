using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {


	public static int experiencia = 10000;
	public static float metros;
	public static bool saltoComprado;
	public static bool dobleSaltoComprado;
	public static bool dashComprado;
	public static bool girarComprado;
	public static bool agacharseComprado;
	public static bool secretoComprado;

	public static GlobalData currentInstance;

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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
