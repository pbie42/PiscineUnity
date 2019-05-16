using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Space Bar Pressed");
			gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
			Debug.Log(gameObject.transform.localScale);
			if (gameObject.transform.localScale.x >= 4.0)
			{
				Destroy(gameObject);
			}
		}
	}
}
