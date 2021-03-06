﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

	private bool _coolDown = false;
	private bool _gameStarted = false;
	private float _coolDownTimer = 0.0f;
	private float _descale = 0.003f;
	private float _gameOverScaleMaxX = 2.0f;
	private float _gameOverScaleMinX = 0.0f;
	private float _gameOverTime = 5.0f;
	private float _gameOverTimer = 0.0f;
	private float _increase = 0.05f;
	private float _startScale = 0.2f;
	private float _timer = 0.0f;
	private int _breaths = 0;

	// Use this for initialization
	void Start()
	{
		gameObject.transform.localScale = new Vector3(_startScale, _startScale, 0);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 currentScale = gameObject.transform.localScale;
		if (_gameStarted)
			GameFrame();
		if (Input.GetKeyDown(KeyCode.Space) && _breaths <= 5 && !_coolDown)
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
		_coolDownTimer -= Time.deltaTime;
		if (_coolDownTimer <= 0)
			_coolDown = false;
		_gameOverTimer += Time.deltaTime;
		gameObject.transform.localScale -= new Vector3(_descale, _descale, 0);
	}

	private void SpaceBarPressed()
	{
		_breaths++;
		if (_breaths > 5)
			StartCoolDown();
		_gameStarted = true;
		_gameOverTimer = 0.0f;
		gameObject.transform.localScale += new Vector3(_increase, _increase, 0);
	}

	private void StartCoolDown()
	{
		_coolDownTimer = 0.5f;
		_coolDown = true;
		_breaths = 0;
	}

	private void GameOver()
	{
		Debug.Log("Balloon life time: " + Mathf.RoundToInt(_timer) + 's');
		Destroy(gameObject);
	}
}
