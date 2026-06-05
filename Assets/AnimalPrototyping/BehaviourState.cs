using System;

using UnityEngine;
using UnityEngine.AI;

public abstract class BehaviourState : MonoBehaviour
{
	public event Action<BehaviourState> OnRequestEnter;
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


	public abstract void EnterState(NavMeshAgent agent);
}