using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropHandler : MonoBehaviour, UnityEngine.EventSystems.IDropHandler
{

	private gameManager _gameManager;
	public towerScript turret;

	// Use this for initialization
	void Start()
	{
		_gameManager = (gameManager)FindObjectOfType(typeof(gameManager));
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (hit.collider != null)
		{
			towerScript newTurret = Instantiate(turret);
			newTurret.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
			_gameManager.playerEnergy -= turret.energy;
		}
	}

}
