using UnityEngine;
using UnityEngine.AI;

public class AnimalFleeState : BehaviourState
{

	private NavMeshAgent _agent;
	private GameObject _player;

	private bool _stateEnabled = false;

	[SerializeField]
	private float _fleeDistance;

	public override void EnterState(NavMeshAgent agent)
	{
		_stateEnabled = true;
		Debug.Log("State Entered");
		_agent = agent;

		if (_player != null) _agent.SetDestination(GetFleePosition(_player));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Moose") // change this later, string matching bad or somethn
		{
			_player = other.gameObject;
			RaiseRequestEnter();
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (!_stateEnabled) return;

		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			_stateEnabled = false;
			RaiseBehaviourEnd();
		}
	}

	Vector3 GetFleePosition(GameObject player) // how the hell am i gonna get the player
	{
		// Calculate a posiion far away in the opposite direction of the player
		// How the hell do i calculate direction
		// help me

		Vector3 currentPosition = transform.position;
		Vector3 heading = transform.position - player.transform.position;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;

		Vector3 newPosition = currentPosition + (direction * 10);

		newPosition += currentPosition;

		return newPosition;
	}
}
