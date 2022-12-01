using System;
using PoolLogic;
using UnityEngine;

namespace FeedContent.ContentDisplays {
  public abstract class ContentObjectDisplay : MonoBehaviour, IPoolable {
    protected void Init(Vector2 sizeDelta) {
      RectTransform rectTransform = (RectTransform) transform;
      rectTransform.sizeDelta = sizeDelta;
    }

    public void Get() {
      gameObject.SetActive(true);
    }

    public void Return() {
      gameObject.SetActive(false);
      Pool.Instance.Return(this);
    }
  }
}