using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHand : MonoBehaviour {

	public AudioClip pickingUpWeaponSound = null;
	public GameObject weaponInHand = null;
	public float throwingSpeed = 10f;
	public float weaponAngularVelocity = 180f;
	public Text weaponName;
	public Text ammunition;

	SpriteRenderer currentSpriteRenderer;

	void Start()
	{
		ClearGUI();
		currentSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (weaponInHand)
		{
			if (weaponInHand.GetComponent<Weapon>().infinite)
				ammunition.text = "INFINITY";
			else
				ammunition.text = weaponInHand.GetComponent<Weapon>().ammo.ToString();
			if (Input.GetButtonDown("Fire2"))
				LaunchGun();
		}
	}

	public void ChangeHandSprite()
	{
		currentSpriteRenderer.sprite = weaponInHand.GetComponent<Weapon>().inHandSprite;
		weaponInHand.GetComponent<Weapon>().SetFirePoint(transform);
	}

	private void LaunchGun()
	{
		ClearGUI();
		var weaponRigidbody2D = weaponInHand.GetComponent<Rigidbody2D>();
		
		weaponInHand.GetComponent<Weapon>().SetFirePoint(null);
		currentSpriteRenderer.sprite = null;
		
		weaponInHand.transform.SetParent(null);
		weaponInHand.GetComponent<Renderer>().enabled = true;
		
		weaponRigidbody2D.isKinematic = false;
		weaponRigidbody2D.velocity = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)) * throwingSpeed;
		weaponRigidbody2D.angularVelocity = weaponAngularVelocity;

		weaponInHand = null;
	}

	public void ClearGUI()
	{
		weaponName.text = "NO WEAPON";
		ammunition.text = "-";
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Weapon")
		{
			if (weaponInHand == null)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					weaponInHand = collider.gameObject;
					weaponName.text = weaponInHand.GetComponent<Weapon>().name;
					weaponInHand.GetComponent<AudioSource>().PlayOneShot(pickingUpWeaponSound, 1f);
					ChangeHandSprite();
					weaponInHand.GetComponent<Renderer>().enabled = false;
					weaponInHand.GetComponent<Rigidbody2D>().isKinematic = true;
					weaponInHand.transform.SetParent(gameObject.transform);
				}
			}	
		}
	}
}
