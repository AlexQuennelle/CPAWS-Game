using UnityEngine;

using System.Collections.Generic;
using System;

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

	/// <summary>
	///   <para>
	///     Renders a picture from the camera and calculates the score for that
	///     picture.
	///   </para>
	///   <para>
	///     Score is calculated by accumulating the individual score values
	///     contributed by any <see cref="CameraTarget"> objects registered as
	///     "in frame" by the camera frustum.
	///   </para>
	/// </summary>
	public PictureInfo TakePicture()
	{
		float score = 0.0f;
		List<TargetInfo> subjects = new();
		foreach (CameraTarget target in _targets)
		{
			int? targetScore = target.GetScore(_cam, transform);
			if (targetScore != null)
			{
				score += (int)targetScore;
				subjects.Add(target.Info);
			}
		}
		// Convoluted process to convert a RenderTexture to a Texture2D
		// without bungling the texture format.
		_cam.Render();
		Texture2D pic = new(_texture.width, _texture.height);
		pic.Reinitialize(pic.width, pic.height, _texture.graphicsFormat, true);
		RenderTexture.active = _texture;
		pic.ReadPixels(new Rect(0, 0, _texture.width, _texture.height), 0, 0);
		pic.Apply();
		return new(pic, (int)score, subjects);
	}

	/// <summary>
	///   Reconstructs the camera frustum collider. Call when changing
	///   properties on the camera.
	/// </summary>
	private void RecalculateSensor()
	{
		if (_cam != null)
		{
			int resX = _cam.scaledPixelWidth;
			int resY = _cam.scaledPixelHeight;
			Vector3 offset = _cam.transform.parent.position;
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

/// <summary>
///   Class containing relevant info to a given picture.
/// </summary>
[Serializable]
public class PictureInfo
{
	public PictureInfo(Texture2D tex, int score, List<TargetInfo> subjects)
	{
		Tex = tex;
		Score = score;
		Subjects = subjects;
	}
	[field: SerializeField]
	public Texture2D Tex { get; private set; }
	[field: SerializeField]
	public int Score { get; private set; }
	[field: SerializeField]
	public List<TargetInfo> Subjects { get; private set; }
}