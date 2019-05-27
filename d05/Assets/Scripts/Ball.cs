using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
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
				_forceY += forceIncrease;
				_forceZ += forceIncrease;
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
			_rgbd.AddForce(transform.forward * _forceZ);
			_rgbd.AddForce(transform.up * _forceY);
			_forceY = 0.0f;
			_forceZ = 0.0f;
		}
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
