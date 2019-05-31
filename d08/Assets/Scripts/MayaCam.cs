using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaCam : MonoBehaviour
{

	public Transform target;
	public float distance = 5f;
	public float minDistance = 1f;
	public float maxDistance = 7f;
	public Vector3 offset;
	public float smoothSpeed = 5f;
	public float scrollSensitivity = 1f;

	void Start()
	{
	}

	// Update is called once per frame
	void LateUpdate()
	{
		// transform.position = new Vector3(target.position.x + 4, target.position.y + 10, target.position.z + 9);
		if (!target)
		{
			Debug.Log("No Target");
			return;
		}

		float num = Input.GetAxis("Mouse ScrollWheel");
		distance -= num * scrollSensitivity;
		distance = Mathf.Clamp(distance, minDistance, maxDistance);

		Vector3 pos = target.position + offset;
		pos -= transform.forward * distance;

		transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
	}
}
