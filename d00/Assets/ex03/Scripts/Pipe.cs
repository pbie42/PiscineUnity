using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	[SerializeField] GameObject bird;
	private bool _gameOver = false;
	private bool _up = false;
	private bool _newPipe = true;
	private float _birdRiseTimer = 3f;
	private float _groundPosY = -3.2f;
	private float _pipeXDiff = 0.6f;
	private float _pipeYDiff = 1.7f;
	private float _timer = 0.0f;
	private int _score = 0;
	private Vector3 _birdDropSpeed = new Vector3(0f, 0.09f, 0);
	private Vector3 _birdRiseSpeed = new Vector3(0f, 0.03f, 0);
	private Vector3 _speed = new Vector3(0.05f, 0, 0);
	private Vector3 _startPosition;

	// Use this for initialization
	void Start()
	{
		_startPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		_timer += Time.deltaTime;
		if (!_gameOver)
		{
			MovePipes();
			if (!_up)
				MoveBirdDown();
			if (Input.GetKeyDown(KeyCode.Space))
				_up = true;
			if (_up)
				MoveBirdUp();
		}
	}

	private void MoveBirdUp()
	{
		_birdRiseTimer -= 0.15f;
		if (_birdRiseTimer <= 0)
		{
			_up = false;
			_birdRiseTimer = 5f;
		}
		bird.transform.position += _birdRiseSpeed;
		if (bird.transform.position.y <= _groundPosY)
			GameOver();
		CheckCollision();
	}

	private void MoveBirdDown()
	{
		bird.transform.position -= _birdDropSpeed;
		if (bird.transform.position.y <= _groundPosY)
			GameOver();
		CheckCollision();
	}

	private void MovePipes()
	{
		gameObject.transform.position -= _speed;
		if (gameObject.transform.position.x <= -4)
			gameObject.transform.position = _startPosition;
	}

	private void CheckCollision()
	{
		float pipeX = gameObject.transform.position.x;
		float pipeY = gameObject.transform.position.y;
		float birdX = bird.transform.position.x;
		float birdY = bird.transform.position.y;
		if (birdX >= pipeX - _pipeXDiff && birdX <= pipeX + _pipeXDiff
			&& (birdY >= pipeY + _pipeYDiff || birdY <= pipeY - _pipeYDiff))
			GameOver();
		if (birdX >= pipeX + _pipeXDiff)
		{
			if (_newPipe)
				IncreaseScoreAndSpeed();
			_newPipe = false;
		}
		else
			_newPipe = true;
	}

	private void IncreaseScoreAndSpeed()
	{
		_score += 5;
		_speed += new Vector3(0.01f, 0, 0);
	}

	private void GameOver()
	{
		_gameOver = true;
		Debug.Log("Score: " + _score + "\nTime: " + Mathf.RoundToInt(_timer) + 's');
	}
}
