using System.Collections.Generic;
using UnityEngine;

namespace Abstract {
  public class BaseMultiFactory<T> : MonoBehaviour where T : Object {
    [SerializeField]
    private T [] _prefabs;

    private List<T> _spawnedObjects = new List<T>();

    public T [] GetNewInstances (Vector3 position) {
      T [] spawnedObjects = new T[_prefabs.Length];

      for (int i = 0; i < _prefabs.Length; i++) {
        T newObj = Instantiate(_prefabs[i], position, Quaternion.identity);
        spawnedObjects[i] = newObj;
        CacheObject(newObj);
      }

      return spawnedObjects;
    }

    public T [] GetNewInstances (Transform parent) {
      T [] spawnedObjects = new T[_prefabs.Length];

      for (int i = 0; i < _prefabs.Length; i++) {
        T newObj = Instantiate(_prefabs[i], parent);
        spawnedObjects[i] = newObj;
        CacheObject(newObj);
      }

      return spawnedObjects;
    }

    public T [] GetNewInstances (Transform parent, Vector3 position) {
      T [] spawnedObjects = new T[_prefabs.Length];

      for (int i = 0; i < _prefabs.Length; i++) {
        T newObj = Instantiate(_prefabs[i], position, Quaternion.identity, parent);
        spawnedObjects[i] = newObj;
        CacheObject(newObj);
      }

      return spawnedObjects;
    }

    private void CacheObject (T obj) {
      _spawnedObjects.Add(obj);
    }

    public List<T> SpawnedObjects => _spawnedObjects;
  }
}