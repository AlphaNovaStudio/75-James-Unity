using UnityEngine;
using UnityEngine.UI;

namespace Keyboard {
  public class KeyboardController : MonoBehaviour {
    [SerializeField]
    private GameObject[] _smallRows;
    [SerializeField]
    private GameObject[] _capitalRows;
    [SerializeField]
    private Button _cancelButton;
    [SerializeField]
    private Button _sendButton;

    private void Awake() {
      InputKeyboardController.CapsLock += SetCapsLock;
    }

    private void OnDestroy() {
      InputKeyboardController.CapsLock -= SetCapsLock;
    }

    private void SetCapsLock(bool isSetCapital) {
      SetSmallRowsActive(!isSetCapital);
      SetCapitalRowsActive(isSetCapital);
    }

    private void SetSmallRowsActive(bool value) {
      foreach (GameObject smallRow in _smallRows) {
        smallRow.SetActive(value);
      }
    }

    private void SetCapitalRowsActive(bool value) {
      foreach (GameObject capitalRow in _capitalRows) {
        capitalRow.SetActive(value);
      }
    }

    public Button CancelButton {
      get {
        return _cancelButton;
      }
    }

    public Button SendButton {
      get {
        return _sendButton;
      }
    }
  }
}