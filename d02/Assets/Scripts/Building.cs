using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	public int buildingHP;
	public bool friendly;
	private int _totalHp;
	private SceneManager _sceneManager;

	// Use this for initialization
	void Start()
	{
		_totalHp = buildingHP;
		_sceneManager = GameObject.FindObjectOfType<SceneManager>();
		if (friendly)
			_sceneManager.AddFriendlyBuilding(this);
		else
			_sceneManager.AddEnemyBuilding(this);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damage)
	{
		buildingHP -= damage;
		Debug.Log("Building [" + buildingHP + "/" + _totalHp + "] has been attacked");
		if (buildingHP <= 0)
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision Building: ");
	}
}
