using UnityEngine;
using UnityEngine.AI;

public class AnimalFleeState : BehaviourState
{
	private NavMeshAgent _agent;
	private GameObject _threat;

	private bool _stateEnabled = false;

	[SerializeField]
	private SphereCollider _detectionSphere;
	[SerializeField]
	private float _fleeDistance;
	[SerializeField]
	private int _fearTolerance;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
		_agent = agent;

		if (_threat != null) _agent.SetDestination(GetFleePosition(_threat));
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.GetComponent<ScaryComponent>() != null)
		{
			if (collider.gameObject.GetComponent<ScaryComponent>().Scariness > _fearTolerance)
			{
				_threat = collider.gameObject;
				RaiseRequestEnter();
			}
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (!_stateEnabled) return;

		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			// Keep fleeing if the threat is within the detection radius
			if (Vector3.Distance(transform.position, _threat.transform.position) < _detectionSphere.radius)
			{
				_agent.SetDestination(GetFleePosition(_threat));
			}
			else
			{
				_stateEnabled = false;
				RaiseBehaviourEnd();
			}
		}
	}

	// Calculate a posiion far away from the threat
	Vector3 GetFleePosition(GameObject threat)
	{
		// TO-DO: Currently the animal only calculates positions directly opposite the threat
		// This means they get cornered and stuck very easily. Need to find a way around this
		Vector3 currentPosition = transform.position;
		Vector3 heading = currentPosition - threat.transform.position;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;

		Vector3 newPosition = currentPosition + (direction * 10);

		newPosition += currentPosition;

		return newPosition;
	}
}
