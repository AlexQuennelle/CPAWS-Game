using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
	[SerializeField]
	private Transform _playerPitch;
	[SerializeField]
	private Transform _playerYaw;

	private float XRot { get; set; }
	private float YRot { get; set; }

	public float MaxVerticalLook { get; set; } = 70f;
	public float MinVerticalLook { get; set; } = -55f;

	private void OnEnable()
	{
		XRot = 0f;
		YRot = 0f;
		_playerYaw.rotation = Quaternion.Euler(Vector3.zero);
		_playerPitch.localRotation = Quaternion.Euler(Vector3.zero);
	}

	public void HandleLook(Vector2 lookDelta)
	{
		YRot += lookDelta.x;
		XRot -= lookDelta.y;

		XRot = Mathf.Clamp(XRot, MinVerticalLook, MaxVerticalLook);

		// player horizontal rotation
		_playerYaw.rotation = Quaternion.AngleAxis(YRot, Vector3.up);
		// camera vertical rotation
		_playerPitch.localRotation = Quaternion.AngleAxis(XRot, Vector3.right);
	}
}