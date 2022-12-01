using System;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Suites.Containers {
  public class SuiteWindowContainer : MonoBehaviour {
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _changeSizeButton;
    [SerializeField]
    private RectTransform _controlButtonsRoot;
    [SerializeField]
    private Image _sizeButtonIcon;
    [SerializeField]
    private Sprite _expandSprite;
    [SerializeField]
    private Sprite _collapseSprite;
    [SerializeField]
    private Image _layoutImage;

    [Header("Anchors"), SerializeField]
    private RectTransform _controlInnerAnchor;
    [SerializeField]
    private RectTransform _controlOutAnchor;

    private RectTransform _rectTransform;
    private Vector2 _defaultSize;

    private void Awake() {
      _rectTransform = GetComponent<RectTransform>();
      _defaultSize = _rectTransform.sizeDelta;
    }

    public void SubscribeButtons (Action changeSizeAction, Action closeWindowAction) {
      _closeBtn.AddListener(closeWindowAction.Invoke);
      _changeSizeButton.AddListener(changeSizeAction.Invoke);
    }

    public void ChangeSizeIcon (bool wasExpanded) {
      _sizeButtonIcon.sprite = wasExpanded ? _collapseSprite : _expandSprite;
      _controlButtonsRoot.position = wasExpanded ? _controlInnerAnchor.position : _controlOutAnchor.position;
    }

    public RectTransform RectTransform => _rectTransform;
    public Vector2 DefaultSize => _defaultSize;
    public Image LayoutImage => _layoutImage;
  }
}