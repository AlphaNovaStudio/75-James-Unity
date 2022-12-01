using System.Collections;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Controls {
  [RequireComponent(typeof(CanvasGroup))]
  public class Compass : MonoBehaviour {
    private const float HIDE_DELAY = 2f;

    [SerializeField]
    private GameObject _compassVisual;
    [SerializeField]
    private RawImage _compassImage;
    [SerializeField]
    private Transform _player;

    [SerializeField, Range(0, 1f)]
    private float _hiddenAlpha;
    [SerializeField]
    private bool _hiddenCompletely;

    private CanvasGroup _canvasGroup;
    private Coroutine _hideDelayRoutine;

    private float _normalAlpha;
    private bool _activeSelf = true;

    private void Awake() {
      Init();
      SubscribeEvents(true);
    }

    public void OnDestroy() {
      SubscribeEvents(false);
    }

    public void SetActive(bool value) {
      _activeSelf = value;
      if (_hiddenCompletely) {
        _compassVisual.SetActive(value);
        return;
      }

      float canvasGroupAlpha = value ? _normalAlpha : _hiddenAlpha;

      _canvasGroup.alpha = canvasGroupAlpha;
    }

    private void Init() {
      if (_hiddenCompletely) {
        _hideDelayRoutine = StartCoroutine(HideDelayRoutine());
      }

      _canvasGroup = GetComponent<CanvasGroup>();
      _normalAlpha = _canvasGroup.alpha;
    }

    private void SetValue(bool isMove) {
      _compassImage.uvRect = new Rect(_player.localEulerAngles.y / 360, 0, 1, 1);

      if (!_hiddenCompletely) {
        return;
      }

      if (_activeSelf == false) {
        SetActive(true);
      }

      if (isMove) {
        return;
      }

      StopCoroutine(_hideDelayRoutine);
      _hideDelayRoutine = StartCoroutine(HideDelayRoutine());
    }

    private IEnumerator HideDelayRoutine() {
      yield return new WaitForSeconds(HIDE_DELAY);
      SetActive(false);
    }

    private void SubscribeEvents(bool value) {
      if (value) {
        Events.CameraEvents.OnRotate += SetValue;
        return;
      }

      Events.CameraEvents.OnRotate -= SetValue;
    }
  }
}