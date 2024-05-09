using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	private Rigidbody2D _rb;
	private HealthManager _healthManager;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float jumpPower;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_healthManager = GetComponent<HealthManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
			_rb.simulated = true;
		Movement();
	}

	private void Movement ()
	{
		float _horizontal = Input.GetAxis("Horizontal");

		_rb.velocity = new Vector2(_horizontal * moveSpeed, _rb.velocity.y);

		if (Input.GetKeyDown(KeyCode.Space))
			_rb.velocity = new Vector2(_horizontal, jumpPower);
	}
}
