using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private Camera _camera;
	public Transform target;
	private Rigidbody _rgbd;
	private Vector3 _direction;
	private bool _increase = true;
	private float _forceY = 0.0f;
	private float _forceZ = 0.0f;
	public float forceIncrease;
	public float forceMaxY;
	public float forceMinY;
	public float forceMaxZ;
	public float forceMinZ;
	public bool _hit = false;

	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
		_camera = GameObject.FindObjectOfType<Camera>();
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
		SetCameraPosition();
	}

	// Update is called once per frame
	void Update()
	{
		float zVel = transform.InverseTransformDirection(_rgbd.velocity).z;
		// Debug.Log("zVel: " + zVel.ToString("F4"));
		if (_hit && (zVel >= -0.05 && zVel <= 0.05 || zVel <= -0.0001))
		{
			_rgbd.velocity = Vector3.zero;
			_rgbd.freezeRotation = true;
			Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.LookAt(targetPosition);
			SetCameraPosition();
		}
		if (Input.GetKey(KeyCode.Space))
		{
			BallHit();
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.LookAt(targetPosition);
			_rgbd.AddForce(transform.forward * _forceZ);
			_rgbd.AddForce(transform.up * _forceY);
			_forceY = 0.0f;
			_forceZ = 0.0f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("rotating");
			transform.Rotate(0, -50f * Time.deltaTime, 0);
			_camera.transform.LookAt(transform.position);
			_camera.transform.Translate(new Vector3(9f, 0, 0) * Time.deltaTime);
			Debug.Log("Vector3.right: " + Vector3.right);
		}
		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("rotating");
			transform.Rotate(0, 50f * Time.deltaTime, 0);
			_camera.transform.LookAt(transform.position);
			// _camera.transform.Translate(new Vector3(-9f, 0, 0) * Time.deltaTime);
			Vector3 offset = _camera.transform.position - transform.position;
			_camera.transform.position = transform.position + (-transform.forward * offset.magnitude);
			Debug.Log("Vector3.left: " + Vector3.left);
		}
	}

	private void BallHit()
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


	private void SetCameraPosition()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		Debug.Log("_camera.transform: " + _camera.transform.position);
		_camera.transform.LookAt(targetPosition);
		Debug.Log("_camera.transform: " + _camera.transform.position);
		float newZ = 0.0f;
		float newX = 0.0f;
		float newY = 0.0f;
		float targetZ = target.transform.position.z;
		float targetX = target.transform.position.x;
		float targetY = target.transform.position.y;
		float cameraZ = _camera.transform.position.z;
		float cameraX = _camera.transform.position.x;
		float cameraY = _camera.transform.position.y;
		if (targetZ < cameraZ)
		{
			newZ = 7;
		}
		else if (targetZ > cameraZ)
		{
			newZ = -9;
		}
		if (targetX > cameraX)
		{
			newX = -7;
		}
		else if (targetX < cameraX)
		{
			newX = 7;
		}

		_camera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + newZ);
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
	}
	private void OnCollisionExit(Collision other)
	{
		_forceY = 0.0f;
		_forceZ = 0.0f;
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
	}
}
