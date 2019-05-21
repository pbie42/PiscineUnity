using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	private bool _gameStarted = false;
	private float _spawnTimer = 0.6f;
	private int _distanceMaxInUnits = -6;
	private int _lineUnitDistance = 10;
	private string[] keys = { "a(Clone)", "s(Clone)", "d(Clone)" };
	public GameObject[] spawnees;
	private GameObject[] spawned;


	private void Start()
	{
		spawned = new GameObject[3];
		SpawnCube();
	}

	private void SpawnCube()
	{
		bool found = true;
		int chosen = 0;
		while (found)
		{
			chosen = Random.Range(0, spawnees.Length);
			GameObject foundObject = FindSpawnee(chosen);
			if (!foundObject)
				found = false;
		}
		GameObject cube = Instantiate(spawnees[chosen]) as GameObject;
		cube.tag = keys[chosen];
		if (!spawned[chosen])
			spawned[chosen] = cube;
	}

	private GameObject FindSpawnee(int chosen)
	{
		for (int i = 0; i < spawned.Length; i++)
		{
			if (spawned[i] && spawned[i].tag == keys[chosen])
				return spawned[i];
		}
		return null;
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
		GameObject foundObject = FindSpawnee(key);
		if (foundObject && foundObject.transform.position.y > _distanceMaxInUnits)
		{
			Debug.Log("Precision: " + CalcPrecision(foundObject));
			Destroy(foundObject);
		}
	}

	private float CalcPrecision(GameObject foundObject)
	{
		float position = 0;
		float distancePixels = (Screen.height - (Screen.height * 0.2f));
		float yPos = foundObject.transform.position.y;
		if (yPos < 0)
			position = 5 + (yPos * -1);
		else
			position = yPos;
		float absolutePosition = _lineUnitDistance - position < 0 ? (_lineUnitDistance - position) * -1 : _lineUnitDistance - position;
		return ((absolutePosition * 0.1f) * distancePixels);
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
