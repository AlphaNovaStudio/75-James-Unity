using System;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Suites.Containers {
  public class SuiteCardContainer : MonoBehaviour {
    [SerializeField]
    private TMP_Text _priceText;
    [SerializeField]
    private TMP_Text _suiteTitleText;
    [SerializeField]
    private TMP_Text _suiteSubTitleText;
    [SerializeField]
    private Button _openLayoutButton;
    [SerializeField]
    private Button _subTitleButton;
    [SerializeField]
    private Toggle _favouriteToggle;
    [SerializeField]
    private RectTransform _backgroundRectTransform;
    [SerializeField]
    private RectTransform _containerRectTransform;

    public void SubscribeButtons (Action openLayoutAction, Action<bool> favouriteToggleChange, Action changeViewModeAction) {
      _openLayoutButton.AddListener(openLayoutAction.Invoke);
      _subTitleButton.AddListener(changeViewModeAction.Invoke);
      _favouriteToggle.AddListener(favouriteToggleChange.Invoke);
    }

    public void SetupData (string price, string title, string subTitle) {
      _priceText.text = price;
      _suiteTitleText.text = title;
      _suiteSubTitleText.text = subTitle;
    }

    public void SetLayoutButtonInteractable (bool state) {
      _openLayoutButton.interactable = state;
    }

    public RectTransform BackgroundRectTransform => _backgroundRectTransform;
    public RectTransform ContainerRectTransform => _containerRectTransform;
  }
}