using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	public gameManager gManager;
	public GameObject pauseMenu;
	public GameObject confirmMenu;
	private bool _paused = false;
	private bool _confirmedQuit = false;
	public UnityEngine.UI.Text resume;
	public UnityEngine.UI.Text exit;


	// Use this for initialization
	void Start()
	{
		pauseMenu.SetActive(false);
		confirmMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_paused)
			{
				UnpauseMenu();
			}
			else
			{
				PauseMenu();
			}
		}
	}

	public void UnpauseMenu()
	{
		gManager.pause(false);
		_paused = false;
		_confirmedQuit = false;
		pauseMenu.SetActive(false);
		confirmMenu.SetActive(false);
	}

	public void PauseMenu()
	{
		gManager.pause(true);
		_paused = true;
		pauseMenu.SetActive(true);
	}

	public void QuitCommand()
	{
		pauseMenu.SetActive(false);
		confirmMenu.SetActive(true);
	}

	public void QuitToMainMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
