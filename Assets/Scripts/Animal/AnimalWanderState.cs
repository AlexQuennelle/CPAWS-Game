using UnityEngine;
using UnityEngine.AI;

public class AnimalWanderState : BehaviourState
{
	[SerializeField]
	private float _minWanderRange;
	[SerializeField]
	private float _maxWanderRange;

	private float _wanderRange;

	// Timer vars for raising an enter request
	[SerializeField]
	private float _maxWaitTime;
	[SerializeField]
	private float _minWaitTime;
	private float _waitTime = 0;

	private float _currentTime = 0;

	private NavMeshAgent _agent;

	public void OnEnable()
	{
		_waitTime = Random.Range(_minWaitTime, _maxWaitTime);
	}
	public override void EnterState(NavMeshAgent agent)
	{
		_agent = agent;
		StateEnabled = true;
		agent.destination = GetWanderPosition();
	}

	// Calculates a random position based on the animal's current location
	private Vector3 GetWanderPosition()
	{
		_wanderRange = Random.Range(_minWanderRange, _maxWanderRange);

		Vector3 currentPosition = transform.position;

		Vector3 newPosition = new(
			Random.Range(-_wanderRange, _wanderRange),
			0,
			Random.Range(-_wanderRange, _wanderRange)
		);

		newPosition += currentPosition;

		return newPosition;
	}

	private void Update()
	{
		if (!StateEnabled)
		{
			// Periodically request to enter this state
			_currentTime += Time.deltaTime;
			if (_currentTime >= _waitTime)
			{
				_currentTime = 0;
				RaiseRequestEnter();
			}
			return;
		}


		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			StateEnabled = false;
			_waitTime = Random.Range(_minWaitTime, _maxWaitTime);
			_currentTime = 0;
			RaiseBehaviourEnd();
		}

	}
}