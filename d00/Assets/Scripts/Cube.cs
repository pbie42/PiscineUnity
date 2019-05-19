using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	private Vector3 _speed;
	private float _maxSpeed = 0.15f;
	private float _minSpeed = 0.05f;
	private int _destroyLocation = -7;

	// Use this for initialization
	void Start()
	{
		_speed = new Vector3(0.0f, Random.Range(_minSpeed, _maxSpeed));
	}

	// Update is called once per frame
	void Update()
	{
		transform.position -= _speed;
		if (transform.position.y < _destroyLocation)
			Destroy(gameObject);
	}
}
