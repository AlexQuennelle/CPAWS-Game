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

	/// <summary>
	///   Event raised when perspective is changed. Passes the Perspective
	///   handler and the current value of <see cref="IsPhotoMode"/>.
	/// </summary>
	public event Action<PlayerPerspectiveHandler, bool> OnPerspectiveChange;

	[SerializeField]
	private PlayerLook _playerLook;

	[SerializeField]
	private List<PhotoModeToggleable> _objectsToToggle;

	public bool IsPhotoMode { get; private set; } = false;
	/// <summary>
	///   <para>
	///     Toggles the player's perspective and handles all logic changes that
	///     need to be dealt with as a result. This includes toggling the active
	///     state of all perspective-specific objects.
	///   </para>
	///   <para>
	///     Also raises the <see cref="OnPerspectiveChange"/> event.
	///   </para>
	/// </summary>
	public void TogglePerspective()
	{
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
		}
		OnPerspectiveChange?.Invoke(this, IsPhotoMode);
	}
}