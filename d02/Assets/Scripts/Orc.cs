using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
	private int _hp = 100;
	private int _totalHp = 100;
	private AudioSource _audioSources;
	private Vector3 _speed = new Vector3(0.02f, 0, 0);
	private bool _right = false;
	private float _moveTimer = 5f;
	private float _moveTime = 5f;
	private float _waitTimer = 5f;
	private float _waitTime = 5f;

	public AudioClip deathSound;


	// Use this for initialization
	void Start()
	{
		_audioSources = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		Move();
	}

	private void Move()
	{
		_waitTimer -= Time.deltaTime;
		if (_waitTimer <= 0)
		{
			if (_moveTimer >= 0)
			{
				if (_right)
					transform.position += _speed;
				else
					transform.position -= _speed;

				if (transform.position.x >= 7 || transform.position.x <= -7)
					_right = !_right;
				_moveTimer -= Time.deltaTime;
			}
			else
			{
				_waitTimer = _waitTime;
				_moveTimer = _moveTime;
				_right = !_right;
			}
		}
	}

	public void TakeDamage(int damage)
	{
		_hp -= damage;
		Debug.Log("Orc unit [" + _hp + "/" + _totalHp + "] has been attacked");
		if (_hp <= 0)
		{
			_audioSources.PlayOneShot(deathSound);
			gameObject.SetActive(false);
			Destroy(gameObject, 3f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
	}
}
