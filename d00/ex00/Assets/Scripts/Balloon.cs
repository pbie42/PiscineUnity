using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

	private bool _gameStarted = false;
	private float _descale = 0.001f;
	private float _gameOverScaleMaxX = 4.0f;
	private float _gameOverScaleMinX = 0.0f;
	private float _gameOverTime = 5.0f;
	private float _gameOverTimer = 0.0f;
	private float _timer = 0.0f;
	private int _breaths = 0;
	private float _coolDown = 0.0f;

	// Use this for initialization
	void Start()
	{
		gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 currentScale = gameObject.transform.localScale;
		if (_gameStarted)
			GameFrame();
		if (Input.GetKeyDown(KeyCode.Space) && _breaths <= 5)
			SpaceBarPressed();
		if (_gameOverTimer >= _gameOverTime
			|| currentScale.x <= _gameOverScaleMinX
			|| currentScale.x >= _gameOverScaleMaxX
			)
			GameOver();
	}

	private void GameFrame()
	{
		_timer += Time.deltaTime;
		_gameOverTimer += Time.deltaTime;
		gameObject.transform.localScale -= new Vector3(_descale, _descale, 0);
	}

	private void SpaceBarPressed()
	{
		_breaths++;
		if (_breaths > 5)
			_coolDown = 0.5f;
		_gameStarted = true;
		_gameOverTimer = 0.0f;
		gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
	}

	private void GameOver()
	{
		Debug.Log("Game Over");
		Debug.Log("Balloon life time: " + Mathf.RoundToInt(_timer) + 's');
		Destroy(gameObject);
	}
}
