using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	public gameManager gManager;
	public GameObject pauseMenu;
	public GameObject confirmMenu;
	public GameObject victoryMenu;
	public GameObject failMenu;
	public Texture2D mouse;
	private bool _paused = false;
	private bool _pausedGame = false;
	private bool _confirmedQuit = false;
	private bool _gameStarted = false;
	public UnityEngine.UI.Text speed;
	public UnityEngine.UI.Text victoryGrade;

	public static GameState gState;


	// Use this for initialization
	void Start()
	{
		if (!gState)
			gState = this;
		gManager = gameManager.gm;
		pauseMenu.SetActive(false);
		confirmMenu.SetActive(false);
		Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
	}


	// Update is called once per frame
	void Update()
	{
		if (!_gameStarted)
		{
			PauseGame();
			speed.text = "PRESS PLAY";
		}
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

	public void PauseGame()
	{
		if (!_pausedGame)
		{
			gManager.pause(true);
			_pausedGame = true;
			speed.text = "PAUSED";
		}
	}

	public void PlayGame()
	{
		gManager.pause(false);
		gManager.changeSpeed(1.0f);
		_pausedGame = false;
		_gameStarted = true;
		speed.text = "SPEED: 1x";
	}

	public void DoubleSpeed()
	{
		gManager.pause(false);
		gManager.changeSpeed(2.0f);
		_pausedGame = false;
		_gameStarted = true;
		speed.text = "SPEED: 2x";
	}

	public void QuadrupleSpeed()
	{
		gManager.pause(false);
		gManager.changeSpeed(4.0f);
		_pausedGame = false;
		_gameStarted = true;
		speed.text = "SPEED: 4x";
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

	public void Victory(bool victory)
	{
		if (victory)
		{
			victoryGrade.text = CalcGrade();
			victoryMenu.SetActive(true);
		}
		else
		{
			CalcGrade();
			failMenu.SetActive(true);
		}
	}

	public void Retry()
	{
		int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
	}

	private string CalcGrade()
	{
		float testing = (float)gManager.playerHp / 10;
		Debug.Log("testing: " + testing);
		float gradeScore = (float)gManager.playerEnergy * testing;
		Debug.Log("gManager.playerEnergy: " + gManager.playerEnergy);
		Debug.Log("gManager.playerHp: " + gManager.playerHp);
		Debug.Log("gradeScore: " + gradeScore);

		if (gradeScore < 500)
			return "F";
		if (gradeScore < 1000)
			return "D";
		if (gradeScore < 1500)
			return "C";
		if (gradeScore < 2000)
			return "B";
		if (gradeScore > 200)
			return "A";
		return "F";
	}
}
