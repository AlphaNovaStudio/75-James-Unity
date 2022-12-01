using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
  public class DragBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    private const float SCREEN_OFFSET_PERCENT = 0.99f;

    [SerializeField]
    private Transform _movableObject;

    private Canvas _canvas;
    private PointerEventData _lastPointerData;
    private Vector3 _mouseOffset;
    private bool _canDrag;

    private void Awake() {
      if (!_movableObject) {
        _movableObject = transform;
      }

      _canvas = GetComponentInParent<Canvas>();
      _canDrag = true;
    }

    public void DisableDrag() { _canDrag = false; }

    public void EnableDrag() { _canDrag = true; }

    public void OnPointerDown (PointerEventData eventData) {
      _movableObject.SetSiblingIndex(_movableObject.parent.childCount - 1);
      _mouseOffset = _movableObject.position - GetMousePosition(eventData.position);
      Events.CameraEvents.SetCameraControlType?.Invoke(Enums.CameraControlType.None);
    }

    public void OnPointerUp (PointerEventData eventData) { Events.CameraEvents.SetCameraControlType?.Invoke(Enums.CameraControlType.Full); }

    public void OnDrag (PointerEventData eventData) {
      if (!_canDrag) return;

      _lastPointerData = eventData;
      _movableObject.position = GetMousePosition(_lastPointerData.position) + _mouseOffset;
    }

    private Vector3 GetMousePosition (Vector2 pos) {
      RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, pos, _canvas.worldCamera, out Vector2 position);
      return ClampPosition(position);
    }

    private Vector2 ClampPosition (Vector2 position) {
      Vector2 screenBorders = new Vector2(Screen.width, Screen.height);
      Vector2 delta = screenBorders - screenBorders * SCREEN_OFFSET_PERCENT;

      position = _canvas.transform.TransformPoint(position);
      position.x = Mathf.Clamp(position.x, delta.x, screenBorders.x - delta.x);
      position.y = Mathf.Clamp(position.y, delta.y, screenBorders.y - delta.y);

      return position;
    }
  }
}
