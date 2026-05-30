using UnityEngine;

using System;

public class TestCameraSensor : MonoBehaviour
{
	public event Action<TestCameraSensor, GameObject> OnTargetEntered;

	void OnTriggerEnter(Collider other)
	{
		OnTargetEntered?.Invoke(this, other.gameObject);
	}
}