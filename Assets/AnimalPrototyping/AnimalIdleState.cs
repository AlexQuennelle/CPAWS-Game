using UnityEngine;
public class AnimalIdleState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Idle State Entered");
	}

	public override void UpdateState(AnimalStateMachine animal)
	{

	}
}
