using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[HideInInspector] public AudioSource currentAudioSource;

	public Sprite inHandSprite;
	public string name;
	public GameObject bulletPrefab;
	public float ammo = 20;
	public AudioClip shootingSound = null;
	private Transform firePoint;
	public float fireRate = 0.3f;
	private float _fireTimer = 0;
	public bool infinite = false;


	void Start()
	{
		currentAudioSource = gameObject.GetComponent<AudioSource>();
	}

	void Update()
	{
		_fireTimer += Time.deltaTime;
		if (firePoint && _fireTimer >= fireRate)
		{
			if (ammo > 0 || infinite)
			{
				if (Input.GetMouseButton(0))
					Shoot();
			}
		}
	}

	public void Shoot()
	{
		_fireTimer = 0;
		currentAudioSource.PlayOneShot(shootingSound);
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		ammo--;
	}

	public void SetFirePoint(Transform firePointToSet)
	{
		firePoint = firePointToSet;
	}

}
