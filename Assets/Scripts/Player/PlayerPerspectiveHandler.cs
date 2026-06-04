using Unity.Cinemachine;

using UnityEngine;

public class PlayerPerspectiveHandler : MonoBehaviour
{
	[SerializeField]
	private CinemachineCamera _firstPersonVCam;
	[SerializeField]
	private CinemachineCamera _thirdPersonVCam;

	[SerializeField]
	private PlayerLook _playerLook;

	private bool _isFirstPerson = true;
	public bool IsFirstPerson
	{
		get { return _isFirstPerson; }
		set
		{
			_isFirstPerson = value;
			if (IsFirstPerson)
			{
				_firstPersonVCam.Priority = 1000;
				_thirdPersonVCam.Priority = 0;
				_playerLook.MaxVerticalLook = 80f;
				_playerLook.MinVerticalLook = -80f;
			}
			else
			{
				_firstPersonVCam.Priority = 0;
				_thirdPersonVCam.Priority = 1000;
				_playerLook.MaxVerticalLook = 70f;
				_playerLook.MinVerticalLook = -55f;
				_playerLook.HandleLook(new Vector2(0, 0));
			}
		}
	}
}