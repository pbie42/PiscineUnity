using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{

	private float _spawnTime = 10.0f;
	private float _spawnTimer = 10.0f;
	public Footman footman;
	public Orc orc;
	public bool friendly;

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
			if (friendly)
				SpawnFriendly();
			else
				SpawnEnemy();
			_spawnTimer = _spawnTime;
		}
	}

	void SpawnFriendly()
	{
		Footman newFootman = Instantiate(footman);
		newFootman.transform.position = new Vector3(-4.8f, 2.5f, -1.0f);
	}

	void SpawnEnemy()
	{
		Orc newOrc = Instantiate(orc);
		newOrc.transform.position = new Vector3(5.1f, -1.0f, -1.0f);
	}
}
