using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
	public event Action<TimeKeeper> OnTimeRunOut;
	public event Action<TimeKeeper, float> OnTimeAdded;

	[SerializeField]
	private float _length = 60f;
	[field: SerializeField]
	public float MaxTime { get; private set; }

	private float _startedAt = 0;
	private float? _endAt = 0;
	public float TimeRemaining
	{
		get
		{
			if (_endAt.HasValue) { return _endAt.Value - Time.time; }
			return float.MaxValue;
		}
	}

	private void OnEnable()
	{
		_startedAt = Time.time;
		_endAt = _startedAt + _length;
	}

	private void OnDisable()
	{
		_endAt = null;
	}

	private void Update()
	{
		if (TimeRemaining <= 0f)
		{
			OnTimeRunOut?.Invoke(this);
			_endAt = null;
		}
	}

	public void ResetTimer()
	{
		_startedAt = Time.time;
		_endAt = _startedAt + _length;
	}
}