using System.Collections.Generic;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

public class AnimalHungerState : BehaviourState
{
	[SerializeField]
	private float _maxHunger;
	[SerializeField]
	private float _currentHunger;

	[SerializeField]
	private float _maxEatTime; // time it takes the animal to eat something
	private float _currentEatTime = 0;


	private bool _stateEnabled = false;

	[SerializeField]
	private FoodType _viableFood; // Could probably be changed to a list if an animal has multiple viable food types
	private FoodSource _nearestFoodSource;	

	private NavMeshAgent _agent;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
		_agent = agent;
		List<FoodSource> _foodSources = new(FindObjectsByType<FoodSource>(FindObjectsInactive.Exclude));

		// Find nearest food source	
		_nearestFoodSource = _foodSources[0];

		// Exclude non-viable food sources
		foreach (FoodSource food in _foodSources)
		{
			if (food.Type != _viableFood)
			{
				_foodSources.Remove(food);
			}	
		}
		_nearestFoodSource = _foodSources.OrderByDescending(food => Vector3.Distance(_agent.transform.position, food.transform.position)).Last();

		agent.SetDestination(_nearestFoodSource.transform.position);
	}

	private void Update()
	{
		// Make animal more hungry over time
		if (!_stateEnabled)
		{
			_currentHunger -= Time.deltaTime;
			if(_currentHunger <= 0)
			{
				RaiseRequestEnter();
			}
			return;
		}

		// Execute munch logic upon reaching food source
		if(_agent.remainingDistance <= _agent.stoppingDistance)
		{			
			_agent.SetDestination(_agent.transform.position);
			_currentEatTime += Time.deltaTime;
			if(_currentEatTime >= _maxEatTime)
			{
				_currentHunger += _nearestFoodSource.Value; // NOM
				if (_currentHunger > _maxHunger) _currentHunger = _maxHunger;

				_currentEatTime = 0;
				_stateEnabled = false;
				RaiseBehaviourEnd();							
			}				
		}		
	}
}
