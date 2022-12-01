namespace Keyboard.Buttons {
  public class DeleteLetterButton : KeyboardButton {
    protected override void OnClick() {
      InputKeyboardController.DeleteLetter?.Invoke();
    }
  }
}