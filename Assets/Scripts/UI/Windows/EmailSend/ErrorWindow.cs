using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.EmailSend {
  public class ErrorWindow : MonoBehaviour {
    [SerializeField]
    private Button _okButton;
    [SerializeField]
    private TMP_Text _exceptionsText;

    private void Awake() {
      _okButton.onClick.AddListener(Close);
    }

    public void ShowExceptions(string exceptions) {
      _exceptionsText.text = exceptions;
      gameObject.SetActive(true);
    }

    private void Close() {
      gameObject.SetActive(false);
    }
  }
}