using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	CharacterController player;
	private bool _canSelectFan = false;
	private bool _detectedCamera = false;
	private bool _detectedLight = false;
	private bool _smokeCamera = false;
	private bool _smokeOn = false;
	private float _cameraIncrease = 10.0f;
	private float _detection = 0f;
	private float _detectionDecrease = 1.0f;
	private float _detectionIncrease = 5.0f;
	private float _detectionTotal = 300f;
	private float _moveFB;
	private float _moveLR;
	private float _rotX;
	private float _rotY;
	public float sensitivity = 5.0f;
	public float speed = 5.0f;
	public GameObject camera;
	public GameObject smoke;
	public UnityEngine.UI.Image detectionBar;


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
		// _rotY = Input.GetAxis("Mouse Y") * sensitivity;

		Vector3 movement = new Vector3(_moveLR, 0, _moveFB);
		transform.Rotate(0, _rotX, 0);
		// if (camera.transform.rotation.x > -0.45f && camera.transform.rotation.x < 0.45f)
		// 	camera.transform.Rotate(-_rotY, 0, 0);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);

		if (_detectedLight && _detection < _detectionTotal)
			_detection += _detectionIncrease;
		if (_detectedCamera && _detection < _detectionTotal)
			_detection += _cameraIncrease;
		if (!_smokeOn && _smokeCamera && _detection < _detectionTotal)
			_detection += _cameraIncrease;
		if (!_detectedLight && _detection > 0)
			_detection -= _detectionDecrease;
		detectionBar.fillAmount = _detection / _detectionTotal;
		if (detectionBar.fillAmount >= 0.75)
			detectionBar.color = new Color32(255, 0, 0, 255);
		else
			detectionBar.color = new Color32(255, 255, 255, 255);
		if (detectionBar.fillAmount >= 1)
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);

		if (_canSelectFan && Input.GetKeyDown(KeyCode.E))
		{
			smoke.SetActive(true);
			_smokeOn = true;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger: other.gameObject.tag: " + other.gameObject.tag);
		if (other.gameObject.tag == "Light")
			_detectedLight = true;
		if (other.gameObject.tag == "Camera")
			_detectedCamera = true;
		if (other.gameObject.tag == "SmokeCamera")
			_smokeCamera = true;
		if (other.gameObject.tag == "Fan")
			_canSelectFan = true;
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Trigger: other.gameObject.tag: " + other.gameObject.tag);
		if (other.gameObject.tag == "Light")
			_detectedLight = false;
		if (other.gameObject.tag == "Camera")
			_detectedCamera = false;
		if (other.gameObject.tag == "Fan")
			_canSelectFan = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision: other.gameObject.tag: " + other.gameObject.tag);

	}

	private void OnCollisionExit(Collision other)
	{
		Debug.Log("Collision: other.gameObject.tag: " + other.gameObject.tag);
	}
}
