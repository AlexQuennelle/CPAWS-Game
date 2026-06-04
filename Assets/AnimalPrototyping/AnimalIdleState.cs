using UnityEngine;
public class AnimalIdleState : AnimalBaseState
{

	[SerializeField] private float _waitTimeRange;
	float _waitTime = 0;
	float _currentTime = 0;
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Idle State Entered");
		_waitTime = Random.Range(0, _waitTimeRange + 1f);
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
