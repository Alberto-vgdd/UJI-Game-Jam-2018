using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    //public float multiplier = 0.5f;
    //PlayerMovementScript player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Añadir activar menu cuando esté 
        Time.timeScale = 0;
       // player = collision.gameObject.GetComponent<PlayerMovementScript>();
		AsegurarDinero();
		GlobalData.currentInstance.currentStreakExpPlayer += 0.5f;
    }

	public void AsegurarDinero() {

		GlobalData.experienciaTotal = GlobalData.experienciaTotal + (int) (GlobalData.currentInstance.expPlayer * 
			GlobalData.currentInstance.currentStreakExpPlayer);
        //Falta llamar al Game Over Cuando esté
    }

    public void Continue() {
         
        //Faltará desactivar el menu cuando esté
        Time.timeScale = 1;
        
    }
}
