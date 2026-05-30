using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraController : MonoBehaviour
{
	[SerializeField]
	private Camera _cam;

	[SerializeField]
	private TestCameraSensor _sensor;

	[SerializeField]
	private float _nearPlane = 0.3f;
	[SerializeField]
	private float _farPlane = 50.0f;

	[SerializeField]
	private MeshCollider _frustumCol;
	private Mesh _frustumMesh;

	private InputAction _lookAction;

	void Start()
	{
		if (_sensor != null)
			_sensor.OnTargetEntered += OnTargetEnteredSensor;

		_lookAction = InputSystem.actions.FindAction("Look");
		Cursor.lockState = CursorLockMode.Locked;
		if (_cam != null)
		{
			int resX = _cam.scaledPixelWidth;
			int resY = _cam.scaledPixelHeight;
			Vector3 offset = _cam.transform.localPosition;
			Vector3[] frustumVerts = {
				_cam.ScreenToWorldPoint(new(0.0f, 0.0f, _nearPlane)) - offset,
				_cam.ScreenToWorldPoint(new(0.0f, resY, _nearPlane)) - offset,
				_cam.ScreenToWorldPoint(new(resX, resY, _nearPlane)) - offset,
				_cam.ScreenToWorldPoint(new(resX, 0.0f, _nearPlane)) - offset,

				_cam.ScreenToWorldPoint(new(0.0f, 0.0f, _farPlane))  - offset,
				_cam.ScreenToWorldPoint(new(0.0f, resY, _farPlane))  - offset,
				_cam.ScreenToWorldPoint(new(resX, resY, _farPlane))  - offset,
				_cam.ScreenToWorldPoint(new(resX, 0.0f, _farPlane))  - offset,
			};
			int[] frustumTris = {
				0, 1, 2,  0, 2, 3, // Front face
				4, 5, 1,  4, 1, 0, // Left face
				7, 6, 5,  4, 7, 5, // Back face
				3, 2, 6,  3, 6, 7, // Right face
				1, 5, 6,  1, 6, 2, // Top face
				4, 0, 3,  4, 3, 7  // Bottom face
			};
			_frustumMesh = new Mesh
			{
				vertices = frustumVerts,
				triangles = frustumTris,
			};
			_frustumMesh.RecalculateNormals();
			_frustumCol.sharedMesh = _frustumMesh;
		}
	}

	void Update()
	{
		Vector2 lookValue = _lookAction.ReadValue<Vector2>();
		transform.Rotate(new(0.0f, lookValue.x / 20.0f, 0.0f));
		_cam.transform.Rotate(new(-lookValue.y / 20.0f, 0.0f, 0.0f));
	}

	void OnTargetEnteredSensor(TestCameraSensor sensor, GameObject target)
	{
		// TODO: Handle data
		Debug.Log(target.name);
	}
}