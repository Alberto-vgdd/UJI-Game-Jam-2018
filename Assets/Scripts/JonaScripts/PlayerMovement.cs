using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float m_MovementSpeed;
    public float m_JumpStrength;
    public float m_jumps;
    public float m_DashForce;

    Rigidbody2D m_Rigidbody2D;
    public BoxCollider2D m_BoxCollider2D;

    public enum AnimationState{IDLE, RUNNING, JUMPING, MIDAIR, FALLING, LANDING, CROUCHING};
    public AnimationState m_AnimationState;
    public Animator m_PlayerAnimator;
    float m_NumberOfJumps;
    float m_DistanceToGround;

    Vector3 m_mouse;
    bool m_Presed = false;
    bool m_IsMoving = false;
    public bool m_IsSliding = false;
    public bool m_IsDashing = false;
    public bool m_IsJumping = false;
    public bool m_IsGrounded = false;
    public PlayerAudioManager m_PlayerAudioManager;



    float m_LastJump = 0;

    void Run(){
        m_AnimationState = AnimationState.RUNNING;
    }

	void Awake () 
    {
        m_BoxCollider2D = GetComponentInChildren<BoxCollider2D>();
        m_DistanceToGround = m_BoxCollider2D.bounds.extents.y;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
                m_IsJumping = false;
                m_PlayerAnimator.SetInteger("AnimationState", 4);
            }
            Invoke("Run",0.3f);            
            
        }
    }
	
	void Update () 
    {
        GetInput();
        Move();
        CheckFall();
        CheckIfGrounded();
        OnGrounded();
        UpdateAnimation();
        
	}

    void GetInput(){

        if (Input.GetMouseButtonUp(0) && m_Presed)
        {
                Jump();
                m_Presed = false;
        }

        if (Input.GetMouseButtonDown(0))
        {

            m_Presed = true;
            m_mouse = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {   
                if(!m_IsJumping){
                    if (Mathf.Abs(m_mouse.x - Input.mousePosition.x) > Mathf.Abs(m_mouse.y - Input.mousePosition.y))
                    {
                        Dash();
                    }
                    else if(Mathf.Abs(m_mouse.x - Input.mousePosition.x) < Mathf.Abs(m_mouse.y - Input.mousePosition.y))
                    {
                        m_IsSliding = true;
                        Slide();
                    }
                }
        }

        if(Input.GetMouseButtonUp(0)){
            m_IsDashing = false;
            m_IsSliding = false;
        }
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

        

    }

    void Dash() {

        if (m_mouse.x != Input.mousePosition.x)
        {
                m_IsDashing = true;
                Debug.Log("Dasheando");
                transform.position = transform.position + new Vector3(10f,0,0) * Time.deltaTime;
                m_Presed = false;
            
        }
    }


    void Slide() {

        if (m_mouse.y != Input.mousePosition.y)
        {
                if(!m_IsJumping){
                  
                    m_AnimationState = AnimationState.CROUCHING;
                    m_PlayerAnimator.SetInteger("AnimationState", 7);
                    m_Presed = false;
                    Invoke("StopSliding", 0.5f);
                }
                
            
        }
    }
    

    void Jump()
    {

        if (m_NumberOfJumps > 0)
        {
            if(!m_IsSliding){
                m_AnimationState = AnimationState.JUMPING;
                m_IsJumping = true;
                m_NumberOfJumps--;
                if(m_LastJump == 1){
                    if(m_NumberOfJumps == 1){
                        m_NumberOfJumps = 0;
                    }
                }
                m_LastJump = m_NumberOfJumps;
                if(m_NumberOfJumps == 1){
                    m_PlayerAudioManager.PlayJumpSound();
                }else{
                    m_PlayerAudioManager.PlayDoubleJumpSound();
                }
                
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpStrength);
                m_PlayerAnimator.SetInteger("AnimationState", 1);
               
            }
            
        }


    }


    void StopSliding(){
        m_IsSliding = false;
    }

    void StopDashing(){
        m_IsDashing = false;
    }


    void UpdateAnimation()
    {
        if(m_AnimationState == AnimationState.RUNNING)
        {
            m_PlayerAnimator.SetInteger("AnimationState", 3);
        }
        else if(m_AnimationState == AnimationState.LANDING)
        {

        }
        else if(m_AnimationState == AnimationState.CROUCHING)
        {
            if(m_IsSliding){
                m_PlayerAudioManager.PlaySlideSound();
            }
        }
    }


}
