using UnityEngine;
using UnityEngine.AI;

public class AnimalIdleState : BehaviourState
{
	[SerializeField]
	private Animator _animator;
	public override void EnterState(NavMeshAgent agent)
	{
		_animator.SetTrigger("Idle");
		agent.destination = transform.position;
	}
}