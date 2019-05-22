using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour
{

	private bool _grounded = true;
	private bool _focused = false;
	private float _lowJump = 400f;
	private float _highJump = 550f;
	private float _medJump = 450f;
	private float _forceX = 0.1f;
	private Rigidbody2D _rgbd;
	private Vector3 _startPos;

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
			if (Input.GetKey(KeyCode.RightArrow))
				gameObject.transform.position += (new Vector3(_forceX, 0, 0));
			if (Input.GetKey(KeyCode.LeftArrow))
				gameObject.transform.position += (new Vector3(-_forceX, 0, 0));
			if (Input.GetKeyDown(KeyCode.Space) && _grounded)
				Jump();
		}
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

	private void OnCollisionExit2D(Collision2D other)
	{
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		string tag = other.gameObject.tag;
		if (tag == "Bottom" || tag == "Thomas" || tag == "John" || tag == "Claire")
			_grounded = true;
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
}
