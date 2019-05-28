using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public Club putter;
	public Club wood;
	public Club wedge;
	public Club iron;
	public GameObject tee2;
	public GameObject tee3;
	public GameObject hole1;
	public GameObject hole2;
	public GameObject hole3;
	private GameObject _targetHole;
	private Club _currentClub;
	private bool _grounded = true;
	private bool _increase = true;
	private bool _startMeter = false;
	private bool _canShoot = true;
	private float _forceY = 0.0f;
	private float _forceZ = 0.0f;
	private FlyCam _camera;
	private int _shots = 0;
	private int _currentHole = 0;
	private int _clubNum = 0;
	private Rigidbody _rgbd;
	private Vector3 _ballPos;
	private Vector3 _direction;
	private Vector3 _startPos;
	public Canvas _canvas;
	public Canvas _card;
	public Canvas _between;

	public bool _hit = false;
	public bool _holeDone = false;
	public float forceIncrease;
	public float forceMaxY;
	public float forceMaxZ;
	public float forceMinY;
	public float forceMinZ;
	public GameObject arrow;
	public UnityEngine.UI.Image powerBar;
	public UnityEngine.UI.Text shotsText;
	public UnityEngine.UI.Text shotsHole1;
	public UnityEngine.UI.Text shotsHole2;
	public UnityEngine.UI.Text shotsHole3;
	public UnityEngine.UI.Text shotsBetween;
	public UnityEngine.UI.Text clubText;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
		_camera = GameObject.FindObjectOfType<FlyCam>();
		_currentClub = wood;
		LookAtTarget();
		_startPos = transform.position;
		_ballPos = _startPos;
		RotateCamera();
		RotateArrow();
		shotsText.text = "Shots: " + _shots;
		shotsHole1.text = "-";
		shotsHole2.text = "-";
		shotsHole3.text = "-";
		powerBar.fillAmount = 0;
		_targetHole = hole1;
	}

	// Update is called once per frame
	void Update()
	{
		_ballPos = transform.position;
		float zVel = transform.InverseTransformDirection(_rgbd.velocity).z;
		if (!_holeDone)
		{
			if (_hit && (zVel >= -1 && zVel <= 0 || zVel <= -0.0001))
				StopBall();
			if (AnyMoveKeyDown())
			{
				_camera.canMove = true;
				arrow.SetActive(false);
				_canvas.gameObject.SetActive(false);
			}
			if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				NextClub();
			}
			if (Input.GetKey(KeyCode.Tab))
				_card.gameObject.SetActive(true);
			else
				_card.gameObject.SetActive(false);
			if (_canShoot)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if (_camera.canMove)
					{
						_camera.canMove = false;
						arrow.SetActive(true);
						_canvas.gameObject.SetActive(true);
						RotateCamera();
					}
					else if (!_camera.canMove && !_startMeter)
						_startMeter = true;
					else if (!_camera.canMove && _startMeter)
					{
						_hit = true;
						_grounded = false;
						_canShoot = false;
						HitBall();
						_startMeter = false;
					}
				}
				if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.A))
					RotateBall(-50f);
				if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.D))
					RotateBall(50f);
				if (_startMeter)
					BetterBallMeter();
			}
		}
	}

	private void NextClub()
	{
		_clubNum++;
		if (_clubNum >= 4)
			_clubNum = 0;
		if (_clubNum == 0)
			_currentClub = wood;
		if (_clubNum == 1)
			_currentClub = putter;
		if (_clubNum == 2)
			_currentClub = wedge;
		if (_clubNum == 3)
			_currentClub = iron;
		clubText.text = "Club: " + _currentClub.name;
	}

	private void StopBall()
	{
		_rgbd.velocity = Vector3.zero;
		_rgbd.freezeRotation = true;
		_hit = false;
		_canShoot = true;
		LookAtTarget();
		arrow.SetActive(true);
		_canvas.gameObject.SetActive(true);
		_camera.canMove = false;
		powerBar.fillAmount = 0;
		RotateCamera();
		RotateArrow();
	}

	private void LookAtTarget()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
	}

	private void HitBall()
	{
		_rgbd.AddForce(transform.forward * _forceZ);
		_rgbd.AddForce(transform.up * _forceY);
		_shots++;
		MarkCard();
		shotsText.text = "Shots: " + _shots;
		_forceY = 0.0f;
		_forceZ = 0.0f;
	}

	private void RotateBall(float dir)
	{
		transform.Rotate(0, dir * Time.deltaTime, 0);
		RotateCamera();
		RotateArrow();
		Debug.Log("_camera.transform.position: " + _camera.transform.position);
	}

	private void RotateArrow()
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
		Vector3 offset = arrow.transform.position - _ballPos;
		arrow.transform.position = new Vector3(_ballPos.x, _ballPos.y, _ballPos.z) + (transform.forward * 50);
		arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y + offsetY, arrow.transform.position.z);
		arrow.transform.LookAt(new Vector3(_ballPos.x, _ballPos.y + offsetY, _ballPos.z));
		arrow.transform.Rotate(18, 0, 0);
	}

	private void RotateCamera()
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
		Vector3 offset = _camera.transform.position - _ballPos;
		_camera.transform.position = new Vector3(_ballPos.x, _ballPos.y, _ballPos.z) + (-transform.forward * 8);
		_camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + offsetY, _camera.transform.position.z);
		_camera.transform.LookAt(new Vector3(_ballPos.x, _ballPos.y + offsetY, _ballPos.z));
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
		powerBar.fillAmount = _forceY / forceMaxY;
	}

	private void BetterBallMeter()
	{
		if (_increase && _forceZ <= _currentClub.maxZ)
		{
			_forceZ += forceIncrease;
			Debug.Log("_forceY: " + _forceY);
			Debug.Log("_forceZ: " + _forceZ);
		}
		if (_increase && _forceZ > _currentClub.maxZ)
		{
			_increase = false;
		}
		if (!_increase && _forceZ >= _currentClub.minZ)
		{
			_forceZ -= forceIncrease;
			Debug.Log("_forceY: " + _forceY);
			Debug.Log("_forceZ: " + _forceZ);
		}
		if (!_increase && _forceZ < _currentClub.minZ)
		{
			_increase = true;
		}
		float forcePercentage = _forceZ / _currentClub.maxZ;
		powerBar.fillAmount = forcePercentage;
		_forceY = _currentClub.maxY * forcePercentage;

	}

	private bool AnyMoveKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
			return true;
		return false;
	}

	private void ResetHole()
	{
		transform.position = _startPos;
		_ballPos = _startPos;
		_rgbd.velocity = Vector3.zero;
		_rgbd.freezeRotation = true;
		LookAtTarget();
		RotateCamera();
		RotateArrow();
		powerBar.fillAmount = 0;
		_hit = false;
	}

	private void MarkCard()
	{
		if (_currentHole == 0)
			shotsHole1.text = "" + _shots;
		if (_currentHole == 1)
			shotsHole2.text = "" + _shots;
		if (_currentHole == 2)
			shotsHole3.text = "" + _shots;
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
		if (other.gameObject.name == "Terrain")
			_grounded = true;
		if (other.gameObject.layer == 4)
			ResetHole();
		if (GameObject.ReferenceEquals(other.gameObject, _targetHole))
		{
			_canvas.gameObject.SetActive(false);
			_holeDone = true;
			_between.gameObject.SetActive(true);
		}
	}

	private void OnCollisionExit(Collision other)
	{
		_forceY = 0.0f;
		_forceZ = 0.0f;
		Debug.Log("other.gameObject.name: " + other.gameObject.name);
	}
}
