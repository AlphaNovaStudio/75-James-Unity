using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Keyboard {
  [RequireComponent(typeof(TMP_InputField))]
  public class InputKeyboardController : MonoBehaviour {
    public static Action<string> AddLetter;
    public static Action DeleteLetter;
    public static Action<bool> CapsLock;

    private TMP_InputField _inputField;
    private BaseEventData _baseEventData;
    private Coroutine _onEndSelectionRoutine;

    private Vector2Int _selectedText;

    private void Awake() {
      _baseEventData = new BaseEventData(EventSystem.current);
      _inputField = GetComponent<TMP_InputField>();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _inputField.onTextSelection.AddListener(OnTextSelection);
      _inputField.onEndTextSelection.AddListener(OnEndTextSelection);
      AddLetter += OnAddLetter;
      DeleteLetter += OnDeleteLetter;
    }

    private void OnEndTextSelection (string arg0, int arg1, int arg2) {
      _onEndSelectionRoutine = StartCoroutine(OnEndSelectionRoutine());
    }

    private IEnumerator OnEndSelectionRoutine() {
      yield return new WaitForSeconds(0.1f);
      _selectedText = Vector2Int.zero;
    }

    private void RemoveListeners() {
      AddLetter -= OnAddLetter;
      DeleteLetter -= OnDeleteLetter;
    }

    private void OnDeleteLetter() {
      if (_inputField.text.Length == 0) {
        return;
      }

      if (_selectedText == Vector2Int.zero) {
        if (_inputField.caretPosition == 0) {
          return;
        }

        int caretPosition = _inputField.caretPosition - 1;
        _inputField.text = _inputField.text.Remove(caretPosition, 1);
        StartCoroutine(SetCaret(caretPosition));
        return;
      }

      _inputField.text = _inputField.text.Remove(_selectedText.x, _selectedText.y - _selectedText.x);
      StartCoroutine(SetCaret(_selectedText.x));
      _selectedText = Vector2Int.zero;
      StopEndSelectionDelay();
    }

    private void OnTextSelection (string arg0, int arg1, int arg2) {
      StopEndSelectionDelay();

      _selectedText = arg1 < arg2 ? new Vector2Int(arg1, arg2) : new Vector2Int(arg2, arg1);
    }

    private void StopEndSelectionDelay() {
      if (_onEndSelectionRoutine != null) {
        StopCoroutine(_onEndSelectionRoutine);
      }
    }

    private void OnAddLetter (string letter) {
      int caretPosition = _inputField.caretPosition;
      _inputField.text = _inputField.text.Insert(caretPosition, letter);
      StartCoroutine(SetCaret(caretPosition + letter.Length));
    }

    private IEnumerator SetCaret (int pos) {
      yield return null;
      _inputField.caretPosition = pos;
      _inputField.OnSelect(_baseEventData);
    }
  }
}