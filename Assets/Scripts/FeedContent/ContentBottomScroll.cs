using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FeedContent {
  public class ContentBottomScroll : ContentGroup, IBeginDragHandler, IEndDragHandler {
    private const float HIDE_DELAY = 3;

    private Coroutine _hideDelayRoutine;

    private void OnDestroy() {
      StopDelayRoutine();
    }

    public void OnBeginDrag (PointerEventData eventData) {
      StopDelayRoutine();
    }

    public void OnEndDrag (PointerEventData eventData) {
      RefreshHideDelay();
    }

    public void Close() {
      StopDelayRoutine();
      ReturnContentObjects();
      gameObject.SetActive(false);
    }

    public void Hide() {
      gameObject.SetActive(false);
      StopDelayRoutine();
    }

    public void Show() {
      gameObject.SetActive(true);
      RefreshHideDelay();
    }

    public void RefreshHideDelay() {
      StopDelayRoutine();
      _hideDelayRoutine = StartCoroutine(HideDelayRoutine());
    }

    private void StopDelayRoutine() {
      if (_hideDelayRoutine != null) {
        StopCoroutine(_hideDelayRoutine);
      }
    }

    private IEnumerator HideDelayRoutine() {
      yield return new WaitForSeconds(HIDE_DELAY);
      gameObject.SetActive(false);
    }
  }
}