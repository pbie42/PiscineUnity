using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHorizontal : MonoBehaviour
{
	public float MoveEndX;
	public float MoveStartX;
	private float _movedX = 0.0f;
	private bool _moveRight = true;
	private float _waitTimer = 5f;
	private Vector3 _moveSpeed = new Vector3(0.04f, 0, 0);

	// Use this for initialization
	void Start()
	{
		Debug.Log("MoveDistanceX " + MoveEndX);
		_movedX = MoveStartX;
		if (MoveStartX < MoveEndX)
			_moveRight = true;
		else
			_moveRight = false;
	}

	// Update is called once per frame
	void Update()
	{
		_waitTimer -= Time.deltaTime;
		if (_waitTimer <= 0)
		{
			if (_moveRight && _movedX <= MoveEndX)
			{
				gameObject.transform.position += _moveSpeed;
				_movedX += _moveSpeed.x;
			}
			else if (_moveRight && _movedX > MoveEndX)
			{
				_moveRight = false;
				_waitTimer = 5f;
				_movedX = MoveEndX;
			}
			else if (!_moveRight && _movedX >= MoveStartX)
			{
				gameObject.transform.position -= _moveSpeed;
				_movedX -= _moveSpeed.x;
			}
			else if (!_moveRight && _movedX < MoveStartX)
			{
				_moveRight = true;
				_waitTimer = 5f;
				_movedX = MoveStartX;
			}
		}
	}
}
