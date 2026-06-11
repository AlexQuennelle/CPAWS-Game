using UnityEngine;
using UnityEngine.AI;

public class AnimalFleeState : BehaviourState
{

	private NavMeshAgent _agent;


	public override void EnterState(NavMeshAgent agent)
	{
		_agent = agent;
		_agent.SetDestination(GetFleePosition());
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
