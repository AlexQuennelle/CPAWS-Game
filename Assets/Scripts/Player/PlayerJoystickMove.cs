using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJoystickMove : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed;

	[SerializeField, Tooltip("Transform that will be rotated according to the player's movement direction")]
	private Transform _heading;

	[SerializeField, Tooltip("Third person camera to align the movement axes with")]
	private Transform _cameraTransform;

	[SerializeField]
	private Rigidbody _rb;

	private Vector2 _lastMoveInput;

	private Quaternion _cameraRotation { get { return Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0); } }

	private void OnEnable()
	{
		_rb.linearVelocity = Vector3.zero;
	}

	public void HandleMove(Vector2 move)
	{
		_lastMoveInput = move;
		_heading.rotation = Quaternion.LookRotation(_cameraRotation * new Vector3(_lastMoveInput.x, 0f, _lastMoveInput.y));
	}

	public void StopMove()
	{
		_lastMoveInput = Vector2.zero;
	}

	private void FixedUpdate()
	{
		Vector3 moveDirection = _cameraRotation * new Vector3(_lastMoveInput.x, 0f, _lastMoveInput.y);

		_rb.linearVelocity = new Vector3(moveDirection.x * _moveSpeed, _rb.linearVelocity.y, moveDirection.z * _moveSpeed);
	}
}