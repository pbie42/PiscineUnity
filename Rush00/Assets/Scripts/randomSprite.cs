using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour {

	[SerializeField] Sprite[] sprites = null;

	void Awake()
	{
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}
}
