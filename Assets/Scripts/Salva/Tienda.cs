using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour {

	public GameObject fondoEstrellas;

	public int PrecioSalto;
	public int PrecioDobleSalto;
	public int PrecioDash;
	public int PrecioGirar;
	public int PrecioAgacharse;
	public int PrecioSecreto;

	public ButtonScript BotonSalto;
	public ButtonScript BotonDobleSalto;
	public ButtonScript BotonDash;
	public ButtonScript BotonGirar;
	public ButtonScript BotonAgacharse;
	public ButtonScript BotonSecreto;
	public ButtonScript BotonSalir;

	public Text experienciaActual;
	public Text distanciaMaxima;

	private GameObject tienda;
	private Animation animacionTienda;
	private Animation animacionEstrellas;



	// Use this for initialization
	public void EntrarTienda () 
	{
		tienda = this.gameObject;

		if (GlobalData.saltoComprado) BotonSalto.EnableButton(false);
		if (GlobalData.dobleSaltoComprado) BotonDobleSalto.EnableButton(false);
		if (GlobalData.dashComprado) BotonDash.EnableButton(false);
		if (GlobalData.girarComprado) BotonGirar.EnableButton(false);
		if (GlobalData.agacharseComprado) BotonAgacharse.EnableButton(false);
		if (GlobalData.secretoComprado) BotonSecreto.EnableButton(false);
		Actualizar ();

		animacionTienda = this.gameObject.GetComponent<Animation> ();
		animacionTienda.Play ("TransicionInicialTienda",PlayMode.StopAll);

		fondoEstrellas.SetActive (true);
		animacionEstrellas = fondoEstrellas.GetComponent<Animation> ();
		animacionEstrellas.Play ("TransicionInicialEstrellas",PlayMode.StopAll);

		Debug.Log("123123");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Actualizar(){
		experienciaActual.text = GlobalData.experiencia + "xp";
		distanciaMaxima.text = GlobalData.metros + "m";
	}
		
	public void SalirTienda(){
		animacionTienda.Play ("TransicionFinalTienda");
		StartCoroutine(EsperarSalir(1));

	}


	IEnumerator EsperarSalir(int segundos)
	{
		yield return new WaitForSeconds(segundos);
		animacionEstrellas.Play ("TransicionFinalEstrellas");
		tienda.SetActive (false);
	}


}
