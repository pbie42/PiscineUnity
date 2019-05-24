using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragHandler : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IEndDragHandler
{

	private Vector3 _startPosition;

	// Use this for initialization
	void Start()
	{
		_startPosition = transform.localPosition;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		transform.localPosition = _startPosition;
	}
}
