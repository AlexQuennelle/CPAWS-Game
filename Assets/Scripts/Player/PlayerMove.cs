using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
	private Vector3 _initialPosition;

	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private Transform _orientation;

	[SerializeField]
	private float _playerHeight;
	[SerializeField]
	private LayerMask _groundMask;

	private Vector2 _lastMoveInput;

	[SerializeField]
	private Rigidbody _rb;

	private bool _grounded
	{
		get { return Physics.Raycast(transform.position + Vector3.up, Vector3.down, (_playerHeight / 2f) + 0.3f, _groundMask); }
	}

	private void OnEnable()
	{
		_rb.linearVelocity = Vector3.zero;
	}

	public void HandleMove(Vector2 move)
	{
		_lastMoveInput = move;
	}

	public void StopMove()
	{
		_lastMoveInput = Vector2.zero;
	}

	private void FixedUpdate()
	{
		Vector3 moveDirection = _orientation.localRotation * new Vector3(_lastMoveInput.x, 0f, _lastMoveInput.y);

		_rb.linearVelocity = new Vector3(moveDirection.x * _moveSpeed, _rb.linearVelocity.y, moveDirection.z * _moveSpeed);
	}
}