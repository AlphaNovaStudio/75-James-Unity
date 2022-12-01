using Other;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FeedContent.ContentDisplays {
  public class VideoDisplay : ContentObjectDisplay {
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private VideoPlayer _videoPlayer;

    private void OnDestroy() {
      _videoPlayer.frameDropped -= StopVideo;
    }

    public void PlayVideo() {
      _videoPlayer.Play();
    }

    public void Init (VideoClip clip, bool isCurrent, Vector2 sizeDelta) {
      Init(sizeDelta);
      _image.rectTransform.sizeDelta = ((RectTransform)transform).sizeDelta;
      Vector2 contentSize = new Vector2(clip.width, clip.height);
      ((RectTransform)transform).localScale = Vector2.one;
      Vector2 rectTransformSizeDelta = Utils.DisplaySizeDivider(_image.rectTransform.sizeDelta, contentSize);

      RenderTexture renderTexture = new RenderTexture((int)rectTransformSizeDelta.x, (int)rectTransformSizeDelta.y, 16, RenderTextureFormat.ARGB32);
      renderTexture.Create();

      _image.rectTransform.sizeDelta = rectTransformSizeDelta;
      _image.texture = renderTexture;
      _videoPlayer.clip = clip;
      _videoPlayer.targetTexture = renderTexture;

      if (isCurrent) {
        return;
      }

      _videoPlayer.frameDropped += StopVideo;
      PlayVideo();
    }

    public void StopVideo() {
      _videoPlayer.Stop();
    }

    private void StopVideo (VideoPlayer source) {
      _videoPlayer.frameDropped -= StopVideo;
      StopVideo();
    }
  }
}