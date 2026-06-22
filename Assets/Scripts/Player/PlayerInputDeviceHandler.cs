using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDeviceHandler : MonoBehaviour
{
	public SupportedInputDevices ActiveInputDevice { get; private set; } =
		SupportedInputDevices.MouseAndKeyboard;

	private void OnEnable()
	{
		InputSystem.onActionChange += OnActionChange;
	}
	private void OnDisable()
	{
		InputSystem.onActionChange -= OnActionChange;
	}

	private void OnActionChange(object arg1, InputActionChange change)
	{
		bool earlyReturn =
			change != InputActionChange.ActionPerformed
			&& change != InputActionChange.ActionStarted;
		if (earlyReturn) return;

		if (arg1 is InputAction action)
		{
			ActiveInputDevice = action.activeControl.device.displayName switch
			{
				"Mouse" or "Keyboard" => SupportedInputDevices.MouseAndKeyboard,
				"Touchscreen" => SupportedInputDevices.Touchscreen,
				_ => SupportedInputDevices.UnsupportedDevice,
			};
		}
	}
}

public enum SupportedInputDevices
{
	MouseAndKeyboard,
	Touchscreen,
	UnsupportedDevice,
}