using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maya : MonoBehaviour
{
	private Animator _animator;
	private UnityEngine.AI.NavMeshAgent _navMeshAgent;
	private bool _running;
	private bool _attack = false;
	private int _health = 100;
	private bool _dead = false;
	Vector3 pos;

	public float speed;
	public CharacterController controller;

	// Use this for initialization
	void Start()
	{
		_animator = GetComponent<Animator>();
		_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		_navMeshAgent.destination = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButton(0))
			LocatePosition();
		MoveToPosition();
	}

	private void LocatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.Log("hit: " + hit.point);
			pos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			_navMeshAgent.destination = hit.point;
			Debug.Log("transform.position: " + transform.position);
			Debug.Log("_navMeshAgent.destination: " + _navMeshAgent.destination);
			Debug.Log("_navMeshAgent.remainingDistance: " + _navMeshAgent.remainingDistance);
			// _attack = false;
			_running = true;
		}
	}

	private void MoveToPosition()
	{
		Quaternion newRotation = Quaternion.LookRotation(pos - transform.position, Vector3.forward);
		newRotation.x = 0f;
		newRotation.z = 0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
		if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
		{
			Debug.Log("Stopping");
			_running = false;
			_navMeshAgent.velocity = Vector3.zero;
			_navMeshAgent.isStopped = true;
		}
		else
		{
			_navMeshAgent.velocity = Vector3.zero;
			_running = true;
			Debug.Log("_navMeshAgent.remainingDistance: " + _navMeshAgent.remainingDistance);
		}
		_animator.SetBool("Running", _running);
		// _animator.SetBool("Attack", _attack);
		// _animator.SetBool("Dead", _dead);
	}

}
