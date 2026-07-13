using System;

using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
	public event Action<TimeKeeper> OnTimeRunOut;
	public event Action<TimeKeeper> OnTimerReset;

	[SerializeField]
	private float _length = 60f;

	[field: SerializeField, Tooltip("The total amount of time in the timer.")]
	public float MaxTime { get; private set; }

	private float _startedAt = 0;
	public float? EndAt { get; private set; } = 0;
	public float TimeRemaining
	{
		get
		{
			return EndAt.HasValue ? EndAt.Value - Time.time : float.MaxValue;
		}
	}

	private void OnEnable()
	{
		_startedAt = Time.time;
		EndAt = _startedAt + _length;
	}

	private void OnDisable()
	{
		EndAt = null;
	}

	private void Update()
	{
		if (TimeRemaining <= 0f)
		{
			OnTimeRunOut?.Invoke(this);
			EndAt = null;
		}
	}

	public void ResetTimer()
	{
		_startedAt = Time.time;
		EndAt = _startedAt + _length;
		OnTimerReset?.Invoke(this);
	}
}