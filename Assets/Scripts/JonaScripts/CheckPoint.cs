using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    //public float multiplier = 0.5f;
    //PlayerMovementScript player;

	public GameObject CheckPointCanvasReference;

	public GameObject playerReference;

	void Awake()
	{
		CheckPointCanvasReference.SetActive(false);
	}



	private void OnTriggerEnter2D(Collider2D other)
    {
        //Añadir activar menu cuando esté 
        //Time.timeScale = 0;
       // player = collision.gameObject.GetComponent<PlayerMovementScript>();
		CheckPointCanvasReference.SetActive(true);
		print ("CHECKPOINT ACHIEVED");
	
		//AUI HAY QUE HACER QUE EL PLAYER NO SE MUEVA

		GlobalData.currentInstance.currentStreakExpPlayer += 0.5f;
		playerReference = other.gameObject;
		playerReference.GetComponentInChildren<Animator> ().SetInteger ("AnimationState", 27);
		other.gameObject.GetComponent<PlayerMovementScript> ().InCheckPoint = true;
    }

	public void AsegurarDinero() {

		GlobalData.experienciaTotal = GlobalData.experienciaTotal + (int) (GlobalData.currentInstance.expPlayer * 
		GlobalData.currentInstance.currentStreakExpPlayer);
		GlobalData.currentInstance.ResetStats ();
		GlobalData.currentInstance.ResetScene ();
        //Falta llamar al Game Over Cuando esté
    }

    public void Continue() {
         

        //Faltará desactivar el menu cuando esté
		//script.InCheckPoint = true;
		playerReference.GetComponentInChildren<Animator> ().SetInteger ("AnimationState", 28);
		CheckPointCanvasReference.SetActive(false);
		playerReference.GetComponent<PlayerMovementScript> ().InCheckPoint = false;
		//AUI HAY QUE HACER QUE EL PLAYER SE MUEVA
        
    }
}
