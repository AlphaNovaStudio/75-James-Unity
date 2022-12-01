using FeedContent.Content;
using UnityEngine;

namespace TemporaryArchitecture {
  public class StoragesInitializer : MonoBehaviour {
    [SerializeField]
    private ContentStorage [] _storages;

    private void Awake() {
      foreach (ContentStorage contentStorage in _storages) {
        contentStorage.Init();
      }
    }
  }
}