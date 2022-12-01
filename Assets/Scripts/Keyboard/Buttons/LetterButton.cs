using TMPro;
using UnityEngine;

namespace Keyboard.Buttons {
  public class LetterButton : KeyboardButton {
    [SerializeField]
    private TextMeshProUGUI _containerText;

    protected override void OnClick() {
      InputKeyboardController.AddLetter?.Invoke(_containerText.text);
    }
  }
}