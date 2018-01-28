using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour {

    public Transform player;
    bool m_IsDead = false;
    Vector3 lastPos;
    float lastScale;
    float count = 1;
	// Update is called once per frame
	void Awake(){

        lastPos = player.position;
        lastScale = player.localScale.y;

	}
	
	void Update () {

        count = count - Time.deltaTime;
        if (count <= 0 && !m_IsDead) {
			if (!player.gameObject.GetComponent<PlayerMovementScript> ().InCheckPoint) 
			{
				CheckDeath();
			}
            count = 0.4f;

        }
	}

	void CheckDeath(){

        if (player.position.x == lastPos.x && lastScale == player.localScale.y) {

            m_IsDead = true;
            GameOver();


        }
        lastPos = player.position;
        lastScale = player.localScale.y;

	}


	void GameOver(){
		if(m_IsDead){
            //Time.timeScale = 0.02f;
			//GameManager.instance.RestartDream();
			GlobalData.currentInstance.KillingInTheNameOf();
		}
	}
}
