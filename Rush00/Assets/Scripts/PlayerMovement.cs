using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float speed = 500f;
	
	Vector3 dir;
	Vector3 mousePos;
	float newX;
	float newY;
	float oldAngle = 0;
	float angle = 0;

	Rigidbody2D currentRigidbody2D;
	Animator currentAnimator;

	void Start()
	{
		currentAnimator = gameObject.GetComponent<Animator>();
		currentRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update () {
		RotatePlayer();
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
	}

	void FixedUpdate()
	{
		var movingVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (movingVector.x != 0 || movingVector.y != 0)
			currentAnimator.SetBool("isRunning", true);
		else
			currentAnimator.SetBool("isRunning", false);
		currentRigidbody2D.MovePosition(currentRigidbody2D.position + (movingVector * speed) * Time.fixedDeltaTime);
	}

	void RotatePlayer()
	{
		oldAngle = angle;
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newX = mousePos.x - transform.position.x;
		newY = mousePos.y - transform.position.y;
		dir = new Vector3(newX, newY, 0f);
		angle = Vector3.Angle(Vector3.down, dir);
		if (mousePos.x < transform.position.x)
			angle *= -1;
		transform.Rotate(new Vector3(0, 0, angle - oldAngle));
	}
}
