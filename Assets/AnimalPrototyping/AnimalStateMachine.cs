using UnityEngine;
using UnityEngine.AI;
public class AnimalStateMachine : MonoBehaviour
{
	// Goal: reusable, no circle reference, pleasant to use animal ai brain
	// Problem: how the fuck

	[field:SerializeField]
	public NavMeshAgent Agent { get; private set; }
	[field:SerializeField]
	public NavMeshAgent Animal { get; private set; }

	private AnimalBaseState _currentState;

	[SerializeField] 
	private float hunger = 0;

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

		// Basic hunger system, will be better implemented later
		hunger += Time.deltaTime / 60;
		if(hunger >= 20)
		{
			//SwitchState();
			hunger = 0;
		}
	}

	public void SwitchState(AnimalBaseState state)
	{
		_currentState = state;
		_currentState.EnterState(this);
	}
}
