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
		Debug.Log("zVel: " + zVel.ToString("F4"));
		if (zVel >= -0.05 && zVel <= 0.05)
		{
			_rgbd.velocity = Vector3.zero;
			_rgbd.freezeRotation = true;
			Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.LookAt(targetPosition);
			SetCameraPosition();
		}
		if (Input.GetKey(KeyCode.Space))
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
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			transform.LookAt(targetPosition);
			_rgbd.AddForce(transform.forward * _forceZ);
			_rgbd.AddForce(transform.up * _forceY);
			_forceY = 0.0f;
			_forceZ = 0.0f;
		}
	}


	private void SetCameraPosition()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		_camera.transform.LookAt(targetPosition);
		float newZ = 0.0f;
		if (target.transform.position.z < transform.position.z)
		{
			newZ = 7;
		}
		else if (target.transform.position.z > transform.position.z)
		{
			newZ = -7;
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
