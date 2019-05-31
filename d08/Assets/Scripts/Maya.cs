using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maya : MonoBehaviour
{
	private Animator _animator;
	private UnityEngine.AI.NavMeshAgent _navMeshAgent;
	private bool _running;

	// Use this for initialization
	void Start()
	{
		_animator = GetComponent<Animator>();
		_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out hit, 100))
			{
				_navMeshAgent.destination = hit.point;
			}
		}

		if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
		{
			_running = false;
		}
		else
		{
			_running = true;
		}

		_animator.SetBool("Running", _running);
	}
}
