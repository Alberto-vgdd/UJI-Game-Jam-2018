using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{

	private GameObject[] buttonGameObjects;

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

	}


	public void OnPointerDown(PointerEventData eventData)
    {
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





}
