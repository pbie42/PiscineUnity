using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour
{

	private bool _grounded = true;
	private bool _inEndPos = false;
	private bool _focused = false;
	private float _lowJump = 400f;
	private float _highJump = 550f;
	private float _medJump = 450f;
	private float _lowSpeed = 0.05f;
	private float _medSpeed = 0.1f;
	private float _highSpeed = 0.15f;
	private float _endDiff = 0.3f;
	private Rigidbody2D _rgbd;
	private Vector3 _startPos;
	public GameObject _end;

	// Use this for initialization
	void Start()
	{
		_startPos = gameObject.transform.position;
		_rgbd = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_focused)
		{
			Move();
			if (Input.GetKeyDown(KeyCode.Space) && _grounded)
				Jump();
		}
	}

	private void Move()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			if (gameObject.tag == "Claire")
				MoveRightLeft(_lowSpeed, 1);
			if (gameObject.tag == "Thomas")
				MoveRightLeft(_medSpeed, 1);
			if (gameObject.tag == "John")
				MoveRightLeft(_highSpeed, 1);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (gameObject.tag == "Claire")
				MoveRightLeft(_lowSpeed, -1);
			if (gameObject.tag == "Thomas")
				MoveRightLeft(_medSpeed, -1);
			if (gameObject.tag == "John")
				MoveRightLeft(_highSpeed, -1);
		}
	}

	private void MoveRightLeft(float speed, int direction)
	{
		gameObject.transform.position += (new Vector3((speed * direction), 0, 0));
		CheckEnd();
	}

	private void Jump()
	{
		if (gameObject.tag == "Claire")
			_rgbd.AddForce(new Vector2(0, _lowJump));
		else if (gameObject.tag == "Thomas")
			_rgbd.AddForce(new Vector2(0, _medJump));
		else if (gameObject.tag == "John")
			_rgbd.AddForce(new Vector2(0, _highJump));
		_grounded = false;
	}

	private void CheckEnd()
	{
		float playerX = gameObject.transform.position.x;
		float playerY = gameObject.transform.position.y;
		float endX = _end.transform.position.x;
		float endY = _end.transform.position.y;
		if (playerX <= endX + _endDiff && playerX >= endX - _endDiff
			&& playerY <= endY + _endDiff && playerY >= endY - _endDiff)
			_inEndPos = true;
		else
			_inEndPos = false;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		string tag = other.gameObject.tag;
		if (tag == "Bottom" || tag == "Thomas" || tag == "John" || tag == "Claire" || tag == "Button")
			_grounded = true;
	}

	private void OnCollisionExit2D(Collision2D other)
	{

	}

	public void EnterFocus()
	{
		_focused = true;
	}

	public void LeaveFocus()
	{
		_focused = false;
	}

	public void IsGrounded(bool grounded)
	{
		_grounded = grounded;
	}

	public void ResetPos()
	{
		gameObject.transform.position = _startPos;
	}

	public bool IsInEnd()
	{
		return _inEndPos;
	}
}
