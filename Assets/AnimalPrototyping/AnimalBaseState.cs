using UnityEngine;

public abstract class AnimalBaseState : MonoBehaviour
{
	public abstract void EnterState(AnimalStateMachine animal);

	public abstract void UpdateState(AnimalStateMachine animal);
}
