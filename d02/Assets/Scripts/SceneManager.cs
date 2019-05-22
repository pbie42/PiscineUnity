﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private List<Footman> _footmen;
	private List<Footman> _selectedFootmen;
	private bool _multipleSelected = false;

	// Use this for initialization
	void Start()
	{
		_footmen = new List<Footman>();
		_selectedFootmen = new List<Footman>();
		Footman firstFootman = (Footman)FindObjectOfType(typeof(Footman));
		_footmen.Add(firstFootman);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("mouse button down with ctrl");
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if (hit.collider != null)
				{
					_multipleSelected = true;
					SelectFootman(hit);
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("mouse button down");
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if (hit.collider != null)
				{
					ClearFootmen();
					SelectFootman(hit);
				}
				else
					MoveOrder(mousePos);
			}
		}
	}

	private void ClearFootmen()
	{
		foreach (Footman footman in _selectedFootmen)
			footman.Deselect();
		_selectedFootmen.Clear();
	}

	private void SelectFootman(RaycastHit2D hit)
	{
		Debug.Log("hit" + hit.collider.gameObject.tag);
		Footman _selectedFootman = hit.collider.GetComponent<Footman>();
		_selectedFootman.Select();
		_selectedFootmen.Add(_selectedFootman);
		Debug.Log("_selectedFootmen.Count " + _selectedFootmen.Count);
	}

	private void MoveOrder(Vector3 mousePos)
	{
		MoveFootman(mousePos);
	}

	private void MoveFootman(Vector3 mousePos)
	{
		foreach (Footman footman in _selectedFootmen)
			if (footman.IsSelected())
				footman.MoveOrdered(mousePos);
	}
}
