using UnityEngine;

public class TargetCenteredModifier : TargetScoreModifier
{
	[SerializeField]
	private float _minMod = 0.0f;
	[SerializeField]
	private float _maxMod = 1.0f;
	[SerializeField]
	private bool _invert = false;

	public override float GetValue(Camera cam, Transform transform, Rect bounds)
	{
		return 0.0f;
	}
}