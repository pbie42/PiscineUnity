using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{
	public gameManager gManager;
	public UnityEngine.UI.Text energy;
	public UnityEngine.UI.Text hp;
	public UnityEngine.UI.Image cannon;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		hp.text = gManager.playerHp.ToString();
		energy.text = gManager.playerEnergy.ToString();
	}
}
