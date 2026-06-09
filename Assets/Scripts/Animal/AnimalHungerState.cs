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

	private bool _stateEnabled = false;

	[SerializeField]
	private FoodSource _viableFood; // Could probably be changed to a list if an animal has multiple viable food types
	[SerializeField]
	private FoodSource _nearestFoodSource;

	private List<FoodSource> _foodSources;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;

		// Get all food source objects that this animal can eat. (I was filled with jubilation when i figured this out)
		foreach (FoodSource food in FindObjectsByType<FoodSource>())
		{
			if (food != null && food.GetType() == _viableFood.GetType())
			{
				Debug.Log(food);
				_foodSources.Add(food); // This is throwing a null reference exception error for some reason???? w H Y????
			}
			
		}

		// Find nearest food source	
		/*_nearestFoodSource = _foodSources[0];
		foreach (FoodSource food in _foodSources)
		{
			// Theres gotta be a cleaner implementation than this, surely
			if(Vector3.Distance(agent.transform.position, food.transform.position) < Vector3.Distance(_nearestFoodSource.transform.position, food.transform.position))
			{
				_nearestFoodSource = food;
			}
		}*/
	}

	private void Update()
	{
		if (!_stateEnabled)
		{
			_currentHunger -= Time.deltaTime;
			if(_currentHunger <= _minHunger)
			{
				RaiseRequestEnter();
			}
			return;
		}

		// Move towards food source

		// Execute munch logic upon reaching food source

		_stateEnabled = false;
		_currentHunger = _maxHunger;
		RaiseBehaviourEnd();
	}
}
