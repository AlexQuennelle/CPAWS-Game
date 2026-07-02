using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField, Tooltip("Player's navmesh agent to control movement")]
	private NavMeshAgent _agent;

	[SerializeField, Tooltip("Transform that will be rotated according to the"
			+ " player's movement direction")]
	private Transform _heading;
	private Vector3? _moveDirection = null;

	private void OnEnable()
	{
		_agent.updateRotation = false;
	}
	private void Update()
	{
		if (_moveDirection != null)
		{
			// Direction gets multiplied by 5 for smooth acceleration.
			// This makes the rotations for the player model smoother.
			_agent.SetDestination(
					((Vector3)_moveDirection * 5.0f) + transform.position);
		}

		Vector3 velocity = _agent.velocity;
		if (velocity.magnitude > 0)
		{
			Debug.Log(velocity);
			_heading.forward = velocity.normalized;
		}
	}

	/// <summary>
	///   Tells the player agent to start moving in a direction.
	/// </summary>
	/// <param name="direction">
	///   Direction the player agent should move in.
	/// </param>
	public void SetDirection(Vector3 direction)
	{
		_agent.isStopped = false;
		_moveDirection = direction;
	}
	/// <summary>
	///   Tells the player agent to start moving to a target destination.
	/// </summary>
	public void MoveTo(Vector3 target)
	{
		_agent.isStopped = false;
		_agent.SetDestination(target);
	}
	/// <summary>
	///   Stops all movement on the player agent and clears any supplied
	///   movement direction.
	/// </summary>
	public void StopPlayer()
	{
		_agent.isStopped = true;
		_moveDirection = null;
	}
}