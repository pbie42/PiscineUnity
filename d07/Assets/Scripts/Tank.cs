using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
	private Rigidbody _rgbd;
	public float force;
	public WheelCollider f_driver_col, f_pass_col;
	public WheelCollider b_driver_col, b_pass_col;
	public Transform f_driver, f_pass;
	public Transform b_driver, b_pass;

	public float _steerAngle = 25.0f;
	public float _motorForce = 1500f;
	public float steerAngl;

	float h, v;


	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// if (Input.GetKey(KeyCode.W))
		// 	_rgbd.AddForce(transform.forward * force);
		// if (Input.GetKey(KeyCode.A))
		// 	RotateTank(-50f);
		// if (Input.GetKey(KeyCode.D))
		// 	RotateTank(50f);
		Inputs();
		Drive();
		SteerCar();
		UpdateWheelPos(f_driver_col, f_driver);
		UpdateWheelPos(b_driver_col, b_driver);
		UpdateWheelPos(f_pass_col, f_pass);
		UpdateWheelPos(b_pass_col, b_pass);
	}

	private void RotateTank(float dir)
	{
		transform.Rotate(0, dir * Time.deltaTime, 0);
		Debug.Log("transform.rotation: " + transform.rotation);
	}

	void Inputs()
	{
		// h = Input.GetAxis("Horizontal");
		// v = Input.GetAxis("Vertical");
	}

	void Drive()
	{
		b_driver_col.motorTorque = v * _motorForce;
		b_pass_col.motorTorque = v * _motorForce;
	}

	void SteerCar()
	{
		steerAngl = _steerAngle * h;
		f_driver_col.steerAngle = steerAngl;
		f_pass_col.steerAngle = steerAngl;
	}

	void UpdateWheelPos(WheelCollider col, Transform t)
	{
		Vector3 pos = t.position;
		Quaternion rot = t.rotation;

		col.GetWorldPose(out pos, out rot);
		t.position = pos;
		t.rotation = rot;
	}
}
