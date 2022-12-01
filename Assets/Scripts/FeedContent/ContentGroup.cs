using System.Collections.Generic;
using FeedContent.Content;
using FeedContent.ContentObjects;
using PoolLogic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace FeedContent {
  public class ContentGroup : MonoBehaviour, IPoolable {
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private GameObject _nameBack;
    [SerializeField]
    private Transform _contentParent;
    [SerializeField]
    private bool _isOnWindow;

    private List<ContentObject> _content;

    public void Get() {
      gameObject.SetActive(true);
    }

    public void Return() {
      ReturnContentObjects();
      gameObject.SetActive(false);
      SetNameActive(true);
      Pool.Instance.Return(this);
    }

    public void HideName() {
      SetNameActive(false);
    }

    public void Init (ContentGroupData data) {
      ((RectTransform)transform).localScale = Vector2.one;

      if (_name != null) {
        _name.text = data.Name;
      }

      Pool pool = Pool.Instance;
      _content = new List<ContentObject>();
      int contentIndex = data.Storage.IsNotGroupableOnDisplay ? data.StartContentIter : 0;
      ContentGroupData dataForDisplay = data.Storage.IsNotGroupableOnDisplay ? data.Storage.AllContent : data;

      foreach (Sprite sprite in data.Sprites) {
        ImageContent imageContent = pool.Get<ImageContent>();
        imageContent.transform.SetParent(_contentParent);
        imageContent.Init(sprite, dataForDisplay, contentIndex, _isOnWindow);
        _content.Add(imageContent);
        contentIndex++;
      }

      foreach (VideoClip dataVideoClip in data.VideoClips) {
        VideoContent videoContent = pool.Get<VideoContent>();
        videoContent.transform.SetParent(_contentParent);
        videoContent.Init(dataVideoClip, dataForDisplay, contentIndex, _isOnWindow);
        _content.Add(videoContent);
        contentIndex++;
      }
    }

    protected void ReturnContentObjects() {
      foreach (ContentObject contentObject in _content) {
        contentObject.Return();
      }
    }

    private void SetNameActive (bool value) {
      _name.gameObject.SetActive(value);
      _nameBack.SetActive(value);
    }
  }
}