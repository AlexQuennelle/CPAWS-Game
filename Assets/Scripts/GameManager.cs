using System;

using Unity.VisualScripting;

using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Game Loop Trackers")]
	[SerializeField]
	private TimeKeeper _daytimeTracker;

	[SerializeField]
	private DaytimePhotoHolder _daytimePhotoHolder;

	[Header("UI")]
	[SerializeField]
	private GameObject _photoModeUI;
	[SerializeField]
	private GameObject _overworldUI;
	[SerializeField]
	private GameObject _endOfDayUI;

	[Header("Player")]
	[SerializeField]
	private PlayerPerspectiveHandler _perspectiveHandler;
	[SerializeField]
	private PlayerInputHandler _inputHandler;

	private void OnEnable()
	{
		_daytimeTracker.OnTimeRunOut += HandleDayEndViaTime;
		_daytimePhotoHolder.OnAllPhotosTaken += HandleDayEndViaPhotoLimit;
	}

	private void OnDisable()
	{
		_daytimeTracker.OnTimeRunOut -= HandleDayEndViaTime;
		_daytimePhotoHolder.OnAllPhotosTaken -= HandleDayEndViaPhotoLimit;
	}

	private void HandleDayEndViaPhotoLimit(DaytimePhotoHolder holder) { EndDay(); }
	private void HandleDayEndViaTime(TimeKeeper timeKeeper) { EndDay(); }

	private void EndDay()
	{
		_photoModeUI.SetActive(false);
		_overworldUI.SetActive(false);
		_endOfDayUI.SetActive(true);
		_perspectiveHandler.SetPerspective(isPhotoMode: false);
		_inputHandler.enabled = false;
	}
}
