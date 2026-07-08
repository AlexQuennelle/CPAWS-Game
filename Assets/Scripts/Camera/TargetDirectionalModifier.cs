using System.Diagnostics;

using UnityEngine;

public class TargetDirectionalModifier : TargetScoreModifier
{
	[SerializeField, Tooltip(
			"How should the directional information be treated?\n\n" +
			"<b>Front</b>: The score will be highest when the attached" +
			" transform's forward direction is facing towards the camera, and" +
			" lowest when pointing away.\n" +
			"<b>Back</b>: Opposite of <b>Front</b>.\n" +
			"<b>Front Or Back</b>: The score will be highest when the" +
			" attached transform's forward direction is facing either" +
			" directly towards or directly away from the camera, and lowest" +
			" when facing perpendicular to the camera.\n" +
			"<b>Sides</b>: Opposite of <b>Front Or Back</b>.")]
	private DirectionMode _mode = DirectionMode.Front;
	[SerializeField, Min(0.0f)]
	private float _minMod = 0.0f;
	[SerializeField, Min(0.0001f)]
	private float _maxMod = 1.0f;

	public override float GetValue(
			Camera cam, Transform camTransform, Rect bounds)
	{
		float dotProd = Vector3.Dot(
				transform.forward,
				(transform.position - camTransform.position).normalized);
		float scoreRange = _maxMod - _minMod;
		return _mode switch
		{
			DirectionMode.Front =>
				((dotProd + 1.0f) * (scoreRange / 2.0f)) + _minMod,
			DirectionMode.Back =>
				(((dotProd + 1.0f) * (scoreRange / 2.0f)) + _minMod) * -1.0f,
			DirectionMode.FrontOrBack =>
				(Mathf.Abs(dotProd) * scoreRange) + _minMod,
			DirectionMode.Sides =>
				((1.0f - Mathf.Abs(dotProd)) * scoreRange) + _minMod,
			_ => 0.0f,
		};
	}

	private enum DirectionMode { Front, Back, FrontOrBack, Sides, }
}