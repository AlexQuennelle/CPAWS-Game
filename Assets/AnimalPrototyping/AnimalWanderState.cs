using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Wander State Entered");

		Vector3 wanderPosition = GetWanderPosition(animal);

		animal.GetNavAgent().destination = wanderPosition;
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		// Return to Idle state after reaching the wander destination
		if (animal.GetNavAgent().remainingDistance <= animal.GetNavAgent().stoppingDistance)
		{
			animal.SwitchState(animal.IdleState);
		}
	}
	private Vector3 GetWanderPosition(AnimalStateMachine animal)
	{
		// Calculate a random position based on the animal's current location
		Vector3 currentPosition = animal.GetAnimal().transform.position;

		Vector3 newPosition = new Vector3(
			Random.Range(-10, 11),
			0,
			Random.Range(-10, 11)
		);

		newPosition += currentPosition;

		// Note: If the new position is outside the navmesh, the agent appears to handle it automatically without issue :D
		// This differs from unreal in which the ai would shit itself.

		return newPosition;
	}
}
