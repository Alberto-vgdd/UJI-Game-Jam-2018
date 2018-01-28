using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {


	private float m_OriginalMovementSpeed;
	public float m_MovementSpeed;
	public float m_DashSpeed;

	public float m_JumpStrength;

	private int m_NumberOfJumps = 2;
	public int m_AvailableJumps;

	public enum AnimationStates{RUNNING, JUMP, DASH, FALLING, TURN, SLIDE, LANDING}
	public AnimationStates m_AnimationState;


	bool m_Pressed;
	public int m_Orientation = 1;

	public bool m_IsGrounded = false;
	public bool m_IsDashing = false;
	public bool m_IsSliding = false;
	
	private float m_SlideTimer;
	private float m_DashTimer;

	bool m_StartSliderTimer = false;
	bool m_StartDashTimer = false;

	Vector3 m_LastMousePosition;



	CapsuleCollider2D m_CapsuleCollider2D;
	PlayerAudioManager m_PlayerAudioManager;
	Rigidbody2D m_Rigidbody2D;
	SpriteRenderer m_SpriteRenderer;

	Animator m_PlayerAnimator;





	void Awake(){
		m_CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
		m_PlayerAudioManager = GetComponentInChildren<PlayerAudioManager>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_PlayerAnimator = GetComponentInChildren<Animator>();
		m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Start () {
		m_OriginalMovementSpeed = m_MovementSpeed;
		m_AvailableJumps = m_NumberOfJumps;
		m_DashSpeed = 2*m_MovementSpeed;
	}
	
	void Update () {
		HandleInput();
	}

	void FixedUpdate(){

		CheckIfGrounded();
		CheckIfFalling();

		/*
		if(m_StartSliderTimer)
			CheckForSliderTimer();
		*/

		if(m_StartDashTimer && m_IsDashing)
			CheckForDashTimer();

		Move();
		Gravity();
	}


	void Gravity(){
		m_Rigidbody2D.AddForce(Physics2D.gravity);
	}


	void Move(){
		Vector2 desiredVelocity = new Vector2(m_MovementSpeed * m_Orientation, m_Rigidbody2D.velocity.y);

        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(new Vector2(transform.position.x, transform.position.y) + m_CapsuleCollider2D.offset, 
		m_CapsuleCollider2D.bounds.size * 0.95f, CapsuleDirection2D.Horizontal, 0f, Vector2.right * m_Orientation, 
		m_Rigidbody2D.velocity.x * Time.deltaTime, LayerMask.GetMask("Ground"));

    
        if(hits.Length >= 1 ){
            foreach(RaycastHit2D hit in hits){
				if(Vector2.Angle(hit.normal,desiredVelocity) >= 90){
					desiredVelocity.x = 0;
				}
			}
        }
        m_Rigidbody2D.velocity = desiredVelocity;
        
	}


	/*
	void CheckForSlideTimer(){
		m_SlideTimer += Time.deltaTime;
		if(m_SlideTimer < 2.5f ){

			m_PlayerAnimator.SetInteger("AnimationState",2);

			
		}else{

			m_PlayerAnimator.SetInteger("AnimationState",5);
			SwitchState(AnimationStates.RUNNING);
		}
	}
	*/

	

	void CheckForDashTimer(){
		m_DashTimer += Time.deltaTime;
		if(m_DashTimer >= 2.5f){

			//TRANSICION A RUNNING DESDE DASH
			
			SwitchState(AnimationStates.RUNNING);
		}else{

			//HACER DASH

		}
	}



	void CheckIfGrounded(){
		RaycastHit2D[] hits;
        hits = Physics2D.CapsuleCastAll(new Vector2(transform.position.x, transform.position.y), m_CapsuleCollider2D.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.005f, LayerMask.GetMask("Ground"));
        
		if(hits.Length >=1){
            m_IsGrounded = true;

			// Tocamos el suelo después de falling
			if(m_AnimationState == AnimationStates.FALLING)

				// TRANSICION A RUNNING DESDE LANDING
				SwitchState(AnimationStates.LANDING);
				
        }else{
            m_IsGrounded = false;
        }

	}

	void CheckIfFalling(){
		if(m_Rigidbody2D.velocity.y < 0){
			m_AnimationState = AnimationStates.FALLING;
			m_PlayerAnimator.SetInteger("AnimationStates",8);
		}
			
			
	}



	void RestartJumpsIfNeeded(){
		if(m_AvailableJumps <= 1)
			m_AvailableJumps = m_NumberOfJumps;
	}


	void SwitchState(AnimationStates nextState){

		m_AnimationState = nextState;

		switch(m_AnimationState){


			case AnimationStates.LANDING:
				m_PlayerAnimator.SetInteger("AnimationState",7);
				// .
				// .
				// .
				// Sistema de partículas con polvillo...

				SwitchState(AnimationStates.RUNNING);
				break;


			case AnimationStates.RUNNING:
				m_IsDashing = false;
				m_IsSliding = false;
				//m_PlayerAnimator.SetInteger("AnimationState",0);
				m_MovementSpeed = m_OriginalMovementSpeed;
				//m_StartSliderTimer = false;
				RestartJumpsIfNeeded();

				break;

			case AnimationStates.JUMP:
				if(m_AvailableJumps == 2)
					m_PlayerAudioManager.PlayJumpSound();
				else if(m_AvailableJumps == 1)
					m_PlayerAudioManager.PlayDoubleJumpSound();
				m_AvailableJumps--;
				m_PlayerAnimator.SetInteger("AnimationState",1);
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpStrength);
				break;

			case AnimationStates.FALLING:
				if(m_IsGrounded)
					SwitchState(AnimationStates.RUNNING);
				break;



			case AnimationStates.TURN:
				m_Orientation = m_Orientation * (-1);
				m_SpriteRenderer.flipX = (m_Orientation == 1) ? false : true;
				SwitchState(AnimationStates.RUNNING);
				break;



			case AnimationStates.DASH:
				m_MovementSpeed = m_DashSpeed;
				//m_DashTimer = 0;
				//m_StartDashTimer = true;
				m_IsDashing = true;
				break;



			case AnimationStates.SLIDE:

				m_IsSliding = true;
				m_PlayerAnimator.SetInteger("AnimationState",2);		
				/*		
				m_SlideTimer = 0;
				m_StartSliderTimer = true;
				*/
				break;
		}
	}


	bool CanJump(){ return m_AvailableJumps > 0 && 
	m_AnimationState != AnimationStates.SLIDE && 
	m_AnimationState != AnimationStates.DASH &&
	m_AnimationState != AnimationStates.TURN; }

	void HandleInput(){

		if (Input.GetMouseButtonUp(0) && m_Pressed)
        {				
			if(CanJump()){
				SwitchState(AnimationStates.JUMP);
                m_Pressed = false; 
			}	 
            
		}
		
		
		if(Input.GetMouseButtonDown(0)){
			m_Pressed = true;
            m_LastMousePosition = Input.mousePosition;
		}


        if (Input.GetMouseButton(0))
		{
			if(m_IsGrounded)
			{
				if (Mathf.Abs(m_LastMousePosition.x - Input.mousePosition.x) > 
					Mathf.Abs(m_LastMousePosition.y - Input.mousePosition.y))
				{
					if (m_Orientation * (m_LastMousePosition.x - Input.mousePosition.x) > 0)
					{
						SwitchState(AnimationStates.TURN);
					}
						
					else
					{
						SwitchState(AnimationStates.DASH);
					}
						
				}


				else if(Mathf.Abs(m_LastMousePosition.x - Input.mousePosition.x) < Mathf.Abs(m_LastMousePosition.y - Input.mousePosition.y))
				{
					SwitchState(AnimationStates.SLIDE);
				}
			}
			
			if(Input.GetMouseButtonUp(0)){
				m_IsDashing = false;
				m_IsSliding = false;
				SwitchState(AnimationStates.RUNNING);
			}

		}
     
			

	}


}
