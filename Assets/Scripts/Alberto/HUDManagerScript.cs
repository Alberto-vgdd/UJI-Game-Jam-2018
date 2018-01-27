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

	private GameObject pauseMenu;
	private GameObject inGameMenu;
	public GameObject shopMenu;

	private Tienda tienda;

	void Awake()
	{
		instance = this;

		pauseMenu = transform.Find("Pause Menu").gameObject;
		inGameMenu = transform.Find("InGame Menu").gameObject;
		//shopMenu = transform.Find("Tienda").gameObject;
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

	void OpenShop()
	{

		pauseMenu.SetActive(false);
		inGameMenu.SetActive(false);

		shopMenu.SetActive(true);
		tienda.EntrarTienda();
	}

	void CloseShop()
	{
		inGameMenu.SetActive(false);
		tienda.SalirTienda();
		pauseMenu.SetActive(true);
	}
	
}
