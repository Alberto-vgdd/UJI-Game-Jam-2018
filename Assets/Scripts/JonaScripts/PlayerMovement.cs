using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float m_MovementSpeed;
    public float m_JumpStrength;

    Rigidbody2D m_Rigidbody2D;
    BoxCollider2D m_BoxCollider2D;

    public enum AnimationState{IDLE, RUNNING, JUMPING, MIDAIR, FALLING};
    public AnimationState m_AnimationState;
    public Animator m_PlayerAnimator;

    bool m_IsMoving = false;

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "Ground"){
            m_AnimationState = AnimationState.RUNNING;
    
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

    }

    void Move() 
    {
        
        m_Rigidbody2D.velocity = new Vector2(m_MovementSpeed * Time.deltaTime, m_Rigidbody2D.velocity.y);
        m_IsMoving = true;
        if (Input.GetKeyDown("space")) {
            m_AnimationState = AnimationState.JUMPING;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,m_JumpStrength * Time.deltaTime);
        }

        
    }




    void UpdateAnimation()
    {
        if(m_AnimationState == AnimationState.RUNNING)
        {
            m_PlayerAnimator.SetInteger("AnimationState", 3);
        }else if(m_AnimationState == AnimationState.JUMPING){
            m_PlayerAnimator.SetInteger("AnimationState", 1);
        }else if(m_AnimationState == AnimationState.FALLING){
            m_PlayerAnimator.SetInteger("AnimationState", 0);
        }
    }
}
