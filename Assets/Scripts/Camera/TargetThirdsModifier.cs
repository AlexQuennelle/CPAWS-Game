using UnityEngine;

public class TargetCenteredModifier : TargetScoreModifier
{
	[SerializeField, Min(0.0f)]
	private float _minMod = 0.0f;
	[SerializeField, Min(0.0001f)]
	private float _maxMod = 1.0f;
	[SerializeField]
	private bool _invert = false;

	public override float GetValue(
			Camera cam, Transform camTransform, Rect bounds)
	{
		// Get the minimum distance from one of 4 points that are each 1/3rd
		// from the edges.
		float normalizedDist = float.MaxValue;
		float[] thirds = { 1.0f / 3.0f, 2.0f / 3.0f };
		foreach (float x in thirds)
		{
			foreach (float y in thirds)
			{
				float dist = Vector2.Distance(
						new(x * cam.pixelWidth, y * cam.pixelHeight),
						bounds.center)
					/ (Mathf.Sqrt(1.0f / 3.0f) * cam.pixelHeight);
				normalizedDist = Mathf.Min(normalizedDist, dist);
			}
		}

		// Modified Gaussian formula
		float finalValue = Mathf.Exp(-(normalizedDist / 0.5f));
		finalValue = _invert ? 1 - finalValue : finalValue;
		finalValue *= _maxMod - _minMod;
		finalValue += _minMod;

		return finalValue;
	}
}