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
		int time = Mathf.FloorToInt(_daytimeTracker.MaxTime - _daytimeTracker.TimeRemaining);
		int minute = time % 60;
		int hour = (time / 60) + _startingHour;

		if(hour < 12)
		{
			_clockText.text = string.Format("{0:00}:{1:00}", hour, minute) + " AM";
		}
		else if(hour == 12)
		{
			_clockText.text = string.Format("{0:00}:{1:00}", hour, minute) + " PM";
		}
		else
		{
			_clockText.text = string.Format("{0:00}:{1:00}", hour - 12, minute) + " PM";
		}
	}
}
