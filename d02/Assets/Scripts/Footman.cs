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

	public AudioClip[] selectedSounds;
	public AudioClip[] orderedSounds;

	// Use this for initialization
	void Start()
	{
		_audioSources = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_move)
		{
			Move();
		}
	}

	// private void OnMouseDown()
	// {
	// 	_audioSources[0].Play();
	// 	_selected = true;
	// }

	// private void OnMouseUp()
	// {
	// }

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
		_destination = destination;
		_movement = _destination - transform.position;
		_movement = _movement.normalized;
		_move = true;
	}

	public void Move()
	{
		transform.Translate(_speed * _movement * Time.deltaTime);
		float destX = _destination.x;
		float destY = _destination.y;
		float posX = transform.position.x;
		float posY = transform.position.y;
		if (posX == destX && posY == destY)
			_move = false;
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
