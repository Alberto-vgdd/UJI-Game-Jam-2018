using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrailScript : MonoBehaviour 
{
	
	private SpriteRenderer[] trailSpriteRenderers;
	private GameObject[] trailGameObjects;
	private float trailTime = 0.1f;
	private float trailTimer = 0f;

	private GameObject playerGameObject;

	// Use this for initialization
	void Awake ()
	{
		
		playerGameObject = this.gameObject;

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		trailTimer += Time.deltaTime;

		if (trailTimer >= trailTime)
		{
			trailTimer = 0;

			GameObject trailObject = Instantiate(playerGameObject,playerGameObject.transform.position+Vector3.forward*0.1f,playerGameObject.transform.rotation);
			trailObject.GetComponent<DashTrailScript>().enabled = false;
			Destroy(trailObject,0.2f);

			SpriteRenderer trailSprite = trailObject.GetComponent<SpriteRenderer>();
			Animator animator = trailObject.GetComponent<Animator>();
			trailSprite.sprite = playerGameObject.GetComponent<SpriteRenderer>().sprite;


			Color originalColor =  new Color(trailSprite.color.r,trailSprite.color.g,trailSprite.color.b,0.5f);
			trailSprite.color = Color.Lerp(originalColor,Color.cyan,0.5f);


		}
	}
}
