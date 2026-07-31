using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class DisplayEndOfDayPhotos : MonoBehaviour
{
	public event Action<DisplayEndOfDayPhotos, PictureInfo[]> OnSelectionComplete;

	[SerializeField]
	private UIPhoto _photoPrefab;

	[SerializeField]
	private Button _doneButton;

	private List<UIPhoto> _createdPhotos = new();

	private HashSet<UIPhoto> _submittedPhotos = new();

	public void OnEnable()
	{
		_doneButton.interactable = false;

		_doneButton.onClick.AddListener(() => OnSelectionComplete?.Invoke(this, _submittedPhotos.Take(3).Select(p => p.PictureInfo).ToArray()));
	}

	private void OnDisable()
	{
		_doneButton.onClick.RemoveAllListeners();

		foreach (var photo in _createdPhotos)
		{
			photo.OnPhotoClick -= HandlePhotoClick;
			Destroy(photo);
		}
	}

	public void DisplayPhotos(List<PictureInfo> pictures)
	{
		foreach (PictureInfo p in pictures)
		{
			UIPhoto newphoto = Instantiate(_photoPrefab, transform);
			_createdPhotos.Add(newphoto);
			newphoto.PictureInfo = p;
			newphoto.OnPhotoClick += HandlePhotoClick;
		}
	}

	private void HandlePhotoClick(UIPhoto photo)
	{
		_submittedPhotos.Add(photo);

		if(_submittedPhotos.Count > 3)
		{
			_submittedPhotos = _submittedPhotos.TakeLast(3).ToHashSet();
		}

		if (_submittedPhotos.Count == 3) _doneButton.interactable = true;
		else _doneButton.interactable = false;

		foreach(var p in _createdPhotos)
		{
			p.Highlight.enabled = false;
		}

		foreach(var p in _submittedPhotos)
		{
			p.Highlight.enabled = true;
		}
	}
}
