using UnityEngine;
using UnityEngine.AI;

public class PlayerTouchMove : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent _agent;

	private void OnEnable()
	{
		_agent.updateRotation = false;
	}

	public void MovePlayer(Vector3 target)
	{
		_agent.SetDestination(target);
	}
}