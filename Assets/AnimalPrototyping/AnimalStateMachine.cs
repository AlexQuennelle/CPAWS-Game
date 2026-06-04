using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;
public class AnimalStateMachine : MonoBehaviour
{
	[field:SerializeField]
	public NavMeshAgent Agent { get; private set; }

	[SerializeField]
	private List<BehaviourState> _states;

	[SerializeField]
	private BehaviourState _defaultState;

	private HashSet<BehaviourState> _stateQueue = new HashSet<BehaviourState>();

	private BehaviourState _currentState;

	private void OnEnable()
	{
		foreach (BehaviourState state in _states)
		{
			state.OnRequestEnter += HandleStateRequest;
			state.OnBehaviourEnd += HandleStateEnd;
		}
	}

	private void OnDisable()
	{
		foreach (BehaviourState state in _states)
		{
			state.OnRequestEnter -= HandleStateRequest;
			state.OnBehaviourEnd -= HandleStateEnd;
		}
	}

	private void Start()
	{
		_currentState = _defaultState;
		_currentState.EnterState(Agent);
	}

	private void HandleStateRequest(BehaviourState state)
	{
		_stateQueue.Add(state);

		if(_currentState.Priority < state.Priority || _currentState == _defaultState)
		{
			SwitchState(state);
		}
	}

	private void HandleStateEnd(BehaviourState state)
	{
		_stateQueue.Remove(state);

		if (_stateQueue.Count > 0)
		{
			SwitchState(_stateQueue.OrderByDescending(item => item.Priority).First());
		}
		else
		{
			_currentState = _defaultState;
			_currentState.EnterState(Agent);
		}
	}

	private void SwitchState(BehaviourState state)
	{
		_currentState = state;
		_currentState.EnterState(Agent);
		_stateQueue.Remove(state);
	}
}
