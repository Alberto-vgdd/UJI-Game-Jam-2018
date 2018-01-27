using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	// Use this for initialization
	public static GameManager instance;

	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void RestartDream()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
