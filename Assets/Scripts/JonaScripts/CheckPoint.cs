using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public float multiplier = 0.5f;
    PlayerMovement player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Añadir activar menu cuando esté 
        Time.timeScale = 0;
        player = collision.gameObject.GetComponent<PlayerMovement>();
    }

    public void Rescute() {

        GlobalData.experiencia = GlobalData.experiencia + (int)player.m_Coins;
        //Falta llamar al Game Over Cuando esté
    }

    public void Continue() {
         
        //Faltará desactivar el menu cuando esté
        Time.timeScale = 1;
        
    }
}
