using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maya : MonoBehaviour
{
	private Animator _animator;
	private UnityEngine.AI.NavMeshAgent _navMeshAgent;
	private bool _running;
	private int _health = 100;
	private bool _dead = false;

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
		if (!_dead)
		{
			if (Input.GetMouseButton(0))
			{
				if (Physics.Raycast(ray, out hit, 100))
				{
					Debug.Log("hit: " + hit);
					_navMeshAgent.destination = hit.point;
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				_dead = true;
				_animator.SetBool("Running", _running);
				_animator.SetBool("Dead", _dead);
			}

			if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
			{
				_running = false;
			}
			else
			{
				_running = true;
			}
		}

		if (!_dead)
			_animator.SetBool("Running", _running);
	}
}
