using UnityEngine;

public class TargetCenteredModifier : TargetScoreModifier
{
	[SerializeField, Min(0.0f)]
	private float _minMod = 0.0f;
	[SerializeField, Min(0.0001f)]
	private float _maxMod = 1.0f;
	[SerializeField]
	private bool _invert = false;

	public override float GetValue(Camera cam, Transform transform, Rect bounds)
	{
		float normalizedDist =
			Vector2.Distance(cam.pixelRect.center, bounds.center)
			/ Vector2.Distance(Vector2.zero, cam.pixelRect.center);
		normalizedDist = Mathf.Pow(normalizedDist, 2.0f);

		// Modified Gaussian formula
		float finalValue = Mathf.Exp(-(normalizedDist / 0.5f));
		finalValue = _invert ? 1 - finalValue : finalValue;
		finalValue *= _maxMod - _minMod;
		finalValue += _minMod;

		return finalValue;
	}
}