using UnityEngine;
using UnityEngine.AI;

public class AnimalHungerState : BehaviourState
{
	[SerializeField]
	private float _minHunger;
	[SerializeField]
	private float _maxHunger;
	private float _currentHunger;

	private bool _stateEnabled = false;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
	}

	private void Update()
	{
		if (!_stateEnabled)
		{
			_currentHunger += Time.deltaTime;
			if(_currentHunger >= _maxHunger)
			{
				_currentHunger = _minHunger;
				RaiseRequestEnter();
			}
			return;
		}

		// Find nearest food source and start moving towards it

		// Execute munch logic upon reaching food source
	}
}
