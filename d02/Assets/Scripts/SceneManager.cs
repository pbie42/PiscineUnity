﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private List<Footman> _footmen;
	private List<Footman> _selectedFootmen;
	private List<Building> _friendlyBuildings;
	private List<Building> _enemyBuildings;
	private List<Orc> _orcs;
	private bool _multipleSelected = false;

	// Use this for initialization
	void Start()
	{
		_footmen = new List<Footman>();
		_selectedFootmen = new List<Footman>();
		_friendlyBuildings = new List<Building>();
		_enemyBuildings = new List<Building>();
		_orcs = new List<Orc>();
		Footman firstFootman = (Footman)FindObjectOfType(typeof(Footman));
		_footmen.Add(firstFootman);
	}

	// Update is called once per frame
	void Update()
	{
		CheckWinner();
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
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Debug.Log("mousePos " + mousePos);
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if (hit.collider != null)
				{
					Debug.Log("hit.collider.gameObject.name" + hit.collider.gameObject.name);
					int layer = hit.collider.gameObject.layer;
					if (layer == 9)
					{
						Debug.Log("Enemy: ");
						AttackCommand(hit.collider.gameObject);
					}
					else
					{
						ClearFootmen();
						SelectFootman(hit);
					}
				}
				else
					MoveFootman(mousePos);
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

	private void AttackCommand(GameObject enemy)
	{
		foreach (Footman footman in _selectedFootmen)
			if (footman.IsSelected())
				footman.AttackOrdered(enemy);
	}

	private void MoveFootman(Vector3 mousePos)
	{
		foreach (Footman footman in _selectedFootmen)
			if (footman.IsSelected())
				footman.MoveOrdered(mousePos);
	}

	private void CheckWinner()
	{
		if (_friendlyBuildings.Count <= 0 && _footmen.Count <= 0)
			Debug.Log("The Orc team wins!");
		else if (_enemyBuildings.Count >= 0 && _orcs.Count <= 0)
			Debug.Log("The Human team wins!");
	}

	public void AddFriendlyBuilding(Building friendly)
	{
		_friendlyBuildings.Add(friendly);
	}

	public void AddHuman(Footman human)
	{
		_footmen.Add(human);
	}

	public void AddOrc(Orc orc)
	{
		_orcs.Add(orc);
	}

	public void AddEnemyBuilding(Building enemy)
	{
		_enemyBuildings.Add(enemy);
	}
}
