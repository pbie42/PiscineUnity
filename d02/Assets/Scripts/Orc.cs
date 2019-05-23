using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
	private int _hp = 100;
	private int _totalHp = 100;
	private SceneManager _sceneManager;

	// Use this for initialization
	void Start()
	{
		_sceneManager = GameObject.FindObjectOfType<SceneManager>();
		_sceneManager.AddOrc(this);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damage)
	{
		_hp -= damage;
		Debug.Log("Orc unit [" + _hp + "/" + _totalHp + "] has been attacked");
		if (_hp <= 0)
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Collision orc: ");
	}
}
