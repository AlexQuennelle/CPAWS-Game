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
	private FoodSource _food;
	private FoodSource _nearestFoodObj;

	[SerializeField]
	private List<FoodSource> _foodSources;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;

		Debug.Log(_food.GetType());

		// Get all food source objects that this animal can eat.
		foreach (FoodSource food in FindObjectsByType<FoodSource>())
		{
			if (food.GetType() == _food.GetType())
			{
				_foodSources.Add(food);
			}
		}


		// Find nearest food source	
		_nearestFoodObj = _foodSources[0];
		foreach (FoodSource food in _foodSources)
		{
			// Theres gotta be a cleaner implementation than this, surely
			if(Vector3.Distance(agent.transform.position, food.transform.position) < Vector3.Distance(_nearestFoodObj.transform.position, food.transform.position))
			{
				_nearestFoodObj = food;
			}
		}
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
