using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{

	public string accion;

	// Variables to enable/disable this button.
	private GameObject[] buttonGameObjects;
	private bool buttonEnabled;

	// Images to asign materials when the color scheme changes.
	[Header("Place here the images separated by color")]
	public Image[] buttonOutlineImages;
	public Image[] buttonFillImages;
	public Image[] buttonPressedImages;

	public Tienda tienda;
	public GameObject tutorial;
	public GameObject tutoSalto;
	public GameObject tutoSaltoDoble;
	public GameObject tutoDash;
	public GameObject tutoAgacharse;
	public GameObject tutoGirar;
	public GameObject tutoSecreto;




	// Use this for initialization
	void Awake () 
	{

		buttonGameObjects = new GameObject[transform.childCount];

		for (int i = 0; i < buttonGameObjects.Length; i++)
		{
			buttonGameObjects[i] = transform.GetChild(i).gameObject;
		}

		buttonEnabled = true;

		//COMPROBAR SI EL PRIMER SALTO ESTÁ COMPRADO
		if (accion == "DobleSalto" && GlobalData.saltoComprado == false) {
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		//COMPROBAR SI EL RESTO DE MEJORAS ESTÁN COMPRADAS
		if(accion == "Salto" && GlobalData.saltoComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		if(accion == "DobleSalto" && GlobalData.dobleSaltoComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		if(accion == "Dash" && GlobalData.dashComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		if(accion == "Girar" && GlobalData.girarComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		if(accion == "Agacharse" && GlobalData.agacharseComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

		if(accion == "Secreto" && GlobalData.secretoComprado == true){
			buttonEnabled = false;
			buttonGameObjects[1].SetActive(false);
		}

	}

	void Update(){
		if (accion == "DobleSalto" && GlobalData.saltoComprado == true) {
			buttonEnabled = true;
			buttonGameObjects[0].SetActive(true);
		}
	}


	public void OnPointerDown(PointerEventData eventData)
    {
		if (buttonEnabled)
		{
			// Enable released button gameobject
			buttonGameObjects[1].SetActive(false);
		}

    }

	public void OnPointerUp(PointerEventData eventData)
    {
		if (buttonEnabled)
		{
			// The button will be disabled here.
			UsarBoton (accion);
		}
    }


	// This function is used to change the material when the color scheme changes.
	public void ChangeMaterials(Material outlineMaterial, Material fillMaterial, Material pressedMaterial)
	{
		foreach (Image image in buttonOutlineImages)
		{
			image.material = outlineMaterial;
		}

		foreach (Image image in buttonFillImages)
		{
			image.material = fillMaterial;
		}

		foreach (Image image in buttonPressedImages)
		{
			image.material = pressedMaterial;
		}
	}

	public void EnableButton(bool isEnabled)
	{
		// Enable/Disable this button
		buttonEnabled = isEnabled;
	}

	public void UsarBoton(string nombre)
	{
		
		switch(nombre){
		case "CargarNivel":
			SceneManager.LoadScene ("TileMapTest");
			break;

		case "Salto":
			if (GlobalData.experienciaTotal >= tienda.PrecioSalto && GlobalData.saltoComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioSalto;
				GlobalData.saltoComprado = true;
				tienda.Actualizar ();

				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoSalto.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "DobleSalto":
			if (GlobalData.experienciaTotal >= tienda.PrecioDobleSalto && GlobalData.dobleSaltoComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioDobleSalto;
				GlobalData.dobleSaltoComprado = true;
				tienda.Actualizar ();

				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoSaltoDoble.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Dash":
			if (GlobalData.experienciaTotal >= tienda.PrecioDash && GlobalData.dashComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioDash;
				GlobalData.dashComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoDash.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Girar":
			if (GlobalData.experienciaTotal >= tienda.PrecioGirar && GlobalData.girarComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioGirar;
				GlobalData.girarComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoGirar.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Agacharse":
			if (GlobalData.experienciaTotal >= tienda.PrecioAgacharse && GlobalData.agacharseComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioAgacharse;
				GlobalData.agacharseComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoAgacharse.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Secreto":
			if (GlobalData.experienciaTotal >= tienda.PrecioSecreto && GlobalData.secretoComprado == false) {
				GlobalData.experienciaTotal = GlobalData.experienciaTotal - tienda.PrecioSecreto;
				GlobalData.secretoComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);

				tutorial.SetActive (true);
				tutoSecreto.SetActive (true);
				StartCoroutine(EsperarCerrarTutorial(3));
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Salir":
			HUDManagerScript.instance.UseButton(3);

			// Enable released button gameobject
			buttonGameObjects[1].SetActive(true);
			break;
		default:
			break;
		}
	}

	IEnumerator EsperarCerrarTutorial(int segundos)
	{
		yield return new WaitForSeconds(segundos);
		tutorial.SetActive (false);
		tutoSalto.SetActive (false);
		tutoSaltoDoble.SetActive (false);
		tutoGirar.SetActive (false);
		tutoAgacharse.SetActive (false);
		tutoDash.SetActive (false);
		tutoSecreto.SetActive (false);

	}



}
