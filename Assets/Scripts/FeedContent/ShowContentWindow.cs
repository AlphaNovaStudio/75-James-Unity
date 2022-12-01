using FeedContent.Content;
using Other;
using UI.Windows;
using UnityEngine;

namespace FeedContent {
  internal class ShowContentWindow : DialogWindow {
    [SerializeField]
    protected ContentStorage _storage;

    private void OnDestroy() {
      UnsubscribeOnClose();
    }

    public override void Show() {
      Events.ContentWindowEvents.Show?.Invoke(_storage.GroupDatas[0], 0);
      IsActivated = true;
      Events.ContentWindowEvents.Close += OnClose;
    }

    protected override void Init() {
      Close();
    }

    private void OnClose() {
      UnsubscribeOnClose();
      ClosingWithAction();
    }

    private void UnsubscribeOnClose() { Events.ContentWindowEvents.Close -= OnClose; }
  }
}