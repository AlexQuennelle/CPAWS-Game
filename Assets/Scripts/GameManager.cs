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
	[SerializeField]
	private DisplayEndOfDayPhotos _endOfDayPhotoDisplay;

	[Header("Player")]
	[SerializeField]
	private PlayerPerspectiveHandler _perspectiveHandler;
	[SerializeField]
	private PlayerInputHandler _inputHandler;

	private void OnEnable()
	{
		_daytimeTracker.OnTimeRunOut += HandleDayEndViaTime;
		_daytimePhotoHolder.OnAllPhotosTaken += HandleDayEndViaPhotoLimit;
		_endOfDayPhotoDisplay.OnSelectionComplete += HandlePhotoSelectionComplete;

		_photoModeUI.SetActive(false);
		_overworldUI.SetActive(true);
		_endOfDayUI.SetActive(false);
	}

	private void OnDisable()
	{
		_daytimeTracker.OnTimeRunOut -= HandleDayEndViaTime;
		_daytimePhotoHolder.OnAllPhotosTaken -= HandleDayEndViaPhotoLimit;
		_endOfDayPhotoDisplay.OnSelectionComplete -= HandlePhotoSelectionComplete;
	}

	private void HandleDayEndViaPhotoLimit(DaytimePhotoHolder holder) { EndDay(); }
	private void HandleDayEndViaTime(TimeKeeper timeKeeper) { EndDay(); }

	private void HandlePhotoSelectionComplete(DisplayEndOfDayPhotos photos, PictureInfo[] arg2)
	{
		// save photos here?


	}

	private void EndDay()
	{
		_photoModeUI.SetActive(false);
		_overworldUI.SetActive(false);
		_endOfDayUI.SetActive(true);
		_endOfDayPhotoDisplay.DisplayPhotos(_daytimePhotoHolder.Photos);
		_perspectiveHandler.SetPerspective(isPhotoMode: false);
		_inputHandler.enabled = false;
	}
}
