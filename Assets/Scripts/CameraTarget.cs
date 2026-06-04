using System.Collections.Generic;

using UnityEngine;

public class CameraTarget : MonoBehaviour
{
	[SerializeField]
	private int _baseScore = 10;
	[SerializeField]
	private List<TargetScoreModifier> _modifiers;

	public int GetScore(Camera cam, Transform transform)
	{
		Rect bounds;

		return _baseScore;
	}
}