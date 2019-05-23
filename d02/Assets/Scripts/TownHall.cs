using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{

	private float _spawn = 0.0f;
	private float _spawnTime = 10.0f;
	private float _spawnTimer = 10.0f;
	private int _buildingsCount;
	private int _buildingsAlive;
	private SceneManager _sceneManager;
	public bool friendly;
	public Building[] _townBuildings;
	public Footman footman;
	public Orc orc;

	// Use this for initialization
	void Start()
	{
		_buildingsCount = _townBuildings.Length;
		_buildingsAlive = _townBuildings.Length;
		_sceneManager = (SceneManager)FindObjectOfType(typeof(SceneManager));
	}

	// Update is called once per frame
	void Update()
	{
		CheckBuildings();
		_spawnTimer -= Time.deltaTime;
		_spawn += Time.deltaTime;
		if (_spawnTimer <= 0)
		{
			if (friendly)
				SpawnFriendly();
			else
				SpawnEnemy();
			_spawnTimer = _spawnTime;
			_spawn = 0.0f;
		}
	}

	void CheckBuildings()
	{
		int buildCount = 0;
		foreach (Building building in _townBuildings)
		{
			if (!building)
				buildCount++;
		}
		if (_townBuildings.Length - buildCount < _buildingsAlive)
		{
			_buildingsAlive = _townBuildings.Length - buildCount;
		}
		if (_buildingsCount > _buildingsAlive)
		{
			_buildingsCount--;
			_spawnTime += 2.5f;
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
