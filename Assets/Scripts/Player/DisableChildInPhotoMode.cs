using System;

using UnityEngine;

public class DisableChildInPhotoMode : MonoBehaviour
{
	[Header("WARNING: Do not use this component to disable the gameobject that it is placed on. It will be unable to re-enable itself.")]
	[SerializeField]
	private PlayerPerspectiveHandler _playerPerspectiveHandler;

	[SerializeField, Tooltip("The GameObject that will be disabled during photo mode.")]
	private GameObject _objectToToggle;

	[SerializeField, Tooltip("Inverts the component to only have the object enabled during photo mode.")]
	private bool _invert = false;

	private void OnEnable()
	{
		_playerPerspectiveHandler.OnPerspectiveChange += HandlePerspectiveChange; 
	}

	private void OnDisable()
	{
		_playerPerspectiveHandler.OnPerspectiveChange -= HandlePerspectiveChange;
	}

	private void HandlePerspectiveChange(PlayerPerspectiveHandler perspectiveHandler)
	{
		if (perspectiveHandler.IsPhotoMode) _objectToToggle.SetActive(_invert);
		else _objectToToggle.SetActive(!_invert);
	}
}
