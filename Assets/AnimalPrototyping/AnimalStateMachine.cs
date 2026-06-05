using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

public class AnimalStateMachine : MonoBehaviour
{
	[field: SerializeField]
	public NavMeshAgent Agent { get; private set; }

	[SerializeField]
	private List<BehaviourState> _states;

	[SerializeField]
	private BehaviourState _defaultState;

	private HashSet<BehaviourState> _stateQueue = new();

	private BehaviourState _currentState;

	private void OnEnable()
	{
		// Bind events to each state attached the state machine
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

	/// <summary>
	///   <para> Event handler to process state change requests.</para>
	///   <para> Adds <paramref name="state"/> to a priority queue. </para>
	///   <para> States already in the queue will not be added. </para>
	/// </summary>
	/// <param name="state">
	///   State that requested to be added to the queue.
	/// </param>
	private void HandleStateRequest(BehaviourState state)
	{
		_stateQueue.Add(state);

		if (_currentState.Priority < state.Priority || _currentState == _defaultState)
		{
			SwitchState(state);
		}
	}

	/// <summary>
	///   <para> Event handler for state completion. </para>
	///   <para> Removes <paramref name="state"/> from priority queue. </para>
	///   <para>
	///     Switches to the highest priority state in the queue. If no states
	///     are in the queue, switches to the default state.
	///   </para>
	/// </summary>
	/// <param name="state">
	///   State that signalled its completion.
	/// </param>
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