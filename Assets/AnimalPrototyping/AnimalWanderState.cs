using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Wander State Entered");

		Vector3 wanderPosition = GetWanderPosition(animal);
		/*Debug.Log(NavMesh.SamplePosition(
			animal.GetAnimalPosition(),
			out hit,
			20,
			NavMesh.AllAreas
		));*/
		animal.GetNavAgent().destination = wanderPosition;
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		if (animal.GetNavAgent().remainingDistance <= animal.GetNavAgent().stoppingDistance)
		{
			animal.SwitchState(animal.IdleState);
		}
	}
	private Vector3 GetWanderPosition(AnimalStateMachine animal)
	{
		Vector3 position = new Vector3(
			Random.Range(-20, 21),
			0,
			Random.Range(-20, 21)
		);

		while (!NavMesh.SamplePosition(position, out NavMeshHit hit, 20, NavMesh.AllAreas))
		{
			position = new Vector3(
				Random.Range(-20, 21),
				0,
				Random.Range(-20, 21)
			);
		}

		return position;
	}
}
