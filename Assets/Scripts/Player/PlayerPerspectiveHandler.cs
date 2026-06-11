using System;

using Unity.Cinemachine;

using UnityEngine;

public class PlayerPerspectiveHandler : MonoBehaviour
{
	public event Action<PlayerPerspectiveHandler> OnPerspectiveChange;

	[SerializeField]
	private PlayerLook _playerLook;

	private bool _isPhotoMode = false;
	public bool IsPhotoMode
	{
		get { return _isPhotoMode; }
		set
		{
			_isPhotoMode = value;
			if (IsPhotoMode)
			{
				_playerLook.MaxVerticalLook = 80f;
				_playerLook.MinVerticalLook = -80f;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				_playerLook.MaxVerticalLook = 70f;
				_playerLook.MinVerticalLook = -55f;
				_playerLook.HandleLook(new Vector2(0, 0));
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			OnPerspectiveChange?.Invoke(this);
		}
	}
}