using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour
{

	public gameManager gManager;
	public towerScript turret;
	public UnityEngine.UI.Text damage;
	public UnityEngine.UI.Text energy;
	public UnityEngine.UI.Text range;
	public UnityEngine.UI.Text wait;
	public UnityEngine.UI.Image image;

	// Use this for initialization
	void Start()
	{
		damage.text = turret.damage.ToString();
		energy.text = turret.energy.ToString();
		range.text = turret.range.ToString();
		wait.text = turret.fireRate.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		if (gManager.playerEnergy < turret.energy)
		{
			image.color = new Color32(255, 54, 54, 255);
			image.GetComponent<ItemDragHandler>().canDrag = false;
		}
		else
		{
			image.color = new Color32(255, 255, 255, 255);
			image.GetComponent<ItemDragHandler>().canDrag = true;
		}
	}
}
