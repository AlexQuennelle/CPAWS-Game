using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Wander State Entered");

		Vector3 wanderPosition = new Vector3(
			Random.Range(-20, 21),
			0,
			Random.Range(-20, 21)
		);

		NavMeshHit hit;
		//NavMesh.SamplePosition();


	}

	public override void UpdateState(AnimalStateMachine animal)
	{

	}
}
