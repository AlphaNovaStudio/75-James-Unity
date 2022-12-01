using Other;
using UnityEngine;
using UnityEngine.UI;

namespace FeedContent.ContentDisplays {
  internal class ImageDisplay : ContentObjectDisplay {
    [SerializeField]
    private Image _image;

    public void Init (Sprite sprite, Vector2 sizeDelta) {
      Init(sizeDelta);
      _image.rectTransform.sizeDelta = ((RectTransform)transform).sizeDelta;
      ((RectTransform)transform).localScale = Vector2.one;
      _image.sprite = sprite;
      _image.rectTransform.sizeDelta = Utils.DisplaySizeDivider(_image.rectTransform.rect.size, sprite.rect.size);
    }
  }
}