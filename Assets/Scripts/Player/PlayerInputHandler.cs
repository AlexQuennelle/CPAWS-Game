using System;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	[Header("Player Stats")]
	[SerializeField, Range(0.2f, 1f)]
	private float _sensitivity = 0.5f;

	[Header("Controller variables")]
	[SerializeField]
	private PlayerLook _playerLook;
	[SerializeField]
	private PlayerJoystickMove _playerJoystickMove;
	[SerializeField]
	private PlayerTouchMove _playerTouchMove;
	[SerializeField]
	private LayerMask _groundMask;
	[SerializeField]
	private PlayerPerspectiveHandler _playerPerspectiveHandler;
	[SerializeField]
	private CameraSensor _cameraSensor;

	private Vector3 _hitPos;

	private void OnEnable()
	{
		InputSystem.actions.FindActionMap("Player").Disable();
		InputSystem.actions.FindActionMap("PlayerCamera(Touch)").Disable();
		InputSystem.actions.FindAction("TouchDrag").performed += OnDrag;

		InputSystem.actions.FindActionMap("PlayerMove(Touch)").Enable();
		InputSystem.actions.FindAction("TouchMove").performed += OnTouch;

		InputSystem.actions.FindAction("Look").performed += OnLook;
		InputSystem.actions.FindAction("ChangeCamera").performed
			+= OnChangeCamera;
		InputSystem.actions.FindAction("Move").performed += OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled += OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed
			+= HandleTakePicture;
	}

	private void OnTouch(InputAction.CallbackContext ctx)
	{
		Debug.Log(ctx.ReadValue<Vector2>());
		Ray targetRay =
			Camera.main.ScreenPointToRay((Vector3)ctx.ReadValue<Vector2>());
		bool isHit =
			Physics.Raycast(
					targetRay, out RaycastHit hit, float.MaxValue, _groundMask);
		if (isHit)
		{
			_hitPos = hit.point;
			_playerTouchMove.MovePlayer(hit.point);
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(_hitPos, 0.1f);
	}
	private void OnDrag(InputAction.CallbackContext ctx)
	{
		Debug.Log(ctx.ReadValue<Vector2>());
	}

	private void OnDisable()
	{
		InputSystem.actions.FindAction("Look").performed -= OnLook;
		InputSystem.actions.FindAction("ChangeCamera").performed
			-= OnChangeCamera;
		InputSystem.actions.FindAction("Move").performed -= OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled -= OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed
			-= HandleTakePicture;
	}

	private void OnLook(InputAction.CallbackContext ctx)
	{
		if (!_playerPerspectiveHandler.IsPhotoMode) return;

		Vector2 lookDelta = ctx.ReadValue<Vector2>() * _sensitivity;
		_playerLook.HandleLook(lookDelta);
	}

	private void OnChangeCamera(InputAction.CallbackContext ctx)
	{
		_playerPerspectiveHandler.IsPhotoMode =
			!_playerPerspectiveHandler.IsPhotoMode;

		_playerJoystickMove.StopMove();
	}

	private void OnMoveStart(InputAction.CallbackContext ctx)
	{
		if (_playerPerspectiveHandler.IsPhotoMode) return;

		_playerJoystickMove.HandleMove(ctx.ReadValue<Vector2>());
	}

	private void OnMoveEnd(InputAction.CallbackContext ctx)
	{
		_playerJoystickMove.StopMove();
	}

	private void HandleTakePicture(InputAction.CallbackContext ctx)
	{
		if (!_playerPerspectiveHandler.IsPhotoMode) return;

		_cameraSensor.TakePicture();
	}
}