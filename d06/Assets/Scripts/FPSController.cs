using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	public float speed = 5.0f;
	public float sensitivity = 5.0f;
	public UnityEngine.UI.Image detectionBar;
	private float _detection = 0f;
	private bool _detectedLight = false;
	private float _detectionTotal = 200f;

	CharacterController player;
	public GameObject camera;

	private float _moveFB;
	private float _moveLR;
	private float _rotX;
	private float _rotY;


	// Use this for initialization
	void Start()
	{
		player = GetComponent<CharacterController>();
		detectionBar.fillAmount = 0;
	}

	// Update is called once per frame
	void Update()
	{
		_moveFB = Input.GetAxis("Vertical") * speed;
		_moveLR = Input.GetAxis("Horizontal") * speed;

		_rotX = Input.GetAxis("Mouse X") * sensitivity;
		_rotY = Input.GetAxis("Mouse Y") * sensitivity;

		Vector3 movement = new Vector3(_moveLR, 0, _moveFB);
		transform.Rotate(0, _rotX, 0);
		// if (camera.transform.rotation.x > -0.45f && camera.transform.rotation.x < 0.45f)
		// 	camera.transform.Rotate(-_rotY, 0, 0);
		Debug.Log("camera.transform.rotation.x: " + camera.transform.rotation.x);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);

		if (_detectedLight && _detection < _detectionTotal)
			_detection += 5f;
		if (!_detectedLight && _detection > 0)
			_detection -= 5f;
		detectionBar.fillAmount = _detection / _detectionTotal;

	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger: other.gameObject.tag: " + other.gameObject.tag);
		_detectedLight = true;
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Trigger: other.gameObject.tag: " + other.gameObject.tag);
		_detectedLight = false;
	}
}
