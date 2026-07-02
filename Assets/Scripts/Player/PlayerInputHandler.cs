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
	private PlayerLook _playerLook;
	[SerializeField]
	private PlayerPerspectiveHandler _playerPerspectiveHandler;
	[SerializeField]
	private PlayerMovement _playerMove;
	[SerializeField]
	private RectTransform _cameraTouchRect;
	[SerializeField]
	private LayerMask _groundMask;
	[SerializeField]

	private CameraSensor _cameraSensor;
	private bool _isMoving = false;

	private void OnEnable()
	{
		InputSystem.actions.FindAction("TouchDrag").performed += OnDrag;

		InputSystem.actions.FindAction("TouchMove").performed += OnTouch;
		InputSystem.actions.FindAction("TouchMove").canceled += OnTouch;

		InputSystem.actions.FindAction("Look").performed += OnLook;
		InputSystem.actions.FindAction("Exit").performed += ExitCamera;
		InputSystem.actions.FindAction("Move").performed += OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled += OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed
			+= HandleTakePicture;
	}
	private void OnDisable()
	{
		InputSystem.actions.FindAction("Look").performed -= OnLook;
		InputSystem.actions.FindAction("Exit").performed -= ExitCamera;
		InputSystem.actions.FindAction("Move").performed -= OnMoveStart;
		InputSystem.actions.FindAction("Move").canceled -= OnMoveEnd;
		InputSystem.actions.FindAction("TakePicture").performed
			-= HandleTakePicture;
	}
	private void Update()
	{
		if (_isMoving)
		{
			Vector3 screenPoint =
				InputSystem.actions.FindAction("TouchPos").ReadValue<Vector2>();
			Ray targetRay = Camera.main.ScreenPointToRay(screenPoint);
			bool isHit =
				Physics.Raycast(
						targetRay, out RaycastHit hit, float.MaxValue, _groundMask);
			if (isHit)
			{
				_playerMove.MoveTo(hit.point);
			}
		}
	}

	private void OnTouch(InputAction.CallbackContext ctx)
	{
		_isMoving = ctx.performed;
	}
	private void OnDrag(InputAction.CallbackContext ctx)
	{
		RectTransform rectParent =
			_cameraTouchRect.parent.transform as RectTransform;
		float mult = ((RectTransform)_cameraTouchRect.root).rect.width
			/ Camera.main.pixelWidth;
		Vector2 screenPoint =
			(InputSystem.actions.FindAction("TouchPos").ReadValue<Vector2>()
			* mult) + rectParent.rect.position;
		if (_cameraTouchRect.rect.Contains(screenPoint))
		{
			_playerLook.HandleLook(ctx.ReadValue<Vector2>() * _sensitivity);
		}
	}
	private void OnLook(InputAction.CallbackContext ctx)
	{
		Vector2 lookDelta = ctx.ReadValue<Vector2>() * _sensitivity;
		_playerLook.HandleLook(lookDelta);
	}

	private void ExitCamera(InputAction.CallbackContext ctx)
	{
		_playerPerspectiveHandler.TogglePerspective();
	}
	private void OnMoveStart(InputAction.CallbackContext ctx)
	{
		var input = ctx.ReadValue<Vector2>();
		var cameraYaw = Camera.main.transform.eulerAngles.y;
		Vector3 moveDirection =
			Quaternion.Euler(0, cameraYaw, 0)
			* new Vector3(input.x, 0f, input.y);
		_playerMove.SetDirection(moveDirection.normalized);
	}
	private void OnMoveEnd(InputAction.CallbackContext ctx)
	{
		_playerMove.StopPlayer();
	}
	private void HandleTakePicture(InputAction.CallbackContext ctx)
	{
		if (!_playerPerspectiveHandler.IsPhotoMode) return;

		_cameraSensor.TakePicture();
	}
}