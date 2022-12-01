using System.Collections.Generic;
using FeedContent.Content;
using PoolLogic;
using TMPro;
using UI.Windows;
using UnityEngine;

namespace FeedContent {
  public class ContentPanel : DialogWindow {
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private RectTransform _content;
    [SerializeField]
    protected ContentStorage _storage;

    private List<ContentGroup> _contentGroups;
    private bool _isInitialized;

    protected override void Init() {
      const float INIT_DELAY = 0.5f;
      Invoke(nameof(GenerateContentGroups), INIT_DELAY);
      base.Init();
    }

    private void GenerateContentGroups() {
      _name.text = _storage.Name;
      _contentGroups = new List<ContentGroup>();
      Pool pool = Pool.Instance;

      foreach (ContentGroupData storageGroupData in _storage.GroupDatas) {
        ContentGroup contentGroup = pool.Get<ContentGroup>();
        contentGroup.transform.SetParent(_content);
        contentGroup.Init(storageGroupData);
        _contentGroups.Add(contentGroup);
      }

      if (_contentGroups.Count == 1) {
        _contentGroups[0].HideName();
      }
    }
  }
}