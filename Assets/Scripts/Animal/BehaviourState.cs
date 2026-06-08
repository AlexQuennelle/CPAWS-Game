using System;

using UnityEngine;
using UnityEngine.AI;

public abstract class BehaviourState : MonoBehaviour
{
	/// <summary>
	///   Event raised when the current <see cref="BehaviourState"/> is ready to
	///   become active.
	/// </summary>
	public event Action<BehaviourState> OnRequestEnter;
	/// <summary>
	///   Event raised when the current <see cref="BehaviourState"/> has
	///   finished. Signals the next state in the queue should become active.
	/// </summary>
	public event Action<BehaviourState> OnBehaviourEnd;

	[field: SerializeField]
	public int Priority { get; private set; }

	protected void RaiseRequestEnter()
	{
		OnRequestEnter?.Invoke(this);
	}
	protected void RaiseBehaviourEnd()
	{
		OnBehaviourEnd?.Invoke(this);
	}


	/// <param name="agent">
	///   The <see cref="NavMeshAgent"/> that handles the attached animal's
	///   movement.
	/// </param>
	public abstract void EnterState(NavMeshAgent agent);
}