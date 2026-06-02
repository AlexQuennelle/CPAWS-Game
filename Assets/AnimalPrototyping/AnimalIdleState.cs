using UnityEngine;
public class AnimalIdleState : AnimalBaseState
{

	float _waitTime = 3;
	float _currentTime = 0;
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Idle State Entered");
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		_currentTime += Time.deltaTime;

		if (_currentTime >= _waitTime) 
		{
			_currentTime = 0;
			animal.SwitchState(animal.WanderState);
		}				
	}
}
