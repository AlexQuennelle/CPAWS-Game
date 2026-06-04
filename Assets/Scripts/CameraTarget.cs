using System.Collections.Generic;

using UnityEngine;

public class CameraTarget : MonoBehaviour
{
	[SerializeField]
	private int _baseScore = 10;
	[SerializeField]
	private List<TargetScoreModifier> _modifiers;
	[Header("Debug")]
	[SerializeField]
	private RectTransform _debugRect;

	public int GetScore(Camera cam, Transform transform)
	{
		if (!TryGetComponent<Collider>(out Collider collider))
		{
			Debug.LogError($"ERROR: Cannot find Collider on {this.gameObject.name}");
			return 0;
		}
		Bounds bounds3D = collider.bounds;
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
		Vector3 bottomLeft = new();
		foreach (Vector3 point in screenSpacePoints)
		{
			topRight.x = Mathf.Max(topRight.x, point.x);
			topRight.y = Mathf.Max(topRight.y, point.y);
			topRight.z = Mathf.Max(topRight.z, point.z);
			bottomLeft.x = Mathf.Min(topRight.x, point.x);
			bottomLeft.y = Mathf.Min(topRight.y, point.y);
			bottomLeft.z = Mathf.Min(topRight.z, point.z);
		}

		bounds.min = bottomLeft;
		bounds.max = topRight;

		_debugRect.position = bottomLeft;
		Debug.Log($"rect: {bottomLeft} - {topRight}");

		return _baseScore;
	}
}