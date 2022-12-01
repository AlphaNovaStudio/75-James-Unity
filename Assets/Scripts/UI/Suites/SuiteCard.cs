using System;
using Other;
using UI.Suites.Containers;
using UnityEngine;

namespace UI.Suites {
  public class SuiteCard : MonoBehaviour {
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private SuiteLayoutWindow _layoutWindowPrefab;
    [SerializeField]
    private SuiteCardContainer _suiteCardContainer;
    [SerializeField]
    private SuitePropertyBlock [] _suiteProperties;

    [Header("Data"), SerializeField]
    private SuiteData _suiteData;

    private SuiteLayoutWindow _suiteLayoutWindow;
    private RectTransform _canvas;
    private bool _isCollapsed;
    private bool _isCanChangeView;

    private void OnDestroy() {
      SubscribeEvents(false);
    }

    public void Init (RectTransform canvas, Transform layoutParent) {
      SubscribeButtons();
      SubscribeEvents(true);
      _canvas = canvas;
      _suiteLayoutWindow = SpawnLayoutWindow(layoutParent);
    }

    public void SetupInfo() {
      string price = Utils.GetParsedPriceByThousands(_suiteData.Price);
      string title = Constants.Names.SUITE_BASE_NAME + " " + _suiteData.SuiteNumber;
      string subTitle = _suiteData.SubTitle;

      _suiteCardContainer.SetupData(price, title, subTitle);
      _suiteLayoutWindow.SetupWindow(OnLayoutClose);

      for (int i = 0; i < _suiteProperties.Length; i++) {
        _suiteProperties[i].SetupBlock(_suiteData.SuitePropertyDatas[i]);
      }
    }

    private void SubscribeEvents (bool value) {
      if (value) {
        Events.CameraEvents.OnRotate += OnCameraRotate;
        return;
      }

      Events.CameraEvents.OnRotate -= OnCameraRotate;
    }

    private void OnCameraRotate (bool obj) {
      if (!_isCollapsed) {
        ChangeViewMode();
      }
    }

    private void SubscribeButtons() {
      _suiteCardContainer.SubscribeButtons(OpenLayout, FavouriteToggleChange, ChangeViewMode);
    }

    private SuiteLayoutWindow SpawnLayoutWindow (Transform parent) {
      SuiteLayoutWindow window = Instantiate(_layoutWindowPrefab, parent);
      window.Init(_canvas);
      window.Close();
      window.transform.localPosition = Vector3.zero;
      return window;
    }

    private void OpenLayout() {
      _suiteLayoutWindow.OnOpen();
      _suiteCardContainer.SetLayoutButtonInteractable(false);
    }

    private void ChangeViewMode() {
      if (!_isCanChangeView) {
        return;
      }

      Vector3 pos = _isCollapsed ? Vector3.zero : Vector3.left * _suiteCardContainer.BackgroundRectTransform.rect.width;
      _suiteCardContainer.ContainerRectTransform.localPosition = pos;
      _isCollapsed = !_isCollapsed;
    }

    private void FavouriteToggleChange (bool state) {
      Events.SuiteCardsEvents.OnFavouriteStatusChange?.Invoke(this, state);
      _isCanChangeView = state;
    }

    private void OnLayoutClose() {
      _suiteCardContainer.SetLayoutButtonInteractable(true);
    }

    public SuiteData GetData {
      get {
        return _suiteData;
      }
    }
    public RectTransform RectTransform {
      get {
        return _rectTransform;
      }
    }
  }

  [Serializable]
  public class SuiteData {
    [SerializeField]
    private string _subTitle;
    [SerializeField]
    private int _price;
    [SerializeField]
    private int _suiteNumber;
    [SerializeField]
    private SuitePropertyData [] _suitePropertyDatas;

    public string SubTitle {
      get {
        return _subTitle;
      }
    }
    public int Price {
      get {
        return _price;
      }
    }
    public int SuiteNumber {
      get {
        return _suiteNumber;
      }
    }
    public SuitePropertyData [] SuitePropertyDatas {
      get {
        return _suitePropertyDatas;
      }
    }
  }
}