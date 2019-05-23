using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	public int buildingHP;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damage)
	{
		buildingHP -= damage;
		if (buildingHP <= 0)
			Destroy(gameObject);
	}
}
