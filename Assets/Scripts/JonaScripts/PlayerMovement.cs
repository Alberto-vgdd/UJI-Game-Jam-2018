using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float m_MovementSpeed;
    public float m_JumpStrength;
    public float m_jumps;

    Rigidbody2D m_Rigidbody2D;
    public BoxCollider2D m_BoxCollider2D;

    public enum AnimationState{IDLE, RUNNING, JUMPING, MIDAIR, FALLING, LANDING};
    public AnimationState m_AnimationState;
    public Animator m_PlayerAnimator;
    float m_NumberOfJumps;
    float m_DistanceToGround;

    bool m_IsMoving = false;
    public bool m_IsGrounded = false;

    public PlayerAudioManager m_PlayerAudioManager;


    void Run(){
        m_AnimationState = AnimationState.RUNNING;
    }

	void Awake () 
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_DistanceToGround = m_BoxCollider2D.bounds.extents.y;
	}

    void CheckIfGrounded(){

        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-m_DistanceToGround), Vector2.down, 0.01f);
        if(hit.collider != null){
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
    }

    void OnGrounded(){
        if(m_IsGrounded)
        {
            m_NumberOfJumps = m_jumps;
            
            if(m_AnimationState == AnimationState.FALLING)
            {
                m_PlayerAnimator.SetInteger("AnimationState", 4);
            }
            Invoke("Run",0.3f);            
            
        }
    }
	
	void Update () 
    {
        Move();
        CheckFall();
        CheckIfGrounded();
        OnGrounded();
        UpdateAnimation();
        
	}

    void CheckFall(){
        if(m_Rigidbody2D.velocity.y < 0 && (m_AnimationState == AnimationState.RUNNING || m_AnimationState == AnimationState.JUMPING)){
            m_PlayerAnimator.SetInteger("AnimationState",0);
            m_AnimationState = AnimationState.FALLING;
            if(m_NumberOfJumps>1) m_NumberOfJumps--;
        }
    }


    void Move()
    {
        m_Rigidbody2D.velocity = new Vector2(m_MovementSpeed * Time.deltaTime, m_Rigidbody2D.velocity.y);
        m_IsMoving = true;
        /*
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown("space")) && m_NumberOfJumps > 0)
        {
            Jump();
        }
         */
        
        /*else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {

            m_Rigidbody2D.AddForce(new Vector2(m_DashForce * Time.deltaTime, 0));
        }*/


        if(Input.GetMouseButtonDown(0)){
            if(m_NumberOfJumps > 0){
                Jump();
            }
        }



    }


    void Jump()
    {

        if (m_NumberOfJumps > 0)
        {
            m_NumberOfJumps--;
            if(m_NumberOfJumps == 1){
                m_PlayerAudioManager.PlayJumpSound();
            }else{
                m_PlayerAudioManager.PlayDoubleJumpSound();
            }
            m_AnimationState = AnimationState.JUMPING;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpStrength);
            m_PlayerAnimator.SetInteger("AnimationState", 1);
        }


    }





    void UpdateAnimation()
    {
        if(m_AnimationState == AnimationState.RUNNING)
        {
            m_PlayerAnimator.SetInteger("AnimationState", 3);
        }else if(m_AnimationState == AnimationState.LANDING){
        }
    }


}
