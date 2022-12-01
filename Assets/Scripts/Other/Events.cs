using System;
using FeedContent.Content;
using UI.Suites;

namespace Other {
  public static class Events {
    public static class CameraEvents {
      public static Action<Enums.CameraControlType> SetCameraControlType;
      public static Action<bool> OnRotate;
    }

    public static class SuiteCardsEvents {
      public static Action<SuiteCard, bool> OnFavouriteStatusChange;
      public static Action<bool> OnKeeperChanged;
    }

    public static class ContentWindowEvents {
      public static Action<ContentGroupData, int> Show;
      public static Action<int> SetCurrent;
      public static Action Close;
    }
  }
}