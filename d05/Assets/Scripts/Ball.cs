using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private bool _grounded = true;
	private bool _increase = true;
	private bool _startMeter = false;
	private float _forceY = 0.0f;
	private float _forceZ = 0.0f;
	private FlyCam _camera;
	private Rigidbody _rgbd;
	private Vector3 _direction;
	private Vector3 _startPos;

	public bool _hit = false;
	public float forceIncrease;
	public float forceMaxY;
	public float forceMaxZ;
	public float forceMinY;
	public float forceMinZ;
	public GameObject arrow;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
		_camera = GameObject.FindObjectOfType<FlyCam>();
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
		_startPos = transform.position;
		SetCameraPosition();
	}

	// Update is called once per frame
	void Update()
	{
		float zVel = transform.InverseTransformDirection(_rgbd.velocity).z;
		if (_hit && (zVel >= -1 && zVel <= 0 || zVel <= -0.0001))
		{
			_rgbd.velocity = Vector3.zero;
			_rgbd.freezeRotation = true;
			_hit = false;
			Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.LookAt(targetPosition);
			SetCameraPosition();
			SetArrowPosition();
		}
		if (AnyMoveKeyDown())
		{
			_camera.canMove = true;
			arrow.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_camera.canMove)
			{
				_camera.canMove = false;
				arrow.SetActive(true);
				SetCameraPosition();
			}
			else if (!_camera.canMove && !_startMeter)
				_startMeter = true;
			else if (!_camera.canMove && _startMeter)
			{
				_hit = true;
				_grounded = false;
				HitBall();
				_startMeter = false;
			}
		}
		if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.A))
			RotateBall(-50f);
		if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.D))
			RotateBall(50f);
		if (_startMeter)
			BallMeter();
	}

	private void HitBall()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		_rgbd.AddForce(transform.forward * _forceZ);
		_rgbd.AddForce(transform.up * _forceY);
		_forceY = 0.0f;
		_forceZ = 0.0f;
	}

	private bool AnyMoveKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
			return true;
		return false;
	}

	private void RotateBall(float dir)
	{
		Vector3 ballPos = transform.position;
		transform.Rotate(0, dir * Time.deltaTime, 0);
		RotateCamera(ballPos);
		RotateArrow(ballPos);
		Debug.Log("_camera.transform.position: " + _camera.transform.position);
	}

	private void RotateArrow(Vector3 ballPos)
	{
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 112)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 112)
		{
			offsetY -= 1;
		}
		Vector3 offset = arrow.transform.position - ballPos;
		arrow.transform.position = new Vector3(ballPos.x, ballPos.y, ballPos.z) + (transform.forward * 50);
		arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y + offsetY, arrow.transform.position.z);
		arrow.transform.LookAt(new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z));
		arrow.transform.Rotate(18, 0, 0);
	}

	private void RotateCamera(Vector3 ballPos)
	{
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 104)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 104)
		{
			offsetY -= 1;
		}
		Vector3 offset = _camera.transform.position - ballPos;
		_camera.transform.position = new Vector3(ballPos.x, ballPos.y, ballPos.z) + (-transform.forward * 8);
		_camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + offsetY, _camera.transform.position.z);
		_camera.transform.LookAt(new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z));
	}

	private void SetCameraPosition()
	{
		Vector3 ballPos = transform.position;
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 104)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 104)
		{
			offsetY -= 1;
		}
		Vector3 offset = _camera.transform.position - ballPos;
		_camera.transform.position = new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z) + (-transform.forward * 8);
		_camera.transform.LookAt(new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z));
	}

	private void SetArrowPosition()
	{
		Vector3 ballPos = transform.position;
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 112)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 112)
		{
			offsetY -= 1;
		}
		Vector3 offset = arrow.transform.position - ballPos;
		arrow.transform.position = new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z) + (-transform.forward * 8);
		arrow.transform.LookAt(new Vector3(ballPos.x, ballPos.y + offsetY, ballPos.z));
	}

	private void BallMeter()
	{
		if (_increase && _forceY <= forceMaxY && _forceZ <= forceMaxZ)
		{
			_forceY += forceIncrease;
			_forceZ += forceIncrease;
			Debug.Log("_forceY: " + _forceY);
			Debug.Log("_forceZ: " + _forceZ);
		}
		if (_increase && _forceY > forceMaxY && _forceZ > forceMaxZ)
		{
			_increase = false;
		}
		if (!_increase && _forceY >= forceMinY && _forceZ >= forceMinZ)
		{
			_forceY -= forceIncrease;
			_forceZ -= forceIncrease;
			Debug.Log("_forceY: " + _forceY);
			Debug.Log("_forceZ: " + _forceZ);
		}
		if (!_increase && _forceY < forceMinY && _forceZ < forceMinZ)
		{
			_increase = true;
		}
	}

	private void FollowBallTurn()
	{
		_camera.transform.LookAt(transform.position);
		_camera.transform.Translate(Vector3.right * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
		if (other.gameObject.name == "Terrain")
			_grounded = true;
		if (other.gameObject.layer == 4)
		{
			Debug.Log("RESET HOLE!!!!!!!!!!!!!!!!!");
			ResetHole();
		}
	}

	private void ResetHole()
	{
		transform.position = _startPos;
		_rgbd.velocity = Vector3.zero;
		_rgbd.freezeRotation = true;
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
		_hit = false;
	}

	private void OnCollisionExit(Collision other)
	{
		_forceY = 0.0f;
		_forceZ = 0.0f;
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
	}
}
