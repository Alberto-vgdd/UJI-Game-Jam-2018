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


	public bool m_Pressed;
	public int m_Orientation = 1;

	public bool m_IsGrounded = false;
	public bool m_IsDashing = false;
	public bool m_IsSliding = false;
	public bool m_IsTurning = false;
	
	private float m_SlideTimer;
	private float m_DashTimer;
	private float m_TurnTimer;

	bool m_StartSliderTimer = false;
	bool m_StartDashTimer = false;
	bool m_StartTurnTimer = false;

	Vector3 m_LastMousePosition;



	CapsuleCollider2D m_CapsuleCollider2D;
	PlayerAudioManager m_PlayerAudioManager;
	Rigidbody2D m_Rigidbody2D;
	SpriteRenderer m_SpriteRenderer;

	Animator m_PlayerAnimator;

	ParticleSystem landingParticleSystem;



	void Awake(){
		m_CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
		m_PlayerAudioManager = GetComponentInChildren<PlayerAudioManager>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_PlayerAnimator = GetComponentInChildren<Animator>();
		m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
		landingParticleSystem = GetComponentInChildren<ParticleSystem>();
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

		
		if(m_StartSliderTimer && m_IsSliding)
			CheckForSlideTimer();
		

		if(m_StartDashTimer && m_IsDashing)
			CheckForDashTimer();

		if(m_IsTurning)
			CheckForTurnTimer();
		else
			Move();

		Gravity();

		UpdateCurrentPlayerCoins ();

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


	
	void CheckForSlideTimer(){
		m_SlideTimer += Time.deltaTime;
		if(m_SlideTimer < 0.5f ){

			m_PlayerAnimator.SetInteger("AnimationState",2);

		}else{

			m_PlayerAnimator.SetInteger("AnimationState",5);
			SwitchState(AnimationStates.RUNNING);
		}
	}
	

	

	void CheckForDashTimer(){
		m_DashTimer += Time.deltaTime;
		if(m_DashTimer >= 0.25f){

			//TRANSICION A RUNNING DESDE DASH
			
			SwitchState(AnimationStates.RUNNING);
		}else{

			//HACER DASH

		}
	}

	void CheckForTurnTimer(){
		
		if(m_TurnTimer < 1/3f){		

			m_TurnTimer += Time.deltaTime;	
			

		}else{

			m_IsTurning = false;
			SwitchState(AnimationStates.RUNNING);
			m_Orientation = m_Orientation * (-1);
			m_SpriteRenderer.flipX = (m_Orientation == 1) ? false : true;
			m_PlayerAnimator.SetInteger("AnimationState",100);

			
			
		}
	}



	void CheckIfGrounded(){
		RaycastHit2D[] hits;
        hits = Physics2D.CapsuleCastAll(new Vector2(transform.position.x, transform.position.y), m_CapsuleCollider2D.bounds.size * 0.95f, CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.01f, LayerMask.GetMask("Ground"));
        
		if(hits.Length >=1){
            m_IsGrounded = true;

			// Tocamos el suelo después de falling
			if(m_AnimationState == AnimationStates.FALLING)

				// TRANSICION A RUNNING DESDE LANDING
				SwitchState(AnimationStates.LANDING);
				
        }else{
            m_IsGrounded = false;
			m_AnimationState = AnimationStates.FALLING;
			m_PlayerAnimator.SetInteger("AnimationState",8);
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
				//if(!landingParticleSystem.isPlaying)
					//landingParticleSystem.Play();
				// Sistema de partículas con polvillo...

				SwitchState(AnimationStates.RUNNING);
				break;


			case AnimationStates.RUNNING:

				m_IsDashing = false;
				m_IsSliding = false;
				m_IsTurning = false;
				
				
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
					SwitchState(AnimationStates.LANDING);
				break;



			case AnimationStates.TURN:
				m_IsTurning = true;
				m_TurnTimer = 0;
				Debug.Log("AAAAAAAAAAA girado.");

				m_PlayerAnimator.SetInteger("AnimationState",10);
				m_MovementSpeed = Vector2.zero.x;
			
				
			
				
				break;



			case AnimationStates.DASH:
				m_MovementSpeed = m_DashSpeed;
				m_DashTimer = 0;
				m_StartDashTimer = true;
				m_IsDashing = true;
				break;



			case AnimationStates.SLIDE:

				m_IsSliding = true;
				m_PlayerAnimator.SetInteger("AnimationState",2);		
				
				m_SlideTimer = 0;
				m_StartSliderTimer = true;
			
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
			if(m_IsGrounded && !m_IsTurning && !m_IsDashing && !m_IsSliding)
			{
				if (Mathf.Abs(m_LastMousePosition.x - Input.mousePosition.x) > Mathf.Abs(m_LastMousePosition.y - Input.mousePosition.y)) 
				//if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.magnitude * Time.deltaTime > 1)
				{
					if (m_Orientation * (m_LastMousePosition.x - Input.mousePosition.x) > 0 )
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

		}
     
			

	}

	void UpdateCurrentPlayerCoins()
	{
		if ((m_Rigidbody2D.velocity.magnitude * Time.deltaTime / 2f) >= 0.04f) 
		{
			GlobalData.currentInstance.expPlayer = GlobalData.currentInstance.expPlayer + (m_Rigidbody2D.velocity.magnitude * Time.deltaTime / 2f);
		}

	}


}
