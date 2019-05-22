using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChange : MonoBehaviour
{
	private SpriteRenderer _spriteRenderer;
	private bool _changed = false;

	// Use this for initialization
	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!_changed)
		{
			ChangeColors(other.gameObject);
			_changed = true;
		}
	}

	private void ChangeColors(GameObject obj)
	{
		_spriteRenderer.color = GetColor(obj.name);
	}

	private Vector4 GetColor(string name)
	{
		if (name == "Claire")
			return hexColor(37, 61, 95, 255);
		if (name == "John")
			return hexColor(180, 156, 56, 255);
		if (name == "Thomas")
			return hexColor(214, 69, 66, 255);
		return hexColor(0, 0, 0, 255);
	}

	private int GetLayer(string name)
	{
		if (name == "Claire")
			return 12;
		if (name == "John")
			return 13;
		if (name == "Thomas")
			return 11;
		return 0;
	}

	public Vector4 hexColor(float r, float g, float b, float a)
	{
		Vector4 color = new Vector4(r / 255, g / 255, b / 255, a / 255);
		return color;
	}
}
