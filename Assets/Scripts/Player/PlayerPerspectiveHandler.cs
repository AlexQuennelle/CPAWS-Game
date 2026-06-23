using System;
using System.Collections.Generic;

using UnityEngine;

public class PlayerPerspectiveHandler : MonoBehaviour
{
	[Serializable]
	class PhotoModeToggleable
	{
		[field: SerializeField, Tooltip("Object to be toggled.")]
		public GameObject Object { get; private set; }

		[field: SerializeField, Tooltip(
				"Whether to hide or show the object in photo mode.")]
		public bool EnabledInPhotoMode { get; private set; } = false;
	}

	public event Action<PlayerPerspectiveHandler> OnPerspectiveChange;

	[SerializeField]
	private PlayerLook _playerLook;

	[SerializeField]
	private List<PhotoModeToggleable> _objectsToToggle;

	public bool IsPhotoMode { get; private set; } = false;
	public void TogglePerspective()
	{
		// foreach (InputActionMap map in InputSystem.actions.actionMaps)
		// {
		// 	map.Disable();
		// }

		IsPhotoMode = !IsPhotoMode;
		if (IsPhotoMode)
		{
			_playerLook.MaxVerticalLook = 80f;
			_playerLook.MinVerticalLook = -80f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			foreach (var toggleable in _objectsToToggle)
			{
				toggleable.Object.SetActive(toggleable.EnabledInPhotoMode);
			}
			// switch (_deviceHandler.ActiveInputDevice)
			// {
			// 	case SupportedInputDevices.MouseAndKeyboard:
			// 		Debug.LogWarning("ActionMap not set up for m+kb");
			// 		break;
			// 	case SupportedInputDevices.Touchscreen:
			// 		InputSystem.actions.FindActionMap("PlayerCamera(Touch)")
			// 			.Enable();
			// 		break;
			// 	default:
			// 		break;
			// }
		}
		else
		{
			_playerLook.MaxVerticalLook = 70f;
			_playerLook.MinVerticalLook = -55f;
			_playerLook.HandleLook(new Vector2(0, 0));
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			foreach (var toggleable in _objectsToToggle)
			{
				toggleable.Object.SetActive(!toggleable.EnabledInPhotoMode);
			}
			// switch (_deviceHandler.ActiveInputDevice)
			// {
			// 	case SupportedInputDevices.MouseAndKeyboard:
			// 		Debug.LogWarning("ActionMap not set up for m+kb");
			// 		break;
			// 	case SupportedInputDevices.Touchscreen:
			// 		InputSystem.actions.FindActionMap("PlayerMove(Touch)")
			// 			.Enable();
			// 		break;
			// 	default:
			// 		break;
			// }
		}
		OnPerspectiveChange?.Invoke(this);
	}
}