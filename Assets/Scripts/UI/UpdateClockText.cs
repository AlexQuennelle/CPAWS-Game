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
		_clockText.text = string.Format("{0:00}:{1:00}", hour, minute);
	}
}
