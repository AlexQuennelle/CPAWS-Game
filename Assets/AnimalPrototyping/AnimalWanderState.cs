using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : AnimalBaseState
{
	[SerializeField] private float _wanderRange;
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Wander State Entered");

		Vector3 wanderPosition = GetWanderPosition(animal);

		animal.Agent.destination = wanderPosition;
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		// Return to Idle state after reaching the wander destination
		if (animal.Agent.remainingDistance <= animal.Agent.stoppingDistance)
		{
			animal.SwitchState(animal.IdleState);
		}
	}
	private Vector3 GetWanderPosition(AnimalStateMachine animal)
	{
		// Calculate a random position based on the animal's current location
		Vector3 currentPosition = animal.Animal.transform.position;

		Vector3 newPosition = new Vector3(
			Random.Range(-_wanderRange, _wanderRange + 1),
			0,
			Random.Range(-_wanderRange, _wanderRange + 1)
		);

		newPosition += currentPosition;

		// Note: If the new position is outside the navmesh, the agent appears to handle it automatically without issue :D
		// This differs from unreal in which the ai would shit itself.

		return newPosition;
	}
}
