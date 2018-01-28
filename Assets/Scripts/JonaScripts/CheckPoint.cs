using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    //public float multiplier = 0.5f;
    //PlayerMovementScript player;

	public GameObject CheckPointCanvasReference;

	public GameObject playerReference;

	public PlayerMovementScript script;

	void Awake()
	{
		CheckPointCanvasReference.SetActive(false);
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Añadir activar menu cuando esté 
        //Time.timeScale = 0;
       // player = collision.gameObject.GetComponent<PlayerMovementScript>();
		CheckPointCanvasReference.SetActive(true);
		print ("CHECKPOINT ACHIEVED");
		//AsegurarDinero();
		GlobalData.currentInstance.currentStreakExpPlayer += 0.5f;
		playerReference = collision.gameObject;
		script = playerReference.GetComponent<PlayerMovementScript> ();
		script.InCheckPoint = true;
		playerReference.GetComponentInChildren<Animator> ().SetInteger ("AnimationState", 27);
    }

	public void AsegurarDinero() {

		GlobalData.experienciaTotal = GlobalData.experienciaTotal + (int) (GlobalData.currentInstance.expPlayer * GlobalData.currentInstance.currentStreakExpPlayer);
		//GlobalData.currentInstance.ResetStats ();
		//GlobalData.currentInstance.ResetScene ();
        //Falta llamar al Game Over Cuando esté
    }

    public void Continue() {
         
		CheckPointCanvasReference.SetActive(false);
        //Faltará desactivar el menu cuando esté
		script.InCheckPoint = true;
		playerReference.GetComponentInChildren<Animator> ().SetInteger ("AnimationState", 28);
        
    }
}
