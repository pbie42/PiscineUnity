using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : MonoBehaviour
{
	private AudioSource[] _audioSources;
	private bool _selected = false;

	// Use this for initialization
	void Start()
	{
		_audioSources = GetComponents<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnMouseDown()
	{
		Debug.Log("Mouse down on Footman");
		_audioSources[0].Play();
		_selected = true;
	}

	private void OnMouseUp()
	{
		Debug.Log("Mouse up on Footman");
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


}
