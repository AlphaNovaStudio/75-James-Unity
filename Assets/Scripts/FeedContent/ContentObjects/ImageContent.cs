using FeedContent.Content;
using UnityEngine;
using UnityEngine.UI;

namespace FeedContent.ContentObjects {
  public class ImageContent : ContentObject {
    [SerializeField]
    private Image _image;

    public void Init (Sprite sprite, ContentGroupData data, int number, bool isOnWindow) {
      float widthToHeightRatio = sprite.rect.width / sprite.rect.height;

      ((RectTransform)transform).localScale = Vector2.one;
      Init(data, number, isOnWindow);

      _image.sprite = sprite;
      _image.rectTransform.sizeDelta *= new Vector2(widthToHeightRatio, 1);
    }
  }
}