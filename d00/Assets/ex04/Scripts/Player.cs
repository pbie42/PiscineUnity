using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float _diffX = 0.2f;
	private float _diffY = 1f;
	private float _playerMax = 4.5f;
	private int _score = 0;
	private Vector3 _playerSpeed = new Vector3(0, 0.3f, 0);
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void MoveUp()
	{
		if (gameObject.transform.position.y < _playerMax)
			gameObject.transform.position += _playerSpeed;
	}

	public void MoveDown()
	{
		if (gameObject.transform.position.y > -_playerMax)
			gameObject.transform.position -= _playerSpeed;
	}

	public void AddPoint()
	{
		_score++;
	}

	public int GetScore()
	{
		return _score;
	}

	public bool CheckCollision(Vector3 ball)
	{
		float paddleY = gameObject.transform.position.y;
		float paddleX = gameObject.transform.position.x;
		if (paddleX > 0)
		{
			if (ball.x > paddleX - _diffX && (ball.y >= paddleY - _diffY && ball.y <= paddleY + _diffY))
				return true;
		}
		if (paddleX < 0)
		{
			if (ball.x < paddleX + _diffX && (ball.y >= paddleY - _diffY && ball.y <= paddleY + _diffY))
				return true;
		}
		return false;
	}
}
