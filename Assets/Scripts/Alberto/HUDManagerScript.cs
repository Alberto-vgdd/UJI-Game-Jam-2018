using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManagerScript : MonoBehaviour
{
	public static HUDManagerScript instance;

	public const int PAUSE_BUTTON = 0;
	public const int RESUME_BUTTON = 1;

	private GameObject pauseMenu;
	private GameObject inGameMenu;

	void Awake()
	{
		instance = this;

		pauseMenu = transform.Find("Pause Menu").gameObject;
		inGameMenu = transform.Find("InGame Menu").gameObject;
	}

	public void UseButton(int buttonId)
	{
		switch (buttonId)
		{
			case PAUSE_BUTTON:
				PauseGame();
				break;
			case RESUME_BUTTON:
				ResumeGame();
				break;
			default:
				break;
		}
	}

	void PauseGame()
	{
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0;

		inGameMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}

	void ResumeGame()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = 1/50f;

		pauseMenu.SetActive(false);
		inGameMenu.SetActive(true);
	}
	
}
