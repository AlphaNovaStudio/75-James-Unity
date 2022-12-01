using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace FeedContent.Content {
  [Serializable]
  public class ContentGroupData {
    public static ContentGroupData operator+ (ContentGroupData a, ContentGroupData b) {
      NullCheck(a);
      NullCheck(b);

      List<Sprite> sprites = new List<Sprite>();
      List<VideoClip> videoClips = new List<VideoClip>();
      sprites.AddRange(a._sprites);
      sprites.AddRange(b._sprites);
      videoClips.AddRange(a._videoClips);
      videoClips.AddRange(b._videoClips);
      a._sprites = sprites.ToArray();
      a._videoClips = videoClips.ToArray();
      return a;
    }

    private static void NullCheck (ContentGroupData a) {
      a._sprites ??= new Sprite [] {};
      a._videoClips ??= new VideoClip [] {};
    }

    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite [] _sprites;
    [SerializeField]
    private VideoClip [] _videoClips;

    public void Init (ContentStorage storage, int startContentIter) {
      Storage = storage;
      StartContentIter = startContentIter;
    }

    public int FinishContentIter {
      get {
        return StartContentIter + _sprites.Length + _videoClips.Length;
      }
    }

    public ContentStorage Storage { get; private set; }
    public int StartContentIter { get; private set; }
    public string Name {
      get {
        return _name;
      }
    }
    public Sprite [] Sprites {
      get {
        return _sprites;
      }
    }
    public VideoClip [] VideoClips {
      get {
        return _videoClips;
      }
    }
  }
}