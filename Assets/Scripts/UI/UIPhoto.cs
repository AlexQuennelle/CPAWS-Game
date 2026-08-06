using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPhoto : MonoBehaviour, IPointerClickHandler
{
	public event Action<UIPhoto> OnPhotoClick; 

	[SerializeField]
	private RawImage _rawImage;

	[field: SerializeField]
	public Image Highlight { get; private set; }

	private PictureInfo _pictureInfo;
	public PictureInfo PictureInfo
	{
		get
		{
			return _pictureInfo;
		}
		set
		{
			_pictureInfo = value;
			_rawImage.texture = _pictureInfo.Tex;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnPhotoClick?.Invoke(this);
	}
}
