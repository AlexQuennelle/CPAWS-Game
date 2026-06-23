using System.Collections.Generic;
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

	// Could probably be changed to a list if an animal has multiple viable
	// food types
	[SerializeField]
	private FoodType _viableFood;
	private FoodSource _nearestFoodSource;

	private NavMeshAgent _agent;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
		_agent = agent;
		List<FoodSource> foodSources =
			new(FindObjectsByType<FoodSource>(FindObjectsInactive.Exclude));

		// Find nearest food source	
		if
		(
			foodSources.Where(source => source.Type == _viableFood)
			.Where(source => agent.CalculatePath(source.transform.position, agent.path) == true).Count() > 0
		)
		{
			_nearestFoodSource =
			foodSources.Where(source => source.Type == _viableFood)
			.Where(source => agent.CalculatePath(source.transform.position, agent.path) == true)
			.OrderByDescending(
					food => Vector3.Distance(
						agent.transform.position, food.transform.position))
			.Last();
		}

		Debug.Log(_nearestFoodSource);

		if (_nearestFoodSource != null)
		{
			agent.SetDestination(_nearestFoodSource.transform.position);
		}
		else
		{
			Debug.Log("No reachable food sources found");
			_currentHunger = _maxHunger;
			_currentEatTime = 0;
			_stateEnabled = false;
			RaiseBehaviourEnd();
		}

		//Test path calculation
		/*NavMeshPath navMeshPath = new();
		Vector3 target = new Vector3(5, 0, 5);
		Debug.Log(agent.CalculatePath(target, navMeshPath));*/
	}

	private void Update()
	{
		// Make animal more hungry over time
		if (!_stateEnabled)
		{
			_currentHunger -= Time.deltaTime;
			if (_currentHunger <= 0)
			{
				RaiseRequestEnter();
			}
			return;
		}

		// Execute munch logic upon reaching food source
		else if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			_agent.SetDestination(_agent.transform.position);
			_currentEatTime += Time.deltaTime;
			if (_currentEatTime >= _maxEatTime)
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