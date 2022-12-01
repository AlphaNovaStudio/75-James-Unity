using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Compass = Controls.Compass;

namespace DayTime {
  public class DayTimeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private event Action<bool> OnPointerInteraction;

    [SerializeField]
    private Light _dirLight;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Gradient _directionLightGradient;
    [SerializeField]
    private Gradient _ambientLightGradient;
    [SerializeField]
    private ChangingHandle _changingHandle;
    [SerializeField]
    private Compass _compass;
    [SerializeField]
    private GameObject _stars;
    [SerializeField]
    private Vector2 _timeShowStars;

    private Vector3 _defaultAngle;

    private void Awake() {
      _slider.value = _slider.maxValue * 0.5f;
      ShowStars(_slider.value);
      _defaultAngle = _dirLight.transform.localEulerAngles;
      _changingHandle.Init();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    public void OnPointerDown(PointerEventData eventData) {
      OnPointerInteraction?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData) {
      OnPointerInteraction?.Invoke(false);
    }

    private void AddListeners() {
      _slider.onValueChanged.AddListener(OnChanged);
      OnPointerInteraction += _changingHandle.OnInteraction;
      if (_compass != null) {
        OnPointerInteraction += HideCompass;
      }
    }

    private void HideCompass(bool value) {
      _compass.SetActive(!value);
    }

    private void RemoveListeners() {
      _slider.onValueChanged.RemoveListener(OnChanged);
      OnPointerInteraction -= _changingHandle.OnInteraction;
      if (_compass != null) {
        OnPointerInteraction -= HideCompass;
      }
    }

    private void OnChanged(float value) {
      _dirLight.color = _directionLightGradient.Evaluate(value);
      RenderSettings.ambientLight = _ambientLightGradient.Evaluate(value);
      _dirLight.transform.localEulerAngles = new Vector3(360f * value - 90, _defaultAngle.y, _defaultAngle.z);
      ShowStars(value);
    }

    private void ShowStars(float value) {
      bool showStars = value < _timeShowStars.x || value > _timeShowStars.y;
      if (_stars.activeSelf != showStars) {
        _stars.SetActive(showStars);
      }
    }
  }
}