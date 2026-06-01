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
	private float _slopeMultiplier = 2f;
	[SerializeField]
	private float _slopeSnap = 20f;
	[SerializeField]
	private float _groundDrag;
	[SerializeField]
	private float _maxSlopeAngle;

	[SerializeField]
	private Transform _orientation;

	[SerializeField]
	private float _playerHeight;
	[SerializeField]
	private LayerMask _groundMask;

	private Vector2 _lastMoveInput;
	private RaycastHit _slopeHit;

	[SerializeField]
	private Rigidbody _rb;

	private bool _grounded
	{
		get { return Physics.Raycast(transform.position + Vector3.up, Vector3.down, (_playerHeight / 2f) + 0.3f, _groundMask); }
	}

	private bool _onSlope
	{
		get
		{
			if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out _slopeHit, _playerHeight / 2f + 0.5f))
			{
				float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
				return angle <= _maxSlopeAngle && angle != 0;
			}
			return false;
		}
	}

	private void Awake()
	{
		_initialPosition = transform.position;
	}

	private void OnEnable()
	{
		transform.position = _initialPosition;
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

		if (!_onSlope)
		{
			// flat ground movement
			_rb.AddForce(moveDirection * _moveSpeed);
		}
		else
		{
			// slope movement
			_rb.AddForce(GetSlopeMoveDirection(moveDirection) * _moveSpeed * _slopeMultiplier);

			if (_rb.linearVelocity.y > 0f)
			{
				// lock player to slope
				_rb.AddForce(Vector3.down * _slopeSnap);
			}
		}

		// ground drag
		if (_grounded)
		{
			_rb.linearDamping = _groundDrag;
		}
		else
		{
			_rb.linearDamping = 0f;
		}

		if (_rb.linearVelocity.sqrMagnitude > _moveSpeed * _moveSpeed)
		{
			LimitSpeed();
		}

		_rb.useGravity = !_onSlope;
	}

	private Vector3 GetSlopeMoveDirection(Vector3 curMoveDir)
	{
		return Vector3.ProjectOnPlane(curMoveDir, _slopeHit.normal).normalized;
	}


	private void LimitSpeed()
	{
		if (!_onSlope)
		{
			Vector3 nVec = _rb.linearVelocity.normalized;
			_rb.linearVelocity = new Vector3(nVec.x * _moveSpeed, _rb.linearVelocity.y, nVec.z * _moveSpeed);
		}
		else
		{
			_rb.linearVelocity = _rb.linearVelocity.normalized * _moveSpeed;
		}
	}
}