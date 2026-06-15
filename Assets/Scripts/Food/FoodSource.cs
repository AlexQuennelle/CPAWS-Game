using UnityEngine;
public enum FoodType
{
	Apple
}
public class FoodSource : MonoBehaviour
{
	[field: SerializeField]
	public int Value { get; private set; }

	[field: SerializeField]
	public FoodType Type { get; private set; }
}