using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private Transform _orientation;

	[SerializeField]
	private float _playerHeight;

	private Vector2 _lastMoveInput;

	[SerializeField]
	private Rigidbody _rb;

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