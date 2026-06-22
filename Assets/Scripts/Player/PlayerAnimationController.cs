using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private Rigidbody _rb;

	[SerializeField]
	private PlayerJoystickMove _playerMove;

	private float _sqrMaxSpeed;

	private void Start()
	{
		_sqrMaxSpeed = _playerMove.MoveSpeed * _playerMove.MoveSpeed;
	}

	private void Update()
	{
		_animator.SetFloat("Velocity", _rb.linearVelocity.sqrMagnitude / _sqrMaxSpeed);
	}
}
