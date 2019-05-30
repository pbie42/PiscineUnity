using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour
{
	private Rigidbody _rgbd;
	private GameObject _tank;

	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
		_tank = this.transform.parent.gameObject;
		Debug.Log("_tank: " + _tank);
	}

	// Update is called once per frame
	void Update()
	{
		// if (Input.GetKey(KeyCode.A))
		// 	RotateTank(-50f);
		// if (Input.GetKey(KeyCode.D))
		// 	RotateTank(50f);
	}

	private void RotateTank(float dir)
	{
		transform.Rotate(0, 0, dir * Time.deltaTime);
	}
}
