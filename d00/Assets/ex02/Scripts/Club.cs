using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
	[SerializeField] GameObject ball;
	private bool _ballMoving = false;
	private bool _ballUp = true;
	private bool _clubMoving = false;
	private bool _gameOver = false;
	private bool _up = true;
	private float _border = 5.5f;
	private float _diminishPower = 0.005f;
	private float _holeBottom = 3.75f;
	private float _holeTop = 4.25f;
	private float _power = 0.0f;
	private float _powerIncrease = 0.01f;
	private int _holePos = 4;
	private int _score = -15;
	private Vector3 _clubMovement = new Vector3(0f, 0.05f, 0);
	private Vector3 _currentPos;

	// Use this for initialization
	void Start()
	{
		_currentPos = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (!_gameOver)
		{
			if (!_clubMoving)
				_currentPos = gameObject.transform.position;
			if (Input.GetKey(KeyCode.Space) && !_ballMoving)
				MoveClub();
			if (!Input.GetKey(KeyCode.Space))
				PreviousPositionClub();
			if (_clubMoving == false && _power > 0)
				HandleMovement();
			if (_power <= 0 && _ballMoving == true)
				ResetObjects();
		}
	}

	private void HandleMovement()
	{
		_ballMoving = true;
		_power = BallMove(_power);
		float ballPosY = ball.transform.position.y;
		if (ballPosY <= _holeTop && ballPosY >= _holeBottom && _power <= 0.1f)
			GameOver();
	}

	private void ResetObjects()
	{
		_power = 0.0f;
		_clubMoving = false;
		_ballMoving = false;
		RepositionClub();
		_score += 5;
		Debug.Log("Score: " + _score);
	}

	private void GameOver()
	{
		_gameOver = true;
		ball.transform.position += new Vector3(0, 0, 5);
	}

	private void MoveClub()
	{
		_clubMoving = true;
		_power += _powerIncrease;
		if (ball.transform.position.y < _holePos)
			gameObject.transform.position -= _clubMovement;
		else
			gameObject.transform.position += _clubMovement;
	}

	private void RepositionClub()
	{
		Vector3 ballPos = ball.transform.position;
		if (ballPos.y < 4)
		{
			gameObject.transform.position = ballPos - new Vector3(0.21f, -0.25f, 0);
			if (!_up)
			{
				_up = true;
				RotateClub();
			}
		}
		else
		{
			gameObject.transform.position = ballPos - new Vector3(0.21f, 0.25f, 0);
			if (_up)
			{
				_up = false;
				RotateClub();
			}
		}
	}

	private void RotateClub()
	{
		gameObject.transform.Rotate(180, 0, 0);
	}

	private void PreviousPositionClub()
	{
		_clubMoving = false;
		gameObject.transform.position = _currentPos;
	}

	private float BallMove(float power)
	{
		BallHandleMovement(power);
		BallMovementDirection();
		power -= _diminishPower;
		BallHandleNextMovementDirection(power);
		return power;
	}

	private void BallHandleMovement(float power)
	{
		if (_ballUp)
			ball.transform.position += new Vector3(0, power, 0);
		else
			ball.transform.position -= new Vector3(0, power, 0);
	}

	private void BallMovementDirection()
	{
		if (ball.transform.position.y > _border)
			_ballUp = false;
		if (ball.transform.position.y < -_border)
			_ballUp = true;
	}

	private void BallHandleNextMovementDirection(float power)
	{
		if (power <= 0 && ball.transform.position.y < 4)
			_ballUp = true;
		else if (power <= 0 && ball.transform.position.y > 4)
			_ballUp = false;
	}
}
