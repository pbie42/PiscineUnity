using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private List<Footman> _footmen;
	private List<Footman> _selectedFootmen;
	public TownHall friendlyTH;
	public TownHall enemyTH;

	// Use this for initialization
	void Start()
	{
		_footmen = new List<Footman>();
		_selectedFootmen = new List<Footman>();
		Footman firstFootman = (Footman)FindObjectOfType(typeof(Footman));
		Orc firstOrc = (Orc)FindObjectOfType(typeof(Orc));
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
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if (hit.collider != null)
				{
					SelectFootman(hit);
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
				if (hit.collider != null)
				{
					int layer = hit.collider.gameObject.layer;
					if (layer == 9)
					{
						AttackCommand(hit.collider.gameObject);
					}
					else
					{
						ClearFootmen();
						SelectFootman(hit);
					}
				}
				else
				{
					foreach (Footman footman in _selectedFootmen)
						footman.RemoveEnemy();
					MoveFootman(mousePos);
				}
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
		Footman _selectedFootman = hit.collider.GetComponent<Footman>();
		_selectedFootman.Select();
		_selectedFootmen.Add(_selectedFootman);
	}

	private void AttackCommand(GameObject enemy)
	{
		foreach (Footman footman in _selectedFootmen)
			if (footman.IsSelected())
			{
				footman.OrderedSound();
				footman.AttackOrdered(enemy);
			}
	}

	private void MoveFootman(Vector3 mousePos)
	{
		foreach (Footman footman in _selectedFootmen)
			if (footman.IsSelected())
			{
				footman.OrderedSound();
				footman.MoveOrdered(mousePos);
			}
	}

	private void CheckWinner()
	{
		if (!friendlyTH)
			Debug.Log("The Orc team wins!");
		else if (!enemyTH)
			Debug.Log("The Human team wins!");
	}
}
