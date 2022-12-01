using UnityEngine;

namespace FeedContent.Content {
  [CreateAssetMenu(fileName = "ContentStorage", menuName = "Settings/ContentStorage")]
  public class ContentStorage : ScriptableObject {
    [SerializeField]
    private string _name;
    [SerializeField]
    private ContentGroupData [] _groupDatas;
    [SerializeField]
    private bool _isNotGroupableOnDisplay;

    public void Init() {
      InitializeGroups();
      GroupContentData();
    }

    private void InitializeGroups() {
      for (int i = 0; i < _groupDatas.Length; i++) {
        int pastFinishIter = i == 0 ? 0 : _groupDatas[i - 1].FinishContentIter;
        _groupDatas[i].Init(this, pastFinishIter);
      }
    }

    private void GroupContentData() {
      AllContent = new ContentGroupData();
      AllContent.Init(this, 0);

      foreach (ContentGroupData contentGroupData in _groupDatas) {
        AllContent += contentGroupData;
      }
    }

    public ContentGroupData [] GroupDatas {
      get {
        return _groupDatas;
      }
    }

    public ContentGroupData AllContent { get; private set; }

    public string Name {
      get {
        return _name;
      }
    }

    public bool IsNotGroupableOnDisplay {
      get {
        return _isNotGroupableOnDisplay;
      }
    }
  }
}