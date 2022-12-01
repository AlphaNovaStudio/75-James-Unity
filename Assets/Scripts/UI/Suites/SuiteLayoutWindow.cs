using System;
using Interfaces;
using Other;
using UI.Suites.Containers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Suites {
  public class SuiteLayoutWindow : MonoBehaviour, IWindow, IPointerDownHandler {
    private const float SCREEN_PLACE_PERCENT = 0.4f;

    [SerializeField]
    private SuiteWindowContainer _suiteWindowContainer;

    private Action _onCloseAction;
    private DragBehaviour _dragBehaviour;
    private RectTransform _canvas;

    private void OnEnable() { IsActivated = true; }

    public void OnPointerDown (PointerEventData eventData) {
      MoveUpInHierarchy();
    }

    public void Close() {
      IsActivated = false;
      Collapse();
      _onCloseAction?.Invoke();
      gameObject.SetActive(false);
    }

    public void ChangeWindowSize() {
      if (IsExpanded) {
        Collapse();
      } else {
        Expand();
      }
    }

    public void Expand() {
      IsExpanded = true;
      _dragBehaviour.DisableDrag();
      _suiteWindowContainer.ChangeSizeIcon(true);
      _suiteWindowContainer.RectTransform.sizeDelta = _canvas.sizeDelta;
      _suiteWindowContainer.RectTransform.localPosition = Vector3.zero;
    }

    public void Collapse() {
      IsExpanded = false;
      _dragBehaviour.EnableDrag();
      _suiteWindowContainer.ChangeSizeIcon(false);
      _suiteWindowContainer.RectTransform.sizeDelta = _suiteWindowContainer.DefaultSize;

      _suiteWindowContainer.RectTransform.localPosition = Utils.GetRandomPositionInSquare(_canvas.sizeDelta, _suiteWindowContainer.RectTransform.sizeDelta, SCREEN_PLACE_PERCENT);
    }

    public void Init (RectTransform canvas) {
      _canvas = canvas;
      InitComponents();
      SubscribeButtons();
      IsExpanded = false;
    }

    public void OnOpen() {
      gameObject.SetActive(true);
      Collapse();
      MoveUpInHierarchy();
    }

    public void SetupWindow (Action onCloseAction, Sprite layoutSprite = null) {
      _onCloseAction = onCloseAction;
      _suiteWindowContainer.LayoutImage.sprite = layoutSprite;
    }

    private void MoveUpInHierarchy() {
      transform.SetSiblingIndex(transform.parent.childCount - 1);
    }

    private void InitComponents() {
      _dragBehaviour = GetComponentInChildren<DragBehaviour>();
    }

    private void SubscribeButtons() {
      _suiteWindowContainer.SubscribeButtons(ChangeWindowSize, Close);
    }

    public bool IsExpanded { get; set; }
    public bool IsActivated { get; set; }
  }
}