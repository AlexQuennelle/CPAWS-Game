using UnityEngine;

public abstract class TargetScoreModifier : MonoBehaviour
{
	[field: SerializeField]
	public bool IsMultiplier { get; private set; }

	public abstract float GetValue(Camera cam, Transform transform, Rect bounds);
}