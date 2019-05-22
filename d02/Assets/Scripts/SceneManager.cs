using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private List<Footman> _footmen;

	// Use this for initialization
	void Start()
	{
		_footmen = new List<Footman>();
	}

	// Update is called once per frame
	void Update()
	{
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	Debug.Log("Input.mousePosition" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
		// }
	}
}
