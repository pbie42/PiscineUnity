using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : MonoBehaviour
{
	private AudioSource _audioSources;
	private bool _selected = false;
	private Vector3 _movement;
	private float _speed = 1.2f;
	private bool _move = false;
	private Vector3 _destination = Vector3.zero;
	private SpriteRenderer _sprite;
	private string _currentDir = "South";

	public AudioClip[] selectedSounds;
	public AudioClip[] orderedSounds;
	public Animator animator;

	// Use this for initialization
	void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_audioSources = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_selected)
			_sprite.color = hexColor(135.0f, 255.0f, 0f, 255f);
		else
			_sprite.color = hexColor(255.0f, 255.0f, 255.0f, 255f);
		if (_move)
		{
			Move();
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public Vector4 hexColor(float r, float g, float b, float a)
	{
		Vector4 color = new Vector4(r / 255, g / 255, b / 255, a / 255);
		return color;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Trigger");
	}

	public void Deselect()
	{
		_selected = false;
	}

	public void Select()
	{
		SelectSound();
		_selected = true;
	}

	public bool IsSelected()
	{
		return _selected;
	}

	public void MoveOrdered(Vector3 destination)
	{
		OrderedSound();
		_destination = new Vector3(destination.x, destination.y, transform.position.z);
		_movement = _destination - transform.position;
		_movement = _movement.normalized;
		_move = true;
		animator.SetBool(_currentDir, false);
	}

	private void CalculateAnimation()
	{
		float degree = Vector3.Angle(Vector3.up, _destination);
		if (_destination.x < transform.position.x)
			degree = 180 + 180 - degree;
		Debug.Log("degree " + degree);
		_sprite.flipX = false;
		if (degree >= 337.5 && degree <= 22.5)
		{
			Debug.Log("North");
			_currentDir = "North";
			_sprite.flipX = false;
		}
		else if (degree >= 22.5 && degree <= 67.5)
		{
			_currentDir = "NorthEast";
			_sprite.flipX = false;
		}
		else if (degree >= 67.5 && degree <= 112.5)
		{
			_currentDir = "East";
			_sprite.flipX = false;
		}
		else if (degree >= 112.5 && degree <= 157.5)
		{
			_currentDir = "SouthEast";
			_sprite.flipX = false;
		}
		else if (degree >= 157.5 && degree <= 202.5)
		{
			_currentDir = "South";
			_sprite.flipX = false;
		}
		else if (degree >= 202.5 && degree <= 247.5)
		{
			_currentDir = "SouthEast";
			_sprite.flipX = true;
		}
		else if (degree >= 247.5 && degree <= 292.5)
		{
			_currentDir = "East";
			_sprite.flipX = true;
		}
		else if (degree >= 292.5 && degree <= 337.5)
		{
			_currentDir = "NorthEast";
			_sprite.flipX = true;
		}
		animator.SetBool(_currentDir, true);
	}

	public void GetAngle()
	{

	}

	public void Move()
	{
		CalculateAnimation();
		transform.Translate(_speed * _movement * Time.deltaTime);
		float destX = _destination.x;
		float destY = _destination.y;
		float posX = transform.position.x;
		float posY = transform.position.y;
		if (Vector3.Distance(_destination, transform.position) < 0.1f)
		{
			_move = false;
			animator.SetBool(_currentDir, false);
		}
	}

	private void SelectSound()
	{
		Debug.Log("SelectSound");
		int selectedSound = Random.Range(0, selectedSounds.Length - 1);
		_audioSources.PlayOneShot(selectedSounds[selectedSound]);
	}

	private void OrderedSound()
	{
		Debug.Log("OrderedSound");
		int selectedSound = Random.Range(0, orderedSounds.Length - 1);
		_audioSources.PlayOneShot(orderedSounds[selectedSound]);
	}

}
