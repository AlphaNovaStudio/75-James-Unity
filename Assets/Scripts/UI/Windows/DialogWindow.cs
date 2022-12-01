using System;
using Interfaces;
using Other;
using UnityEngine;

namespace UI.Windows {
  public class DialogWindow : MonoBehaviour, IWindow {
    [SerializeField]
    private Enums.WindowTabType _type;
    [SerializeField]
    private WindowContainer _windowContainer;

    protected Action _onCloseAction;

    private void Awake() {
      Init();
    }

    public void Close() {
      IsActivated = false;
      gameObject.SetActive(false);
    }

    public void Expand() {}

    public void Collapse() {}

    public void ChangeWindowSize() {}

    public void SetupWindow (Action onCloseAction) {
      _onCloseAction = onCloseAction;
    }

    public virtual void Show() {
      gameObject.SetActive(true);
      IsActivated = true;
    }

    protected virtual void Init() {
      Close();
      _windowContainer?.SubscribeButtons(ClosingWithAction);
    }

    protected void ClosingWithAction() {
      Close();
      _onCloseAction?.Invoke();
    }

    public bool IsExpanded { get; set; }
    public bool IsActivated { get; set; }
    public Enums.WindowTabType Type {
      get {
        return _type;
      }
    }
  }
}