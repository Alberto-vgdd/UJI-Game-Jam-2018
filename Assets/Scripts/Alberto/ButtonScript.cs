using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{

	public string accion;

	private GameObject[] buttonGameObjects;

	// Images to asign materials when the color scheme changes.
	[Header("Place here the images separated by color")]
	public Image[] buttonOutlineImages;
	public Image[] buttonFillImages;
	public Image[] buttonPressedImages;

	public Tienda tienda;



	// Use this for initialization
	void Awake () 
	{

		buttonGameObjects = new GameObject[transform.childCount];

		for (int i = 0; i < buttonGameObjects.Length; i++)
		{
			buttonGameObjects[i] = transform.GetChild(i).gameObject;
		}

	}


	public void OnPointerDown(PointerEventData eventData)
    {
		ComprarMejora (accion);
		// Disable released button gameobject
		buttonGameObjects[1].SetActive(false);
    }

	public void OnPointerUp(PointerEventData eventData)
    {
		// Enable released button gameobject
		buttonGameObjects[1].SetActive(true);
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

	public void ComprarMejora(string nombre){
		
		switch(nombre){
		case "Salto":
			if (GlobalData.experiencia >= tienda.PrecioSalto && GlobalData.saltoComprado == false) {
				print ("HOLA");
				//BotonSalto.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioSalto;
				GlobalData.saltoComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "DobleSalto":
			if (GlobalData.experiencia >= tienda.PrecioDobleSalto && GlobalData.dobleSaltoComprado == false) {
				//BotonDobleSalto.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioDobleSalto;
				GlobalData.dobleSaltoComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "Dash":
			if (GlobalData.experiencia >= tienda.PrecioDash && GlobalData.dashComprado == false) {
				//BotonDash.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioDash;
				GlobalData.dashComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "Girar":
			if (GlobalData.experiencia >= tienda.PrecioGirar && GlobalData.girarComprado == false) {
				//BotonGirar.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioGirar;
				GlobalData.girarComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "Agacharse":
			if (GlobalData.experiencia >= tienda.PrecioAgacharse && GlobalData.agacharseComprado == false) {
				//BotonAgacharse.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioAgacharse;
				GlobalData.agacharseComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "Secreto":
			if (GlobalData.experiencia >= tienda.PrecioSecreto && GlobalData.secretoComprado == false) {
				//BotonSecreto.interactable = false;
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioSecreto;
				GlobalData.secretoComprado = true;
				tienda.Actualizar ();
			}
			break;
		case "Salir":
			tienda.SalirTienda ();
			break;
		default:
			break;
		}
	}





}
