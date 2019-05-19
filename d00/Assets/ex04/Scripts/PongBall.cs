using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
	[SerializeField] Player[] players;
	private bool _ballRight = true;
	private bool _ballUp = true;
	private float _border = 5.3f;
	private float _goal = 8f;
	private Vector3 _ballSpeedRight = new Vector3(0.1f, 0, 0);
	private Vector3 _ballSpeedUp = new Vector3(0, 0.1f, 0);
	private Vector3 _ballStartPos;

	// Use this for initialization
	void Start()
	{
		_ballStartPos = gameObject.transform.position;
		LaunchBall();
	}

	// Update is called once per frame
	void Update()
	{
		MoveBall();
		CheckCollisions();
		if (Input.GetKey(KeyCode.DownArrow))
			players[1].MoveDown();
		if (Input.GetKey(KeyCode.UpArrow))
			players[1].MoveUp();
		if (Input.GetKey(KeyCode.S))
			players[0].MoveDown();
		if (Input.GetKey(KeyCode.W))
			players[0].MoveUp();
	}

	private void LaunchBall()
	{
		gameObject.transform.position = _ballStartPos;
		_ballRight = Random.Range(0, 2) == 1 ? true : false;
		_ballUp = Random.Range(0, 2) == 1 ? true : false;
	}

	private void MoveBall()
	{
		if (_ballRight)
			gameObject.transform.position += _ballSpeedRight;
		else
			gameObject.transform.position -= _ballSpeedRight;
		if (_ballUp)
			gameObject.transform.position += _ballSpeedUp;
		else
			gameObject.transform.position -= _ballSpeedUp;
	}

	private void CheckCollisions()
	{
		CheckBorderCollision();
		CheckGoalCollision();
		CheckPlayerCollision();
	}

	private void CheckBorderCollision()
	{
		float ballY = gameObject.transform.position.y;
		float ballX = gameObject.transform.position.x;
		if (ballY >= _border || ballY <= -_border)
			_ballUp = !_ballUp;
	}

	private void CheckGoalCollision()
	{
		float ballY = gameObject.transform.position.y;
		float ballX = gameObject.transform.position.x;
		if (ballX >= _goal)
		{
			players[0].AddPoint();
			PointScored();
		}
		else if (ballX <= -_goal)
		{
			players[1].AddPoint();
			PointScored();
		}
	}

	private void CheckPlayerCollision()
	{
		Vector3 ballPos = gameObject.transform.position;
		if (players[0].CheckCollision(ballPos) || players[1].CheckCollision(ballPos))
			_ballRight = !_ballRight;
	}

	private void PointScored()
	{
		Debug.Log("Player 1: " + players[0].GetScore() + " | Player 2: " + players[1].GetScore());
		LaunchBall();
	}
}
