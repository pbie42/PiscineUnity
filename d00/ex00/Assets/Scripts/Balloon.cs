using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

	private bool _gameStarted = false;
	private float _gameOverTimer = 0.0f;
	private float _timer = 0.0f;
	private Vector3 _localScale;
	private Vector3 _gameOverScale = new Vector3(0f, 0f, 0f);
	private Vector3 _explodeScale = new Vector3(4.0f, 4.0f, 0f);
	[SerializeField] float _descale = 0.0f;

	// Use this for initialization
	void Start()
	{
		_localScale = gameObject.transform.localScale;
		gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0);
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 currentScale = gameObject.transform.localScale;

		if (_gameStarted)
		{
			_timer += Time.deltaTime;
			_gameOverTimer += Time.deltaTime;
			gameObject.transform.localScale -= new Vector3(_descale, _descale, 0);
		}
		Debug.Log("currentScale " + currentScale.ToString("F4"));
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_gameStarted = true;
			_gameOverTimer = 0.0f;
			gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
			if (gameObject.transform.localScale.x >= 4.0)
				Destroy(gameObject);
		}
		if (_gameOverTimer >= 5f || currentScale.x <= 0.0f || currentScale.x >= 4.0f)
		{
			Debug.Log("Game Over");
			Destroy(gameObject);
		}
	}
}
