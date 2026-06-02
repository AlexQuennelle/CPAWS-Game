using UnityEngine;
using UnityEngine.AI;
public class AnimalStateMachine : MonoBehaviour
{
	[SerializeField] private GameObject _animalPrototype;
	[SerializeField] private NavMeshAgent _agent;
	public GameObject Animal { get { return _animalPrototype; } }
	public float Test { get; private set; }
	public NavMeshAgent Agent { get { return _agent; } }

	AnimalBaseState _currentState;

	[SerializeField]
	public AnimalBaseState IdleState;
	[SerializeField]
	public AnimalBaseState WanderState;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_currentState = IdleState;
		_currentState.EnterState(this);
	}

	// Update is called once per frame
	void Update()
	{
		_currentState.UpdateState(this);
	}

	public void SwitchState(AnimalBaseState state)
	{
		_currentState = state;
		_currentState.EnterState(this);
	}
}
