using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenHandScript : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public int buttonId;
   
	public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HUDManagerScript.instance.UseButton(buttonId);
    }
}


