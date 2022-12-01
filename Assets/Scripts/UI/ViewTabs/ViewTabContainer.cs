using System;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ViewTabs {
  public class ViewTabContainer : MonoBehaviour {
    [SerializeField]
    private RectTransform _containerRectTransform;
    [SerializeField]
    private Button _tabButton;
    [SerializeField]
    private Image _buttonImage;
    [SerializeField]
    private TMP_Text _tabNameText;

    [Header("Values"), SerializeField, Range(0, 1)]
    private float _nonActiveShownPercent;
    [Range(0, 1), SerializeField]
    private float _nonSelectedShownPercent;

    [Header("Colors"), SerializeField]
    private Color _unSelectedColor;
    [SerializeField]
    private Color _selectedColor;
    [SerializeField]
    private Color _unActiveColor;

    public void SubscribeButtons (Action tabPressAction) {
      _tabButton.AddListener(tabPressAction.Invoke);
    }

    public void InitVisual (Enums.WindowTabType type) {
      _tabNameText.text = type.ToString().ToUpper();
    }

    public void UpdateTabView (bool active, bool selected) {
      UpdateTabPosition(active, selected);
      UpdateTabColor(active, selected);
    }

    private void UpdateTabPosition (bool active, bool selected) {
      float deltaYPosition;

      if (active) {
        deltaYPosition = selected ? 0 : PercentToHeight(_nonSelectedShownPercent);
      } else {
        deltaYPosition = PercentToHeight(_nonActiveShownPercent);
      }

      _containerRectTransform.localPosition = Vector3.up * deltaYPosition;
    }

    private void UpdateTabColor (bool active, bool selected) {
      Color color = _unActiveColor;

      if (active) {
        color = selected ? _selectedColor : _unSelectedColor;
      }

      _buttonImage.color = color;
    }

    private float PercentToHeight (float percent) {
      return _containerRectTransform.rect.height - percent * _containerRectTransform.rect.height;
    }
  }
}