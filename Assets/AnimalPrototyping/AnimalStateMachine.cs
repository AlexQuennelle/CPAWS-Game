using UnityEngine;
using UnityEngine.AI;
public class AnimalStateMachine : MonoBehaviour
{

	[SerializeField] private GameObject _animalPrototype;
	[SerializeField] private NavMeshAgent _agent;

	AnimalBaseState _currentState;
	public AnimalIdleState IdleState = new AnimalIdleState();
	public AnimalWanderState WanderState = new AnimalWanderState();

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

	public NavMeshAgent GetNavAgent()
	{
		return _animalPrototype.GetComponent<NavMeshAgent>();
	}

	public GameObject GetAnimal() 
	{ 
		return _animalPrototype;
	}
}
