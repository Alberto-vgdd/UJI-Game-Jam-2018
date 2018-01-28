using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManagerScript : MonoBehaviour
{
	public static HUDManagerScript instance;

	public const int PAUSE_BUTTON = 0;
	public const int RESUME_BUTTON = 1;
	public const int ENTER_SHOP_BUTTON = 2;
	public const int EXIT_SHOP_BUTTON = 3;
	public const int QUIT_APPLICATION_BUTTON = 4;
	public const int PLAY_GAME_BUTTON = 5;
	public const int TITLE_SCREEN_BUTTON = 6;

	private GameObject pauseMenu;
	private GameObject inGameMenu;
	private GameObject TitleMenu;
	public GameObject shopMenu;

	private Tienda tienda;


	private int currentMenu;

	void Awake()
	{
		instance = this;

		pauseMenu = transform.Find("Pause Menu").gameObject;
		inGameMenu = transform.Find("InGame Menu").gameObject;
		TitleMenu = transform.Find("Title Menu").gameObject;

		tienda = shopMenu.GetComponentInChildren<Tienda>();
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
			case ENTER_SHOP_BUTTON:
				OpenShop();
				break;
			case EXIT_SHOP_BUTTON:
				CloseShop();
				break;
			case QUIT_APPLICATION_BUTTON:
				ExitGame();
				break;
			case PLAY_GAME_BUTTON:
				PlayGame();
				break; 
			case TITLE_SCREEN_BUTTON:
				TittleScreen();
				break;
			default:
				break;
		}
	}

	void PauseGame()
	{
		//Time.timeScale = 0;
		//Time.fixedDeltaTime = 0;

		inGameMenu.SetActive(false);
		pauseMenu.SetActive(true);

		currentMenu = 1;
	}

	void ResumeGame()
	{
		//Time.timeScale = 1;
		//Time.fixedDeltaTime = 1/50f;

		pauseMenu.SetActive(false);
		inGameMenu.SetActive(true);

		currentMenu = 0;
	}

	void OpenShop()
	{


		TitleMenu.SetActive(false);
		shopMenu.SetActive(true);

		tienda.EntrarTienda(currentMenu);

		currentMenu = 3;
	}

	void CloseShop()
	{
		inGameMenu.SetActive(false);
		TitleMenu.SetActive(true);

		currentMenu = tienda.SalirTienda();
	}

	void ExitGame()
	{
		Application.Quit();
	}

	void PlayGame()
	{
		TitleMenu.SetActive(false);
		inGameMenu.SetActive(true);

		// Start Game
	}

	void TittleScreen()
	{
		pauseMenu.SetActive(false);
		TitleMenu.SetActive(true);

		//Back to the menu.
	}
	
}
