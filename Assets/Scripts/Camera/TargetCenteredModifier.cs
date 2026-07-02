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
		// float normalizedDist =
		// 	Vector2.Distance(cam.pixelRect.center, bounds.center)
		// 	/ Vector2.Distance(Vector2.zero, cam.pixelRect.center);
		// float normalizedDist = Vector2.Distance(
		// 				new(1.0f / 3.0f * cam.pixelWidth, 1.0f / 3.0f * cam.pixelHeight),
		// 				bounds.center)
		// 			/ (Mathf.Sqrt(1.0f / 3.0f) * cam.pixelHeight);
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
				normalizedDist = SmoothMin(normalizedDist, dist, 1.0f);
			}
		}
		// Debug.Log($"Center: {Vector2.Distance(cam.pixelRect.center, bounds.center)}");
		// normalizedDist = Mathf.Pow(normalizedDist, 2.0f);
		// Debug.Log($"Third: {Vector2.Distance(new Vector2(1.0f / 3.0f * cam.pixelWidth, 1.0f / 3.0f * cam.pixelHeight), bounds.center)}");
		// Debug.Log(cam.pixelHeight);

		Debug.Log(normalizedDist);

		// Modified Gaussian formula
		float finalValue = Mathf.Exp(-(normalizedDist / 0.5f));
		finalValue = _invert ? 1 - finalValue : finalValue;
		finalValue *= _maxMod - _minMod;
		finalValue += _minMod;

		return finalValue;
	}
	private float SmoothMin(float a, float b, float d)
	{
		float h = Mathf.Clamp(0.5f + 0.5f * (a - b) / d, 0.0f, 1.0f);
		return Mathf.Lerp(a, b, h) - (d * h * (1.0f - h));
	}
}