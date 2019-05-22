using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{

	private float _spawnTime = 10.0f;
	private float _spawnTimer = 10.0f;
	public Footman footman;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		_spawnTimer -= Time.deltaTime;
		if (_spawnTimer <= 0)
		{
			Footman newFootman = Instantiate(footman);
			newFootman.transform.position = new Vector3(-4.8f, 2.5f, -1.0f);
			_spawnTimer = _spawnTime;
		}
	}
}
