using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : MonoBehaviour
{
	private AudioSource _audioSources;
	private bool _selected = false;

	public AudioClip[] _selectedSounds;

	// Use this for initialization
	void Start()
	{
		_audioSources = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

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
		int selectedSound = Random.Range(0, _selectedSounds.Length - 1);
		_audioSources.PlayOneShot(_selectedSounds[selectedSound]);
		_selected = true;
	}

	public bool IsSelected()
	{
		return _selected;
	}

	public void Move()
	{

	}


}
