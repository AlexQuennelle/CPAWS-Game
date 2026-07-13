using UnityEngine;

public class LerpRotationOverDaytime : MonoBehaviour
{
	[SerializeField]
	private TimeKeeper _daytimeTracker;
	[SerializeField]
	private float _sunriseAngle = 0f;
	[SerializeField]
	private float _sunsetAngle = 180f;

	private void Update()
	{
		if (_daytimeTracker.EndAt == null) return;

		float angle = Mathf.Lerp(_sunsetAngle, _sunriseAngle, _daytimeTracker.TimeRemaining / _daytimeTracker.MaxTime);
		transform.rotation = Quaternion.Euler(angle, 0, 0);
	}
}