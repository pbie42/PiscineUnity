using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	private Vector2 _screenBounds;
	private Vector3 _speed;
	private float _maxSpeed = 0.2f;
	private float _minSpeed = 0.05f;

	// Use this for initialization
	void Start()
	{
		_screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		_speed = new Vector3(0.0f, Random.Range(_minSpeed, _maxSpeed));
	}

	// Update is called once per frame
	void Update()
	{
		transform.position -= _speed;
		if (transform.position.y < -15)
			Destroy(gameObject);
	}
}
