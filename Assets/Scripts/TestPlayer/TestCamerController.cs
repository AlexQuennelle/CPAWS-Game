using UnityEngine;
using UnityEngine.InputSystem;

public class TestCamerController : MonoBehaviour
{
	[SerializeField]
	private Camera _cam;

	private InputAction _lookAction;

	void Start()
	{
		_lookAction = InputSystem.actions.FindAction("Look");
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		Vector2 lookValue = _lookAction.ReadValue<Vector2>();
		transform.Rotate(new(0.0f, lookValue.x / 20.0f, 0.0f));
		_cam.transform.Rotate(new(-lookValue.y / 20.0f, 0.0f, 0.0f));
	}
}