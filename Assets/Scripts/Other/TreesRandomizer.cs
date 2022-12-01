using UnityEngine;

namespace Other {
  public class TreesRandomizer : MonoBehaviour {
    [SerializeField]
    private Vector2 _normalSizeRange = new Vector2(0.33f,0.58f);
    [SerializeField]
    private Vector2 _smallSizeRange = new Vector2(0.21f,0.32f);
    [SerializeField]
    private bool _isSmall;

    [ContextMenu(nameof(Randomize))]
    public void Randomize() {
      Vector3 rotation = new Vector3(RandomTill, Random.Range(-180, 180), RandomTill);
      transform.localScale = Vector3.one * RandomSizeValue;
      transform.rotation = Quaternion.Euler(rotation);
    }

    private float RandomTill {
      get {
        const float TILT_CHANGE = 3f;
        return Random.Range(-TILT_CHANGE, TILT_CHANGE);
      }
    }

    private float RandomSizeValue {
      get {
        Vector2 currentSize = _isSmall ? _smallSizeRange : _normalSizeRange;
        return Random.Range(currentSize.x, currentSize.y);
      }
    }
  }
}