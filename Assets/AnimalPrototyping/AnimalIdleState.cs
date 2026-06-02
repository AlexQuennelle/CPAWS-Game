using UnityEngine;
public class AnimalIdleState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Idle State Entered");
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		animal.SwitchState(animal.WanderState);

		// TO-DO: Have the animal wait before wandering to a new location. Probably a random amount of time?
	}
}
