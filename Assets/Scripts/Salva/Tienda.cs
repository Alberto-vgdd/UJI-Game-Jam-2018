using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour {

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
	private Animator animacion;


	// Use this for initialization
	void Start () {
		tienda = this.gameObject;
		animacion = this.gameObject.GetComponent<Animator> ();
		animacion.enabled = false;
		if (GlobalData.saltoComprado) BotonSalto.EnableButton(false);
		if (GlobalData.dobleSaltoComprado) BotonDobleSalto.EnableButton(false);
		if (GlobalData.dashComprado) BotonDash.EnableButton(false);
		if (GlobalData.girarComprado) BotonGirar.EnableButton(false);
		if (GlobalData.agacharseComprado) BotonAgacharse.EnableButton(false);
		if (GlobalData.secretoComprado) BotonSecreto.EnableButton(false);
		Actualizar ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Actualizar(){
		experienciaActual.text = GlobalData.experiencia + "xp";
		distanciaMaxima.text = GlobalData.metros + "m";
	}

	// public void ComprarMejora(string nombre){
	// 	switch(nombre){
	// 	case "Salto":
	// 		if (GlobalData.experiencia >= PrecioSalto) {
	// 			BotonSalto.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioSalto;
	// 		}
	// 				break;
	// 	case "DobleSalto":
	// 		if (GlobalData.experiencia >= PrecioDobleSalto) {
	// 			BotonDobleSalto.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioDobleSalto;
	// 		}
	// 				break;
	// 	case "Dash":
	// 		if (GlobalData.experiencia >= PrecioDash) {
	// 			BotonDash.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioDash;
	// 		}
	// 				break;
	// 	case "Girar":
	// 		if (GlobalData.experiencia >= PrecioGirar) {
	// 			BotonGirar.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioGirar;
	// 		}
	// 				break;
	// 	case "Agacharse":
	// 		if (GlobalData.experiencia >= PrecioAgacharse) {
	// 			BotonAgacharse.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioAgacharse;
	// 		}
	// 				break;
	// 	case "Secreto":
	// 		if (GlobalData.experiencia >= PrecioSecreto) {
	// 			BotonSecreto.interactable = false;
	// 			GlobalData.experiencia = GlobalData.experiencia - PrecioSecreto;
	// 		}
	// 				break;
	// 		default:
	// 			break;
	// 	}
	// }

	public void SalirTienda(){
		animacion.enabled = true;
		StartCoroutine(EsperarSalir(1));

	}


	IEnumerator EsperarSalir(int segundos)
	{
		yield return new WaitForSeconds(segundos);
		tienda.SetActive (false);
	}
}
