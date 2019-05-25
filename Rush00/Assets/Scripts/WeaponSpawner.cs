using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

	[SerializeField] Weapon[] weapons;

	void Start()
	{
		Instantiate(weapons[Random.Range(0, weapons.Length)], transform.position, transform.rotation);
	}
}
