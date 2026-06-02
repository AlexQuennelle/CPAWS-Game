using UnityEngine;

using System.Collections.Generic;

public class CameraSensor : MonoBehaviour
{
	[SerializeField]
	private Camera _cam;

	[SerializeField]
	private float _nearPlane = 0.3f;
	[SerializeField]
	private float _farPlane = 50.0f;

	[SerializeField]
	private MeshCollider _frustumCol;
	private Mesh _frustumMesh;

	[SerializeField]
	private RenderTexture _texture;

	// [SerializeField]
	private List<CameraTarget> _targets = new();

	private void Start()
	{
		RecalculateSensor();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<CameraTarget>() != null)
		{
			_targets.Add(other.gameObject.GetComponent<CameraTarget>());
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<CameraTarget>() != null)
		{
			_targets.Remove(other.gameObject.GetComponent<CameraTarget>());
		}
	}

	public void TakePicture()
	{
		Debug.Log("Taking Picture");
	}

	private void RecalculateSensor()
	{
		if (_cam != null)
		{
			int resX = _cam.scaledPixelWidth;
			int resY = _cam.scaledPixelHeight;
			Vector3 offset = _cam.transform.parent.localPosition;
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
}