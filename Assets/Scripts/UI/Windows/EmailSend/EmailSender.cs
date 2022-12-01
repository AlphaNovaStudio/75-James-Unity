using System;
using System.Collections;
using System.Net;
using System.Net.Mail;
using Keyboard;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if !PLATFORM_ANDROID
using System.ComponentModel.DataAnnotations;
#endif

namespace UI.Windows.EmailSend {
  public class EmailSender : MonoBehaviour {
    private const string NO_INTERNET_ERROR = "No internet connections. To use this feature, you need an internet connection";
    private const string CLIENT_HOST = "smtp.gmail.com";
    private const int CLIENT_PORT = 587;

    [SerializeField]
    private TMP_InputField _input;
    [SerializeField]
    private DialogWindow _window;
    [SerializeField]
    private Toggle _consentToggle;
    [SerializeField]
    private KeyboardController _keyboard;
    [SerializeField]
    private ErrorWindow _errorWindow;
    [SerializeField]
    private MailboxData _sender;
    [SerializeField]
    private float _hideAlpha;

    private CanvasGroup _canvasGroup;

    private void Awake() {
      _canvasGroup = GetComponent<CanvasGroup>();
      AddListeners();
      OnToggleChanged(false);
    }

    private void OnEnable() {
      OnInputChanged();
    }

    private void AddListeners() {
      _keyboard.SendButton.AddListener(Send);
      _keyboard.CancelButton.AddListener(CloseWindow);
      _input.AddListener(OnInputChanged);
      _consentToggle.AddListener(OnToggleChanged);
    }

    private void CloseWindow() {
      _window.Close();
    }

    private void OnToggleChanged (bool value) {
      _canvasGroup.alpha = value ? 1 : _hideAlpha;
      _canvasGroup.interactable = value;
    }

    private void OnInputChanged (string arg0 = "") {
      _keyboard.SendButton.interactable = IsEmailValid() && _consentToggle.isOn;
    }

    private bool IsEmailValid() {
#if !PLATFORM_ANDROID
      var emailAddressAttribute = new EmailAddressAttribute();
      return emailAddressAttribute.IsValid(_input.text);
#else
      return false;
#endif
    }

    private void Send() {
      StartCoroutine(SendRoutine());
    }

    private IEnumerator SendRoutine() {
      if (Application.internetReachability == NetworkReachability.NotReachable) {
        SendError(NO_INTERNET_ERROR);
        yield break;
      }

      SmtpClient client = SetSender();
      MailMessage message = MessageDataHandler.GetMessage(_sender.Name, _input.text);

      try {
        client.Send(message);
      } catch (Exception exception) {
        SendError(exception.Message);
      }

      _window.Close();
    }

    private void SendError (string exceptions) {
      _errorWindow.ShowExceptions(exceptions);
    }

    private SmtpClient SetSender() {
      var client = new SmtpClient {
        Host = CLIENT_HOST,
        Port = CLIENT_PORT,
        Credentials = new NetworkCredential(_sender.Name, _sender.Password),
        EnableSsl = true
      };

      ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
      return client;
    }

    [Serializable]
    private struct MailboxData {
      public string Name;
      public string Password;
    }
  }
}