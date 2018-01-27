using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 300f;
    public float jump = 200f;
    Rigidbody2D p_rigidbody2D;
	// Use this for initialization
	void Awake () {

        p_rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Move();
       
	}

    void Move() {


        
        p_rigidbody2D.velocity = Vector2.right * speed * Time.deltaTime + Vector2.up * p_rigidbody2D.velocity.y;

        if (Input.GetKeyDown("space")) {

            p_rigidbody2D.velocity = Vector2.right * speed * Time.deltaTime + Vector2.up * jump * Time.deltaTime;
        }
    }
}
