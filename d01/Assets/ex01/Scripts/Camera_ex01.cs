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
		if (Thomas.IsInEnd() && Claire.IsInEnd() && John.IsInEnd())
		{
			Debug.Log("You've Won!");
			int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
		}
		if (Thomas.transform.position.y <= _deathY
			|| Claire.transform.position.y <= _deathY
			|| John.transform.position.y <= _deathY)
			ResetScene();
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
		Thomas.ResetPos();
		John.ResetPos();
		Claire.ResetPos();
		ChangeFocus(Thomas);
	}

	private void ChangeFocus(playerScript_ex01 player)
	{
		_currentCharacter.LeaveFocus();
		_currentCharacter = player;
		_currentCharacter.EnterFocus();
	}
}
