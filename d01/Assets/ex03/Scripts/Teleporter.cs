using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

	public GameObject _end;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Triggered!");
		Debug.Log("_end.transform.position: " + _end.transform.position);
		Debug.Log("gameObject name: " + other.gameObject.name);
		Debug.Log("other.gameObject.transform.position: " + other.gameObject.transform.position);
		other.gameObject.transform.position = _end.transform.position;
		Debug.Log("other.gameObject.transform.position: " + other.gameObject.transform.position);
	}
}
