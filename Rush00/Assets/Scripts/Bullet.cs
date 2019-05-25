using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 60f;
	public Rigidbody2D rb;
	public float time = 0f;

	float timer = 0f;

	void Update()
	{
		if (time != 0f)
		{
			timer += Time.deltaTime;
			if (timer >= time)
				Destroy(gameObject);
		}
		transform.Translate(Vector3.down * 10 * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("other.name: " + other.name);
		if (other.gameObject.tag == "Wall")
			Destroy(gameObject);
	}
}
