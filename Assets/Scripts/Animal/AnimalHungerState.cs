using System.Collections.Generic;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

public class AnimalHungerState : BehaviourState
{
	[SerializeField]
	private float _minHunger;
	[SerializeField]
	private float _maxHunger;
	[SerializeField]
	private float _currentHunger;

	[SerializeField]
	private float _maxEatTime; // time it takes the animal to eat something
	private float _currentEatTime = 0;


	private bool _stateEnabled = false;

	[SerializeField]
	private FoodSource _viableFood; // Could probably be changed to a list if an animal has multiple viable food types
	[SerializeField]
	private FoodSource _nearestFoodSource;

	[SerializeField] // do NOT remove serializefield from this list, it shits itself when you do
	private List<FoodSource> _foodSources;

	private NavMeshAgent _agent;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
		_agent = agent;

		// Get all food source objects that this animal can eat. (I was filled with jubilation when i figured this out)
		foreach (FoodSource food in FindObjectsByType<FoodSource>())
		{
			if (food != null && food.GetType() == _viableFood.GetType())
			{
				_foodSources.Add(food);
			}			
		}

		// Find nearest food source	
		_nearestFoodSource = _foodSources[0];
		foreach (FoodSource food in _foodSources)
		{
			// Theres gotta be a cleaner implementation than this, surely
			if(Vector3.Distance(agent.transform.position, food.transform.position) < Vector3.Distance(_nearestFoodSource.transform.position, food.transform.position))
			{
				_nearestFoodSource = food;
			}
		}

		agent.SetDestination(_nearestFoodSource.transform.position);
	}

	private void Update()
	{
		// Make animal more hungry over time
		if (!_stateEnabled)
		{
			_currentHunger -= Time.deltaTime;
			if(_currentHunger <= _minHunger)
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
				_currentHunger += _nearestFoodSource._value; // NOM
				if (_currentHunger > _maxHunger) _currentHunger = _maxHunger;

				_foodSources.Clear();
				RaiseBehaviourEnd();
				_stateEnabled = false;
				_currentEatTime = 0;
			}				
		}		
	}
}
