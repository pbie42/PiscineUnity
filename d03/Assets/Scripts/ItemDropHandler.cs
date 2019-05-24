using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropHandler : MonoBehaviour, UnityEngine.EventSystems.IDropHandler
{

	private gameManager _gameManager;

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
		Debug.Log("Input.mousePosition: " + mousePos);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (hit.collider != null)
		{
			Debug.Log("hit.collider.gameObject: " + hit.collider.gameObject);
		}
	}

}
