using UnityEngine;
using UnityEngine.AI;

public class PlayerTouchMove : MonoBehaviour
{
	[SerializeField, Tooltip("Player's navmesh agent to control movement")]
	private NavMeshAgent _agent;

	[SerializeField, Tooltip("Transform that will be rotated according to the"
			+ " player's movement direction")]
	private Transform _heading;

	private void OnEnable()
	{
		_agent.updateRotation = false;
	}
	private void Update()
	{
		Vector3 velocity = _agent.velocity;
		if (velocity.magnitude > 0)
		{
			_heading.forward = velocity.normalized;
		}
	}

	public void MovePlayer(Vector3 target)
	{
		_agent.isStopped = false;
		_agent.SetDestination(target);
	}
	public void StopPlayer()
	{
		_agent.isStopped = true;
	}
}