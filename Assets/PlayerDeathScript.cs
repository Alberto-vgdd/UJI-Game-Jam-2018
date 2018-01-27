using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour {

	public Rigidbody2D m_Rigidbody2D;
	GameManager m_GameManager;
	BoxCollider2D m_BoxCollider2D;
	public bool m_IsDead = false;

	public float m_ActualPositionX = 0;
	public float m_LastPositionX = 0;
	float frames = 0;
	// Update is called once per frame
	void Awake(){
		m_BoxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
		frames++;
		CheckDeath();
		GameOver();
	}

	void CheckDeath(){
		if(frames == 0){
			
		}
		m_ActualPositionX = transform.position.x;

		if(m_LastPositionX != 0){
			if(m_ActualPositionX == m_LastPositionX){
				m_IsDead = true;
			}
		}
		if(frames % 60 == 0){
			m_LastPositionX = m_ActualPositionX;

		}
	}


	void GameOver(){
		if(m_IsDead){
			GameManager.instance.RestartDream();
		}
	}
}
