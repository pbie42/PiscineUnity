using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHead : MonoBehaviour
{

	private Camera _camera;
	private Vector3 _tankPos;
	private float _moveFB;
	private float _moveLR;
	private float _rotX;
	private float _rotY;
	public float sensitivity = 5.0f;
	public float speed = 5.0f;

	// Use this for initialization
	void Start()
	{
		_camera = GameObject.FindObjectOfType<Camera>();
		_tankPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		// _moveFB = Input.GetAxis("Vertical") * speed;
		// _moveLR = Input.GetAxis("Horizontal") * speed;

		// _rotX = Input.GetAxis("Mouse X") * sensitivity;
		// // _rotY = Input.GetAxis("Mouse Y") * sensitivity;

		// Vector3 movement = new Vector3(_moveLR, 0, _moveFB);
		// transform.Rotate(0, 0, _rotX);
		// if (camera.transform.rotation.x > -0.45f && camera.transform.rotation.x < 0.45f)
		// 	camera.transform.Rotate(-_rotY, 0, 0);
		// movement = transform.rotation * movement;
		// player.Move(movement * Time.deltaTime);
	}
}
