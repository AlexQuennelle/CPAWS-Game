using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : BehaviourState
{
	[SerializeField] 
	private float _wanderRange;
	private bool _stateEnabled = false;

	[SerializeField] 
	private float _maxWaitTime;
	[SerializeField]
	private float _minWaitTime;

	private NavMeshAgent _agent;

	private float _waitTime = 0;
	private float _currentTime = 0;

	public void OnEnable()
	{
		_waitTime = Random.Range(_minWaitTime, _maxWaitTime );
	}
	public override void EnterState(NavMeshAgent agent)
	{
		_agent = agent;
		_stateEnabled = true;
		agent.destination = GetWanderPosition();
	}

	private Vector3 GetWanderPosition()
	{
		// Calculate a random position based on the animal's current location
		Vector3 currentPosition = transform.position;

		Vector3 newPosition = new Vector3(
			Random.Range(-_wanderRange, _wanderRange),
			0,
			Random.Range(-_wanderRange, _wanderRange)
		);

		newPosition += currentPosition;

		// Note: If the new position is outside the navmesh, the agent appears to handle it automatically without issue :D
		// This differs from unreal in which the ai would shit itself.

		return newPosition;
	}

	private void Update()
	{
		if (!_stateEnabled) 
		{
			_currentTime += Time.deltaTime;
			if (_currentTime >= _waitTime)
			{
				RaiseRequestEnter();
			}
			return; 
		}
		

		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			_stateEnabled = false;
			_waitTime = Random.Range(_minWaitTime, _maxWaitTime);
			_currentTime = 0;
			RaiseBehaviourEnd();
		}

	}
}
