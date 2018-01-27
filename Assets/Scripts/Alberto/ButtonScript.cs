using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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




	// Use this for initialization
	void Awake () 
	{

		buttonGameObjects = new GameObject[transform.childCount];

		for (int i = 0; i < buttonGameObjects.Length; i++)
		{
			buttonGameObjects[i] = transform.GetChild(i).gameObject;
		}

		buttonEnabled = true;

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
		case "Salto":
			if (GlobalData.experiencia >= tienda.PrecioSalto && GlobalData.saltoComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioSalto;
				GlobalData.saltoComprado = true;
				tienda.Actualizar ();

				//Disable this button
				EnableButton(false);
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "DobleSalto":
			if (GlobalData.experiencia >= tienda.PrecioDobleSalto && GlobalData.dobleSaltoComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioDobleSalto;
				GlobalData.dobleSaltoComprado = true;
				tienda.Actualizar ();

				//Disable this button
				EnableButton(false);
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Dash":
			if (GlobalData.experiencia >= tienda.PrecioDash && GlobalData.dashComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioDash;
				GlobalData.dashComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Girar":
			if (GlobalData.experiencia >= tienda.PrecioGirar && GlobalData.girarComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioGirar;
				GlobalData.girarComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Agacharse":
			if (GlobalData.experiencia >= tienda.PrecioAgacharse && GlobalData.agacharseComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioAgacharse;
				GlobalData.agacharseComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);
			}
			else
			{
				// Enable released button gameobject
				buttonGameObjects[1].SetActive(true);
			}
			break;
		case "Secreto":
			if (GlobalData.experiencia >= tienda.PrecioSecreto && GlobalData.secretoComprado == false) {
				GlobalData.experiencia = GlobalData.experiencia - tienda.PrecioSecreto;
				GlobalData.secretoComprado = true;
				tienda.Actualizar ();
				
				//Disable this button
				EnableButton(false);
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





}
