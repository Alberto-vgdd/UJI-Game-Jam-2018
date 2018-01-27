using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	public Transform m_CameraTarget;
	public float m_CameraSmoothTime = 0.1f;
	Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.SmoothDamp(transform.position, m_CameraTarget.position - new Vector3(0,-1.5f,10), ref velocity, m_CameraSmoothTime);
	}
}
