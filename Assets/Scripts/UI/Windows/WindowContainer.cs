using System;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows {
  public class WindowContainer : MonoBehaviour {
    [SerializeField]
    private Button _closeButton;

    public void SubscribeButtons (Action closeAction) {
      _closeButton.AddListener(closeAction.Invoke);
    }
  }
}