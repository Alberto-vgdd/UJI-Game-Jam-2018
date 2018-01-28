using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour {

	public GameObject fondoEstrellas;
	public GameObject HUDTiendaEntero;

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

	public Text dineroSalto;
	public Text dineroDobleSalto;
	public Text dineroDash;
	public Text dineroGirar;
	public Text dineroAgacharse;
	public Text dineroSecreto;


	private Animation animacionTienda;
	private Animation animacionEstrellas;

	private int previousHUDMenu;

	// Use this for initialization
	public void EntrarTienda (int previousHUDMenu) 
	{
		this.previousHUDMenu = previousHUDMenu;

		if (GlobalData.saltoComprado) BotonSalto.EnableButton(false);
		if (GlobalData.dobleSaltoComprado) BotonDobleSalto.EnableButton(false);
		if (GlobalData.dashComprado) BotonDash.EnableButton(false);
		if (GlobalData.girarComprado) BotonGirar.EnableButton(false);
		if (GlobalData.agacharseComprado) BotonAgacharse.EnableButton(false);
		if (GlobalData.secretoComprado) BotonSecreto.EnableButton(false);


		animacionTienda = this.gameObject.GetComponent<Animation> ();
		animacionTienda.Play ("TransicionInicialTienda",PlayMode.StopAll);

		fondoEstrellas.SetActive (true);
		animacionEstrellas = fondoEstrellas.GetComponent<Animation> ();
		animacionEstrellas.Play ("TransicionInicialEstrellas",PlayMode.StopAll);

		Actualizar ();

		dineroSalto.text = PrecioSalto + "xp";
		dineroDobleSalto.text = PrecioDobleSalto + "xp";
		dineroDash.text = PrecioDash + "xp";
		dineroGirar.text = PrecioGirar + "xp";
		dineroAgacharse.text = PrecioAgacharse + "xp";
		dineroSecreto.text = PrecioSecreto + "xp";

		distanciaMaxima.text = GlobalData.metros + "m"; //Esto solo se actualiza cuando se abre la tienda ya que mientras no corre el personaje.

	}
	
	// Update is called once per frame
	void Update () {
		experienciaActual.text = GlobalData.experienciaTotal + "xp";
	}

	public void Actualizar(){
		experienciaActual.text = GlobalData.experienciaTotal + "xp";
	}
		
	public int SalirTienda(){
		animacionTienda.Play ("TransicionFinalTienda");
		StartCoroutine(EsperarSalir(1));
		return previousHUDMenu;

	}


	IEnumerator EsperarSalir(int segundos)
	{
		animacionEstrellas.Play ("TransicionFinalEstrellas");
		yield return new WaitForSeconds(segundos);
		StartCoroutine(EsperarSalir2(1));
		//tienda.SetActive (false);
	}

	IEnumerator EsperarSalir2(int segundos)
	{
		yield return new WaitForSeconds(segundos);
		HUDTiendaEntero.SetActive(false);
	}


}
