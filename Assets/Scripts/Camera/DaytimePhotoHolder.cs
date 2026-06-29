using System;
using System.Collections.Generic;

using UnityEngine;

public class DaytimePhotoHolder : MonoBehaviour
{
	public event Action<DaytimePhotoHolder> OnAllPhotosTaken;

	[SerializeField]
	private int _photosPerDay = 10;

	[SerializeField]
	private CameraSensor _cameraSensor;

	public List<PictureInfo> _photos { get; private set; } = new();

	private int _photosTaken;

	private void OnEnable()
	{
		_cameraSensor.OnPictureTaken += HandlePictureTaken;
		_photosTaken = 0;
	}

	private void OnDisable()
	{
		_cameraSensor.OnPictureTaken -= HandlePictureTaken;
	}

	private void HandlePictureTaken(CameraSensor sens, PictureInfo picInfo)
	{
		_photos.Add(picInfo);
		_photosTaken++;
		if(_photosTaken >= _photosPerDay)
		{
			OnAllPhotosTaken?.Invoke(this);
		}
	}
}
