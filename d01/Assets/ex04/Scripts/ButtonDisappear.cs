﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisappear : MonoBehaviour
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
		ObjectsDisappear();
	}

	private void ObjectsDisappear()
	{
		for (int i = 0; i < Objects.Length; i++)
		{
			Objects[i].SetActive(false);
		}
	}
}
