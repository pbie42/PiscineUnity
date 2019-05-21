using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorLayerChange : MonoBehaviour
{

	public GameObject[] Objects;
	private SpriteRenderer _spriteRenderer;

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
		ChangeColors(other.gameObject);
		ChangeColorsOfObjects(other.gameObject.name);
	}

	private void ChangeColors(GameObject obj)
	{
		_spriteRenderer.color = GetColor(obj.name);
		ChangeColorsOfObjects(obj.name);
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

	private void ChangeColorsOfObjects(string name)
	{
		for (int i = 0; i < Objects.Length; i++)
		{
			SpriteRenderer objSprite = Objects[i].GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
			if (objSprite)
			{
				objSprite.color = GetColor(name);
				Objects[i].layer = GetLayer(name);
			}
		}
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
