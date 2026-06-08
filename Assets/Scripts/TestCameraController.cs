using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraController : MonoBehaviour
{
	[SerializeField]
	private Transform _head;
	[SerializeField]
	private CameraSensor _cam;

	private InputAction _lookAction;

	void Start()
	{
		_lookAction = InputSystem.actions.FindAction("Look");
		Cursor.lockState = CursorLockMode.Locked;
		InputSystem.actions.FindAction("Attack").performed += OnAttack;
	}

	void Update()
	{
		Vector2 lookValue = _lookAction.ReadValue<Vector2>();
		transform.Rotate(new(0.0f, lookValue.x / 20.0f, 0.0f));
		_head.Rotate(new(-lookValue.y / 20.0f, 0.0f, 0.0f));
	}

	private void OnAttack(InputAction.CallbackContext context)
	{
		if (_cam == null)
			return;

		PictureInfo pic = _cam.TakePicture();
		Debug.Log($"Picture worth {pic.Score} taken!");
	}
}