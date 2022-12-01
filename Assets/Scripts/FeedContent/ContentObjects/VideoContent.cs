using FeedContent.Content;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FeedContent.ContentObjects {
  public class VideoContent : ContentObject {
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private VideoPlayer _videoPlayer;

    private bool _isInitialized;

    private void OnEnable() {
      if (_isInitialized) {
        return;
      }

      _videoPlayer.frameDropped += StopVideo;
      _videoPlayer.Play();
    }

    private void OnDestroy() {
      _videoPlayer.frameDropped -= StopVideo;
    }

    public void Init (VideoClip clip, ContentGroupData data, int number, bool isOnWindow) {
      RenderTexture renderTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);

      ((RectTransform)transform).localScale = Vector2.one;
      Init(data, number, isOnWindow);
      renderTexture.Create();

      _image.texture = renderTexture;
      _videoPlayer.clip = clip;
      _videoPlayer.targetTexture = renderTexture;
    }

    private void StopVideo (VideoPlayer source) {
      _videoPlayer.frameDropped -= StopVideo;
      _videoPlayer.Stop();
      _isInitialized = true;
    }
  }
}