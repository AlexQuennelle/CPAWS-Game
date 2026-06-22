using UnityEngine;
using UnityEngine.UI;

public class SetTextureToCameraOutput : MonoBehaviour
{
	[SerializeField]
	private CameraSensor _cameraSensor;

	[SerializeField]
	private RawImage _rawImage;

	private void OnEnable()
	{
		_cameraSensor.OnPictureTaken += HandlePictureTaken;
	}

	private void OnDisable()
	{
		_cameraSensor.OnPictureTaken -= HandlePictureTaken;
	}

	private void HandlePictureTaken(CameraSensor sensor, PictureInfo info)
	{
		_rawImage.texture = info.Tex;
	}
}