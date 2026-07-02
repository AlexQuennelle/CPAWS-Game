using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private Rigidbody _rb;

	[SerializeField]
	private NavMeshAgent _agent;

	private float _sqrMaxSpeed;

	private void Start()
	{
		_sqrMaxSpeed = _agent.speed * _agent.speed;
	}

	private void Update()
	{
		_animator.SetFloat(
				"Velocity", _agent.velocity.sqrMagnitude / _sqrMaxSpeed);
	}
}