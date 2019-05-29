using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	CharacterController player;
	private bool _canSelectDocument = false;
	private bool _canSelectFan = false;
	private bool _canSelectKey = false;
	private bool _canSelectLock = false;
	private bool _detectedCamera = false;
	private bool _detectedLight = false;
	private bool _hasDocument = false;
	private bool _hasKey = false;
	private bool _doorUnlocked = false;
	private bool _smokeCamera = false;
	private bool _smokeOn = false;
	private bool _showInfo = false;
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
	public GameObject door;
	public GameObject keyCard;
	public GameObject smoke;
	public UnityEngine.UI.Image detectionBar;
	public Canvas infoCanvas;
	public UnityEngine.UI.Text infoText;


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

		if (_showInfo)
		{
			infoCanvas.gameObject.SetActive(true);
			if (_canSelectFan)
				infoText.text = "Press E To Activate Fan";
			else if (_canSelectKey)
				infoText.text = "Press E To Grab Key Card";
			else if (_canSelectLock && !_hasKey)
				infoText.text = "Must find the key card to unlock the door";
			else if (_canSelectLock && _hasKey)
				infoText.text = "Press E to Unlock The Door";
			else if (_canSelectDocument && !_hasDocument)
				infoText.text = "Press E to Take Document and end game";
			else
				infoText.text = "";
		}
		else
			infoCanvas.gameObject.SetActive(false);
		if (_canSelectFan && Input.GetKeyDown(KeyCode.E))
		{
			smoke.SetActive(true);
			_smokeOn = true;
		}
		if (_canSelectKey && Input.GetKeyDown(KeyCode.E))
		{
			keyCard.SetActive(false);
			_hasKey = true;
		}
		if (_canSelectLock && _hasKey && Input.GetKeyDown(KeyCode.E))
		{
			door.SetActive(false);
			_doorUnlocked = true;
		}
		if (_canSelectDocument && Input.GetKeyDown(KeyCode.E))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Light")
			_detectedLight = true;
		if (other.gameObject.tag == "Camera")
			_detectedCamera = true;
		if (other.gameObject.tag == "SmokeCamera")
			_smokeCamera = true;
		if (other.gameObject.tag == "Fan" && !_smokeOn)
		{
			_showInfo = true;
			_canSelectFan = true;
		}
		if (other.gameObject.tag == "Key" && !_hasKey)
		{
			_showInfo = true;
			_canSelectKey = true;
		}
		if (other.gameObject.tag == "Lock" && !_doorUnlocked)
		{
			_showInfo = true;
			_canSelectLock = true;
		}
		if (other.gameObject.tag == "Document" && !_hasDocument)
		{
			_showInfo = true;
			_canSelectDocument = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Light")
			_detectedLight = false;
		if (other.gameObject.tag == "Camera")
			_detectedCamera = false;
		if (other.gameObject.tag == "Fan")
		{
			_showInfo = false;
			_canSelectFan = false;
		}
		if (other.gameObject.tag == "Key")
		{
			_showInfo = false;
			_canSelectKey = false;
		}
		if (other.gameObject.tag == "Lock")
		{
			_showInfo = false;
			_canSelectLock = false;
		}
		if (other.gameObject.tag == "Document")
		{
			_showInfo = false;
			_canSelectDocument = false;
		}
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
