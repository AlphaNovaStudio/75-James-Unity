using UnityEngine;
using UnityEngine.EventSystems;

namespace Other {
  public static class Utils {

    public static bool IsPointerOverGameObject {
      get {
        if (Input.touchCount > 0) {
          return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
        }

        return EventSystem.current.IsPointerOverGameObject();
      }
    }

    public static string GetParsedPriceByThousands (int price) {
      string numsAfterDot = (price % 1000).ToString();
      int digitsAfterDot = 3;
      int missingNums = digitsAfterDot - numsAfterDot.Length;

      for (int i = 0; i < missingNums; i++) {
        numsAfterDot = '0' + numsAfterDot;
      }

      return price / 1000 + "," + numsAfterDot;
    }

    public static Vector2 GetRandomPositionInSquare (Vector2 borderSquare, Vector2 objectSize, float screenAreaPercent) {
      Vector2 borders = (borderSquare - objectSize) * 0.5f * screenAreaPercent;

      return new Vector2(Random.Range(-borders.x, borders.x), Random.Range(-borders.y, borders.y));
    }

    public static Vector2 DisplaySizeDivider (Vector2 displaySize, Vector2 contentSize) {
      Vector2 result;
      float widthFactor = displaySize.x / contentSize.x;
      float heightFactor = displaySize.y / contentSize.y;
      float spriteWidthToHeightRatio = contentSize.x / contentSize.y;

      if (widthFactor < heightFactor) {
        result = new Vector2(displaySize.x, displaySize.x / spriteWidthToHeightRatio);
      } else {
        result = new Vector2(displaySize.y * spriteWidthToHeightRatio, displaySize.y);
      }

      return result;
    }
  }
}