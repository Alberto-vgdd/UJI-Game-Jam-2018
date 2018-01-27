using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float m_MovementSpeed = 300f;
    public float m_JumpStrength = 375f;

    Rigidbody2D m_Rigidbody2D;
    BoxCollider2D m_BoxCollider2D;

    public enum AnimationState{IDLE, RUNNING, JUMPING, MIDAIR, FALLING};
    public AnimationState m_AnimationState;

    bool m_IsMoving = false;

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "Ground"){
            if(m_AnimationState == AnimationState.MIDAIR){
                m_AnimationState = AnimationState.FALLING;
            }else{
                m_AnimationState = AnimationState.RUNNING;
            }
           

        }
            
        
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        
    }

	void Awake () 
    {
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        Move();
        CheckAnimationStatus();
        UpdateAnimation();
	}

    void CheckAnimationStatus()
    {

        if(m_Rigidbody2D.velocity.y == 0 && m_AnimationState == AnimationState.JUMPING){
            m_AnimationState = AnimationState.MIDAIR;
        }
        if(m_Rigidbody2D.velocity.y > 0){
            m_AnimationState = AnimationState.JUMPING;
        }else if(m_Rigidbody2D.velocity.y < 0){
            m_AnimationState = AnimationState.FALLING;
        }

    }

    void Move() 
    {
        
        m_Rigidbody2D.velocity = new Vector2(m_MovementSpeed * Time.deltaTime, m_Rigidbody2D.velocity.y);
        m_IsMoving = true;
        if (Input.GetKeyDown("space")) {
            
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,m_JumpStrength * Time.deltaTime);
        }

        
    }




    void UpdateAnimation()
    {

    }
}
