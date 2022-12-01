using UnityEngine;
using UnityEngine.UI;

namespace Keyboard.Buttons {
  [RequireComponent(typeof(Button))]
  public abstract class KeyboardButton : MonoBehaviour {
    private Button _button;

    private void Awake() {
      _button = GetComponent<Button>();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    protected abstract void OnClick();

    private void AddListeners() {
      _button.onClick.AddListener(OnClick);
    }

    private void RemoveListeners() {
      _button.onClick.RemoveListener(OnClick);
    }
  }
}