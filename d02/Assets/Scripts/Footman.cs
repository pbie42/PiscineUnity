using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : MonoBehaviour
{
	private AudioSource _audioSources;
	private bool _attack = false;
	private bool _move = false;
	private bool _selected = false;
	private float _attackTime = 3.0f;
	private float _attackTimer = 3.0f;
	private float _destOffset = 0.3f;
	private float _speed = 1.2f;
	private GameObject _enemy = null;
	private int _attackDamage = 25;
	private int _hp = 100;
	private SceneManager _sceneManager;
	private SpriteRenderer _sprite;
	private string _currentDir = "South";
	private Vector3 _destination = Vector3.zero;
	private Vector3 _movement;

	public Animator animator;
	public AudioClip[] attackSounds;
	public AudioClip[] orderedSounds;
	public AudioClip[] selectedSounds;

	// Use this for initialization
	void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_audioSources = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_selected)
			_sprite.color = hexColor(135.0f, 255.0f, 0f, 255f);
		else
			_sprite.color = hexColor(255.0f, 255.0f, 255.0f, 255f);
		if (_move)
		{
			Move();
		}
		if (_attack)
		{
			if (_enemy && _enemy.activeSelf)
			{
				_attackTimer -= Time.deltaTime;
				if (_attackTimer <= 0)
				{
					_attackTimer = _attackTime;
					DamageEnemy();
				}
				AttackEnemy();
			}
			else
			{
				_attack = false;
				animator.SetBool("Attack", false);
				animator.SetBool(_currentDir, false);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{

		if (_enemy && GameObject.ReferenceEquals(_enemy, other.gameObject))
		{
			AttackEnemy();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (_enemy && GameObject.ReferenceEquals(_enemy, other.gameObject))
		{
			_attack = false;
			AttackOrdered(other.gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (!_attack && _enemy && GameObject.ReferenceEquals(_enemy, other.gameObject))
		{
			AttackEnemy();
		}
	}

	public void AttackOrdered(GameObject enemy)
	{
		if (_attack && _enemy && GameObject.ReferenceEquals(_enemy, enemy))
		{
			AttackEnemy();
		}
		else
		{
			_attack = false;
			_enemy = enemy;
			MoveOrdered(_enemy.transform.position);
		}
	}

	private void AttackEnemy()
	{
		_attack = true;
		_move = false;
		animator.SetBool("Move", false);
		CalculateAnimation();
	}

	private void DamageEnemy()
	{
		if (_enemy.tag == "Building")
		{
			Building enemyBuilding = (Building)_enemy.GetComponent<Building>();
			enemyBuilding.TakeDamage(_attackDamage);
			_audioSources.PlayOneShot(attackSounds[0]);
		}
		if (_enemy.tag == "Orc")
		{
			Orc enemyOrc = (Orc)_enemy.GetComponent<Orc>();
			enemyOrc.TakeDamage(_attackDamage);
			_audioSources.PlayOneShot(attackSounds[1]);
		}
	}

	public void MoveOrdered(Vector3 destination)
	{
		if (!_enemy)
		{
			_destination = new Vector3(destination.x, destination.y, transform.position.z);
			_movement = _destination - transform.position;
		}
		else
		{
			_destination = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, transform.position.z);
			_movement = _destination - transform.position;
		}

		_movement = _movement.normalized;
		_move = true;
		_attack = false;
		animator.SetBool("Attack", false);
		animator.SetBool(_currentDir, false);
	}

	public void Move()
	{
		CalculateAnimation();
		if (_enemy)
		{
			_destination = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, transform.position.z);
			_movement = _destination - transform.position;
			_movement = _movement.normalized;
		}
		transform.Translate(_speed * _movement * Time.deltaTime);
		if (Vector3.Distance(_destination, transform.position) < _destOffset)
		{
			_move = false;
			animator.SetBool(_currentDir, false);
			animator.SetBool("Move", false);
		}
	}

	private float CalculateDegrees()
	{
		float degree = Vector3.Angle(Vector3.up, _destination);
		if (_destination.x < transform.position.x)
			degree = 180 + 180 - degree;
		return degree;
	}

	private void CalculateAnimation()
	{
		float degree = CalculateDegrees();
		_sprite.flipX = false;
		if (degree >= 337.5 && degree <= 22.5)
		{
			_currentDir = "North";
			_sprite.flipX = false;
		}
		else if (degree >= 22.5 && degree <= 67.5)
		{
			_currentDir = "NorthEast";
			_sprite.flipX = false;
		}
		else if (degree >= 67.5 && degree <= 112.5)
		{
			_currentDir = "East";
			_sprite.flipX = false;
		}
		else if (degree >= 112.5 && degree <= 157.5)
		{
			_currentDir = "SouthEast";
			_sprite.flipX = false;
		}
		else if (degree >= 157.5 && degree <= 202.5)
		{
			_currentDir = "South";
			_sprite.flipX = false;
		}
		else if (degree >= 202.5 && degree <= 247.5)
		{
			_currentDir = "SouthEast";
			_sprite.flipX = true;
		}
		else if (degree >= 247.5 && degree <= 292.5)
		{
			_currentDir = "East";
			_sprite.flipX = true;
		}
		else if (degree >= 292.5 && degree <= 337.5)
		{
			_currentDir = "NorthEast";
			_sprite.flipX = true;
		}
		animator.SetBool(_currentDir, true);
		if (_move)
			animator.SetBool("Move", true);
		if (_attack)
			animator.SetBool("Attack", true);
	}

	public void Deselect()
	{
		_selected = false;
	}

	public void Select()
	{
		SelectSound();
		_selected = true;
	}

	public bool IsSelected()
	{
		return _selected;
	}

	private void SelectSound()
	{
		int selectedSound = Random.Range(0, selectedSounds.Length - 1);
		_audioSources.PlayOneShot(selectedSounds[selectedSound]);
	}

	public void OrderedSound()
	{
		int selectedSound = Random.Range(0, orderedSounds.Length - 1);
		_audioSources.PlayOneShot(orderedSounds[selectedSound]);
	}

	public Vector4 hexColor(float r, float g, float b, float a)
	{
		Vector4 color = new Vector4(r / 255, g / 255, b / 255, a / 255);
		return color;
	}

	public void RemoveEnemy()
	{
		_enemy = null;
	}

}
