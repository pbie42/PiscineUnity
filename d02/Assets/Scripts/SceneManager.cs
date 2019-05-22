using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private List<Footman> _footmen;
	private List<Footman> _selectedFootmen;

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
		if (Input.GetMouseButtonDown(0) && _selectedFootmen.Count == 0)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			SelectFootman(mousePos);
			// MoveOrder(mousePos);
		}
		if (Input.GetMouseButtonDown(0) && _selectedFootmen.Count > 0)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			MoveOrder(mousePos);
		}
	}

	private void SelectFootman(Vector3 mousePos)
	{
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (hit.collider != null)
		{
			Debug.Log("hit" + hit.collider.gameObject.tag);
			Footman _selectedFootman = hit.collider.GetComponent<Footman>();
			_selectedFootman.Select();
			_selectedFootmen.Add(_selectedFootman);
			Debug.Log("_selectedFootmen.Count " + _selectedFootmen.Count);
		}
	}

	private void MoveOrder(Vector3 mousePos)
	{
		MoveFootman(mousePos);
	}

	private void MoveFootman(Vector3 mousePos)
	{
	}

	// private void FindSelectedFootman()
	// {
	// 	foreach (Footman footman in _footmen)
	// 		if (footman.IsSelected())
	// 			_selectedFootmen.Add(footman);
	// }
}
