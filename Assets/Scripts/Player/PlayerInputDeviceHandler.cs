using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDeviceHandler : MonoBehaviour
{
	[SerializeField]
	private PlayerPerspectiveHandler _perspectiveHandler;
	public SupportedInputDevices ActiveInputDevice { get; private set; } =
		SupportedInputDevices.MouseAndKeyboard;
	/// <summary>
	///   <para>
	///     Event raised when the active input device or perspective is changed.
	///     Unlike the events built in to the <see cref="InputSystem"/>, this
	///     event is only fired when the value changes, rather than on all
	///     updates.
	///   </para>
	///   <para>
	///     Passes the current device handler and the
	///     <see cref="SupportedInputDevices"/> enum value of the active device.
	///   </para>
	/// </summary>
	public event Action<PlayerInputDeviceHandler, SupportedInputDevices>
		OnDeviceChanged;
	private bool _deviceChanged = true;

	private void OnEnable()
	{
		_perspectiveHandler.OnPerspectiveChange += HandlePerspectiveChange;
		InputSystem.onActionChange += HandleActionChange;
		InputSystem.actions.Disable();
		switch (ActiveInputDevice)
		{
			case SupportedInputDevices.MouseAndKeyboard:
				InputSystem.actions.FindActionMap("PlayerMove(KB)")
					.Enable();
				break;
			case SupportedInputDevices.Touchscreen:
				InputSystem.actions.FindActionMap("PlayerMove(Touch)")
					.Enable();
				break;
			default:
				break;
		}
	}
	private void OnDisable()
	{
		_perspectiveHandler.OnPerspectiveChange -= HandlePerspectiveChange;
		InputSystem.onActionChange -= HandleActionChange;
	}

	private void SetInputMap()
	{
		_deviceChanged = false;
		InputSystem.actions.Disable();
		switch (ActiveInputDevice)
		{
			case SupportedInputDevices.MouseAndKeyboard
				when _perspectiveHandler.IsPhotoMode:
				InputSystem.actions.FindActionMap("PlayerCamera(KB)")
					.Enable();
				break;
			case SupportedInputDevices.MouseAndKeyboard
				when !_perspectiveHandler.IsPhotoMode:
				InputSystem.actions.FindActionMap("PlayerMove(KB)")
					.Enable();
				break;
			case SupportedInputDevices.Touchscreen
				when _perspectiveHandler.IsPhotoMode:
				OnDeviceChanged?.Invoke(this, ActiveInputDevice);
				InputSystem.actions.FindActionMap("PlayerCamera(Touch)")
					.Enable();
				break;
			case SupportedInputDevices.Touchscreen
				when !_perspectiveHandler.IsPhotoMode:
				InputSystem.actions.FindActionMap("PlayerMove(Touch)")
					.Enable();
				break;
			default:
				break;
		}
	}
	private void HandlePerspectiveChange(
			PlayerPerspectiveHandler handler, bool isCameraMode)
	{
		SetInputMap();
	}
	private void HandleActionChange(object arg1, InputActionChange change)
	{
		bool earlyReturn =
			change != InputActionChange.ActionPerformed
			&& change != InputActionChange.ActionStarted;
		if (earlyReturn) return;

		SupportedInputDevices oldDevice = ActiveInputDevice;
		if (arg1 is InputAction action && action.activeControl != null)
		{
			ActiveInputDevice = action.activeControl.device.displayName switch
			{
				"Mouse" or "Keyboard" => SupportedInputDevices.MouseAndKeyboard,
				"Touchscreen" => SupportedInputDevices.Touchscreen,
				_ => SupportedInputDevices.UnsupportedDevice,
			};
		}
		_deviceChanged = ActiveInputDevice != oldDevice | _deviceChanged;
		if (_deviceChanged)
		{
			OnDeviceChanged?.Invoke(this, ActiveInputDevice);
			SetInputMap();
		}
	}
}

public enum SupportedInputDevices
{
	MouseAndKeyboard,
	Touchscreen,
	UnsupportedDevice,
}