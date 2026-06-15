using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	[Header("Player Stats")]
	[SerializeField, Range(0.2f, 1f)]
	private float _sensitivity = 0.5f;

	[Header("Controller variables")]
	[SerializeField]
	private PlayerInput _playerInput;
	[SerializeField]
	private PlayerLook _playerLook;
	[SerializeField]
	private PlayerJoystickMove _playerMove;
	[SerializeField]
	private PlayerPerspectiveHandler _playerPerspectiveHandler;
	[SerializeField]
	private CameraSensor _cameraSensor;

	private void OnEnable()
	{
		InputSystem.actions.FindAction("Look").performed += OnLook;
		InputSystem.actions.FindAction("ChangeCamera").performed += OnChangeCamera;
		InputSystem.actions.FindAction("Move").performed += OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled += OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed += HandleTakePicture;
	}

	private void OnDisable()
	{
		InputSystem.actions.FindAction("Look").performed -= OnLook;
		InputSystem.actions.FindAction("ChangeCamera").performed -= OnChangeCamera;
		InputSystem.actions.FindAction("Move").performed -= OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled -= OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed -= HandleTakePicture;
	}

	private void OnLook(InputAction.CallbackContext ctx)
	{
		if (!_playerPerspectiveHandler.IsPhotoMode) return;

		Vector2 lookDelta = ctx.ReadValue<Vector2>() * _sensitivity;
		_playerLook.HandleLook(lookDelta);
	}

	private void OnChangeCamera(InputAction.CallbackContext ctx)
	{
		_playerPerspectiveHandler.IsPhotoMode = !_playerPerspectiveHandler.IsPhotoMode;

		_playerMove.StopMove();
	}

	private void OnMoveStart(InputAction.CallbackContext ctx)
	{
		if (_playerPerspectiveHandler.IsPhotoMode) return;

		_playerMove.HandleMove(ctx.ReadValue<Vector2>());
	}

	private void OnMoveEnd(InputAction.CallbackContext ctx)
	{
		_playerMove.StopMove();
	}

	private void HandleTakePicture(InputAction.CallbackContext ctx)
	{
		if (!_playerPerspectiveHandler.IsPhotoMode) return;

		_cameraSensor.TakePicture();
	}
}