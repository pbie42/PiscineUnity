using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	public GameObject[] spawnees;
	private string[] keys = { "a(Clone)", "s(Clone)", "d(Clone)" };
	private bool _gameStarted = false;
	private float _spawnTimer = 0.6f;

	private void Start()
	{
		SpawnCube();
	}

	private void SpawnCube()
	{
		bool found = true;
		int chosen = 0;
		while (found)
		{
			Debug.Log("We in here");
			chosen = Random.Range(0, spawnees.Length);
			GameObject foundObject = GameObject.Find(keys[chosen]);
			if (foundObject)
				Debug.Log("name: " + foundObject.name);
			if (!foundObject)
				found = false;
		}
		GameObject cube = Instantiate(spawnees[chosen]) as GameObject;
		Debug.Log("Cube Name " + cube.name);
	}

	// Update is called once per frame
	void Update()
	{
		_spawnTimer -= Time.deltaTime;

		if (_spawnTimer <= 0 && NeedCube())
		{
			_spawnTimer = 0.6f;
			SpawnCube();
		}
		HandleKeyPress();
	}

	private void HandleKeyPress()
	{
		if (Input.GetKeyDown(KeyCode.A))
			DestroyCube(0);
		if (Input.GetKeyDown(KeyCode.S))
			DestroyCube(1);
		if (Input.GetKeyDown(KeyCode.D))
			DestroyCube(2);
	}

	private void DestroyCube(int key)
	{
		GameObject foundObject = GameObject.Find(keys[key]);
		if (foundObject && foundObject.transform.position.y > -5)
			Destroy(foundObject);
	}

	private bool NeedCube()
	{
		GameObject a = GameObject.Find(keys[0]);
		GameObject s = GameObject.Find(keys[1]);
		GameObject d = GameObject.Find(keys[2]);
		if (!a || !s || !d)
			return true;
		return false;
	}
}
