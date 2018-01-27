using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Variables to enable/disable this button.
	private GameObject[] buttonGameObjects;
	private bool buttonEnabled;
	private bool fingerOnButton;

	// Variables to use this button
	[Header("Place here the buttonID from HUDManagerScript")]
	public int buttonId;

	// Images to asign materials when the color scheme changes.
	[Header("Place here the images separated by color")]
	public Image[] buttonOutlineImages;
	public Image[] buttonFillImages;
	public Image[] buttonPressedImages;



	// Use this for initialization
	void Awake () 
	{

		buttonGameObjects = new GameObject[transform.childCount];

		for (int i = 0; i < buttonGameObjects.Length; i++)
		{
			buttonGameObjects[i] = transform.GetChild(i).gameObject;
		}

		buttonEnabled = true;
		fingerOnButton = false;

	}

	// Button Pressed
	public void OnPointerDown(PointerEventData eventData)
    {
		if (buttonEnabled)
		{
			// Enable released button gameobject
			buttonGameObjects[1].SetActive(false);
		}

    }

	// Button Released
	public void OnPointerUp(PointerEventData eventData)
    {
		if (buttonEnabled)
		{
			// Enable released button gameobject
			buttonGameObjects[1].SetActive(true);
		}

		if (fingerOnButton)
		{
			//Use the button
			HUDManagerScript.instance.UseButton(buttonId);
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

	// This is used to disable button
	public void EnableButton(bool isEnabled)
	{
		// Enable/Disable this button
		buttonEnabled = isEnabled;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        fingerOnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fingerOnButton = false;
    }
}
