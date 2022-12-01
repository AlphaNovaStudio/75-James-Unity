using System;
using UnityEngine;
using UnityEngine.UI;

namespace DayTime {
  [Serializable]
  public class ChangingHandle {
    [SerializeField, Range(0, 1)]
    private float _selectedAlpha;
    [SerializeField]
    private float _selectedScale;
    [SerializeField]
    private Image _image;

    private float _normalAlpha;

    public void Init() {
      _normalAlpha = _image.color.a;
    }

    public void OnInteraction(bool isTake) {
      _image.color = new Color(1, 1, 1, isTake ? _selectedAlpha : _normalAlpha);
      _image.transform.localScale = isTake ? Vector3.one * _selectedScale : Vector3.one;
    }
  }
}