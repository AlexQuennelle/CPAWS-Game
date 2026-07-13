using TMPro;

using UnityEngine;

public class UpdateClockText : MonoBehaviour
{
	[SerializeField]
	private TimeKeeper _daytimeTracker;
	[SerializeField]
	private TextMeshProUGUI _clockText;
	[SerializeField]
	private int _startingHour = 9; // 9AM

	private void Update()
	{
		if (_daytimeTracker.EndAt == null)
		{
			_clockText.text = "DAY OVER";
			return;
		}

		int time = Mathf.FloorToInt(_daytimeTracker.MaxTime - _daytimeTracker.TimeRemaining);
		int minute = time % 60;
		int hour = (time / 60) + _startingHour;

		_clockText.text = hour switch
		{
			< 12 => string.Format("{0:00}:{1:00}", hour, minute) + " AM",
			12 => string.Format("{0:00}:{1:00}", hour, minute) + " PM",
			_ => string.Format("{0:00}:{1:00}", hour - 12, minute) + " PM",
		};
	}
}