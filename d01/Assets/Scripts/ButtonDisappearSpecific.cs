using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisappearSpecific : MonoBehaviour
{

	public GameObject[] Objects;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		ObjectsDisappear(other.gameObject.name);
	}

	private void ObjectsDisappear(string name)
	{
		for (int i = 0; i < Objects.Length; i++)
		{
			if (Objects[i].tag == name)
				Objects[i].SetActive(false);
		}
	}
}
