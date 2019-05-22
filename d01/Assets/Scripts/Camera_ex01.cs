using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ex01 : MonoBehaviour
{

	public playerScript_ex01 Thomas;
	public playerScript_ex01 John;
	public playerScript_ex01 Claire;
	private playerScript_ex01 _currentCharacter;
	private float _deathY = -30f;

	private bool _thomas = false;
	private bool _john = false;
	private bool _claire = false;


	// Use this for initialization
	void Start()
	{
		_currentCharacter = Thomas;
		_currentCharacter.EnterFocus();
		gameObject.transform.position = _currentCharacter.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 newPosition = _currentCharacter.transform.position + new Vector3(0, 0, -10);
		gameObject.transform.position = newPosition;
		CheckChange();
		CheckEnd();
	}

	private void CheckEnd()
	{
		if (!_thomas && Thomas.IsInEnd())
		{
			Debug.Log("Thomas is in");
			_thomas = true;
		}
		if (!_claire && Claire.IsInEnd())
		{
			Debug.Log("Claire is in");
			_claire = true;
		}
		if (!_john && John.IsInEnd())
		{
			Debug.Log("John is in");
			_john = true;
		}

		if (Thomas.IsInEnd() && Claire.IsInEnd() && John.IsInEnd())
		{
			Debug.Log("You've Won!");
			int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			if (currentSceneIndex + 1 >= 4)
				currentSceneIndex = 0;
			else
				currentSceneIndex += 1;
			UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
		}
		if (Thomas.transform.position.y <= _deathY
			|| Claire.transform.position.y <= _deathY
			|| John.transform.position.y <= _deathY)
		{
			ResetScene();
		}
	}

	private void CheckChange()
	{
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			ChangeFocus(Thomas);
		if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			ChangeFocus(John);
		if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			ChangeFocus(Claire);
		if (Input.GetKeyDown(KeyCode.Backspace))
			ResetScene();
	}

	private void ResetScene()
	{
		int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
	}

	private void ChangeFocus(playerScript_ex01 player)
	{
		_currentCharacter.LeaveFocus();
		_currentCharacter = player;
		_currentCharacter.EnterFocus();
	}
}
