using FeedContent.Content;
using Other;
using PoolLogic;
using UnityEngine;
using UnityEngine.UI;

namespace FeedContent.ContentObjects {
  public abstract class ContentObject : MonoBehaviour, IPoolable {
    [SerializeField]
    private Button _button;

    private ContentGroupData _groupData;
    private int _number;

    public void Get() {
      gameObject.SetActive(true);
    }

    public void Return() {
      gameObject.SetActive(false);
      Pool.Instance.Return(this);
    }

    protected void Init (ContentGroupData data, int number, bool isOnWindow) {
      _button.AddListener(isOnWindow ? SetCurrent : ShowContent);
      _groupData = data;
      _number = number;
    }

    private void ShowContent() {
      Events.ContentWindowEvents.Show?.Invoke(_groupData, _number);
    }

    private void SetCurrent() {
      Events.ContentWindowEvents.SetCurrent?.Invoke(_number);
    }
  }
}