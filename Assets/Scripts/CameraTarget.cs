using System.Collections.Generic;

using UnityEngine;

/// <summary>
///   Component that marks a <see cref="GameObject"/> as a visible target to be
///   used when calculating the score value of a picture.
/// </summary>
public class CameraTarget : MonoBehaviour
{
	[field: SerializeField]
	public TargetInfo Info { get; private set; }
	[SerializeField]
	private List<TargetScoreModifier> _modifiers;

	/// <summary>
	///   Calculates the final score the target should contribute to the final
	///   picture. The score is calculated using the value in
	///   <see cref="_baseScore"/> and a series of additive and
	///   multiplicative modifiers(<see cref="_modifiers"/>) using
	///   <see langword="float"/> values before being floored to an
	///   <see langword="int"/>.
	/// </summary>
	/// <param name="cam">
	///   Camera to use when calculating the score and related values. This
	///   should be the same camera whose output is used as the final picture.
	/// </param>
	/// <param name="camTransform">
	///   The transform component of the object <paramref name="cam"/> is
	///   attached to.
	/// </param>
	public int? GetScore(Camera cam, Transform camTransform)
	{
		if (!TryGetComponent(out Collider col))
		{
			Debug.LogError($"ERROR: Cannot find Collider on {gameObject.name}");
			return 0;
		}
		Bounds bounds3D = col.bounds;
		Vector3 center = bounds3D.center;
		Vector3 extents = bounds3D.extents;
		Vector3[] screenSpacePoints = {
			cam.WorldToScreenPoint(new(center.x - extents.x, center.y - extents.y, center.z - extents.z)),
			cam.WorldToScreenPoint(new(center.x + extents.x, center.y - extents.y, center.z - extents.z)),
			cam.WorldToScreenPoint(new(center.x + extents.x, center.y + extents.y, center.z - extents.z)),
			cam.WorldToScreenPoint(new(center.x - extents.x, center.y + extents.y, center.z - extents.z)),
			cam.WorldToScreenPoint(new(center.x - extents.x, center.y - extents.y, center.z + extents.z)),
			cam.WorldToScreenPoint(new(center.x + extents.x, center.y - extents.y, center.z + extents.z)),
			cam.WorldToScreenPoint(new(center.x + extents.x, center.y + extents.y, center.z + extents.z)),
			cam.WorldToScreenPoint(new(center.x - extents.x, center.y + extents.y, center.z + extents.z)),
		};
		Rect bounds = new();
		Vector3 topRight = new();
		Vector3 bottomLeft = new(float.MaxValue, float.MaxValue, float.MaxValue);
		foreach (Vector3 point in screenSpacePoints)
		{
			topRight.x = Mathf.Max(topRight.x, point.x);
			topRight.y = Mathf.Max(topRight.y, point.y);
			topRight.z = Mathf.Max(topRight.z, point.z);
			bottomLeft.x = Mathf.Min(bottomLeft.x, point.x);
			bottomLeft.y = Mathf.Min(bottomLeft.y, point.y);
			bottomLeft.z = Mathf.Min(bottomLeft.z, point.z);
		}

		bounds.min = bottomLeft;
		bounds.max = topRight;

		float score = Info.Score;
		foreach (TargetScoreModifier modifier in _modifiers)
		{
			float modValue = modifier.GetValue(cam, camTransform, bounds);
			if (modifier.IsMultiplier)
				score *= modValue;
			else
				score += modValue;
		}

		return Mathf.RoundToInt(score);
	}
}