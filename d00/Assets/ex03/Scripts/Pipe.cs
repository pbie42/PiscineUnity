using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	[SerializeField] GameObject bird;
	private bool _gameOver = false;
	private bool _up = false;
	private Vector3 _startPosition;
	private Vector3 _speed = new Vector3(0.05f, 0, 0);
	private Vector3 _birdDropSpeed = new Vector3(0f, 0.05f, 0);
	private Vector3 _birdRiseSpeed = new Vector3(0f, 0.03f, 0);
	private float _birdRiseTimer = 5f;
	private float _groundPosY = -3.2f;

	// Use this for initialization
	void Start()
	{
		_startPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
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
			_gameOver = true;
	}

	private void MoveBirdDown()
	{
		bird.transform.position -= _birdDropSpeed;
		if (bird.transform.position.y <= _groundPosY)
			_gameOver = true;
	}

	private void MovePipes()
	{
		gameObject.transform.position -= _speed;
		if (gameObject.transform.position.x <= -4)
			gameObject.transform.position = _startPosition;
	}
}
