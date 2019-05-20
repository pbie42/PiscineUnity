using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	public playerScript_ex00 Player;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Ground Check hit something");
		Player.IsGrounded(true);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Player.IsGrounded(false);
	}
}
