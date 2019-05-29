using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	public float speed = 5.0f;

	CharacterController player;

	private float _moveFB;
	private float _moveLR;


	// Use this for initialization
	void Start()
	{
		player = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		_moveFB = Input.GetAxis("Vertical") * speed;
		_moveLR = Input.GetAxis("Horizontal") * speed;

		Vector3 movement = new Vector3(_moveLR, 0, _moveFB);
		movement = transform.rotation * movement;
		player.Move(movement * Time.deltaTime);
	}
}
