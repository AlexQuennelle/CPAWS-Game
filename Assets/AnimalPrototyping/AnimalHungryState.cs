using UnityEngine;

public class AnimalHungryState : AnimalBaseState
{
	public override void EnterState(AnimalStateMachine animal)
	{
		Debug.Log("Hungry State Entered");
	}

	public override void UpdateState(AnimalStateMachine animal)
	{
		animal.SwitchState(animal.IdleState);
	}

	
	/*FoodSource GetFoodLocation() 
	{
		FoodSource food = find;
		
		return food;
	}*/
}
