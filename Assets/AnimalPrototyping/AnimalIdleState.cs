using UnityEngine.AI;

public class AnimalIdleState : BehaviourState
{
	public override void EnterState(NavMeshAgent agent)
	{
		agent.destination = transform.position;
	}
}
