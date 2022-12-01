using System.Collections;
using System.Collections.Generic;
using FeedContent.Content;
using FeedContent.ContentDisplays;
using Other;
using PoolLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FeedContent {
  public class ContentWindow : MonoBehaviour, IDropHandler, IPointerDownHandler {
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private ScrollRect _windowScrollRect;
    [SerializeField]
    private ContentBottomScroll _bottomScroll;
    [SerializeField]
    private Button _bottomButton;
    [SerializeField]
    private HorizontalLayoutGroup _contentLayoutGroup;
    [SerializeField]
    private float _distanceForScroll;

    private List<ContentObjectDisplay> _contentObjectDisplays;
    private int _currentSlide;

    private void Awake() {
      AddListeners();
      gameObject.SetActive(false);
      SubscribeEvents();
    }

    public void OnDrop (PointerEventData eventData) {
      PlayVideoCheck(false);
      float posX = -SlidePos;
      RectTransform content = _windowScrollRect.content;
      float contentX = content.anchoredPosition.x;
      float difference = posX - contentX;

      if (Mathf.Abs(difference) > _distanceForScroll) {
        _currentSlide += difference > 0 ? 1 : -1;
      }

      Scroll();
    }

    public void OnPointerDown (PointerEventData eventData) {
      _bottomScroll.Hide();
    }

    private void SubscribeEvents (bool state = true) {
      if (state) {
        Events.ContentWindowEvents.Show += Show;
        Events.ContentWindowEvents.SetCurrent += SetCurrent;
        return;
      }

      Events.ContentWindowEvents.Show -= Show;
      Events.ContentWindowEvents.SetCurrent -= SetCurrent;
    }

    private void Show (ContentGroupData data, int number) {
      gameObject.SetActive(true);
      _currentSlide = number;
      CreateDisplays(data);
      _contentLayoutGroup.CalculateLayoutInputHorizontal();
      _bottomScroll.Init(data);
      _bottomScroll.Hide();
      StartCoroutine(DelayedScroll());
    }

    private void SetCurrent (int number) {
      _bottomScroll.RefreshHideDelay();
      _currentSlide = number;
      Scroll();
    }

    private IEnumerator DelayedScroll() {
      yield return null;
      Scroll();
    }

    private void AddListeners() {
      _closeButton.AddListener(Close);
      _bottomButton.AddListener(ShowBottomScroll);
    }

    private void Close() {
      foreach (ContentObjectDisplay contentObjectDisplay in _contentObjectDisplays) {
        contentObjectDisplay.Return();
      }

      _bottomScroll.Close();
      Events.ContentWindowEvents.Close?.Invoke();
      gameObject.SetActive(false);
    }

    private void ShowBottomScroll() {
      _bottomScroll.Show();
    }

    private void CreateDisplays (ContentGroupData data) {
      Pool pool = Pool.Instance;
      _contentObjectDisplays = new List<ContentObjectDisplay>();
      int contentIndex = 0;
      Vector2 sizeDelta = ((RectTransform)transform).rect.size;

      foreach (Sprite sprite in data.Sprites) {
        ImageDisplay imageContent = pool.Get<ImageDisplay>();
        imageContent.transform.SetParent(Content);
        imageContent.Init(sprite, sizeDelta);
        _contentObjectDisplays.Add(imageContent);
        contentIndex++;
      }

      foreach (VideoClip dataVideoClip in data.VideoClips) {
        VideoDisplay videoContent = pool.Get<VideoDisplay>();
        videoContent.transform.SetParent(Content);
        videoContent.Init(dataVideoClip, contentIndex == _currentSlide, sizeDelta);
        _contentObjectDisplays.Add(videoContent);
        contentIndex++;
      }
    }

    private void PlayVideoCheck (bool isShow) {
      if (_contentObjectDisplays[_currentSlide] is not VideoDisplay videoDisplay) {
        return;
      }

      if (isShow) {
        videoDisplay.PlayVideo();
      } else {
        videoDisplay.StopVideo();
      }
    }

    private void Scroll() {
      Content.localPosition = new Vector3(-SlidePos, Content.localPosition.y);
      PlayVideoCheck(true);
    }

    private RectTransform Content {
      get {
        return _windowScrollRect.content;
      }
    }

    private float SlidePos {
      get {
        return CurrentSlide.transform.localPosition.x;
      }
    }

    private ContentObjectDisplay CurrentSlide {
      get {
        return _contentObjectDisplays[_currentSlide];
      }
    }
  }
}