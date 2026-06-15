using UnityEngine;
using UnityEngine.AI;

public class AnimalFleeState : BehaviourState
{

	private NavMeshAgent _agent;
	private GameObject _player;

	public override void EnterState(NavMeshAgent agent)
	{
		_agent = agent;
		if (_player != null) _agent.SetDestination(GetFleePosition(_player));
	}


	// Update is called once per frame
	void Update()
	{

	}

	Vector3 GetFleePosition(GameObject player) // how the hell am i gonna get the player
	{
		// Calculate a posiion far away in the opposite direction of the player
		// How the hell do i calculate direction

		Vector3 currentPosition = transform.position;

		Vector3 newPosition = new
		(


		);

		newPosition += currentPosition;

		return newPosition;
	}
}
