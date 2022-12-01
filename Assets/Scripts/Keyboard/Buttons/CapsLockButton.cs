using UnityEngine;

namespace Keyboard.Buttons {
  public class CapsLockButton : KeyboardButton {
    [SerializeField]
    private bool _isSmallLetters;
    
    protected override void OnClick() {
      InputKeyboardController.CapsLock?.Invoke(_isSmallLetters);
    }
  }
}