using Unity.VisualScripting;

using UnityEngine;
public enum FoodType
{
	Apple
}
public class FoodSource : MonoBehaviour
{
	[field: SerializeField]
	public int _value { get; private set; }

	[field: SerializeField]
	public FoodType _type { get; private set; }
}
