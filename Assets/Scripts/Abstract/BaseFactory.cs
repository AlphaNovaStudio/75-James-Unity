using System.Collections.Generic;
using UnityEngine;

namespace Abstract {
  public class BaseFactory<T> : MonoBehaviour where T : Object {
    [SerializeField]
    private T _prefab;

    private List<T> _spawnedObjects = new List<T>();

    public T GetNewInstance (Vector3 position) {
      T newObj = Instantiate(_prefab, position, Quaternion.identity);
      CacheObject(newObj);
      return newObj;
    }

    public T GetNewInstance (Transform parent) {
      T newObj = Instantiate(_prefab, parent);
      CacheObject(newObj);
      return newObj;
    }

    public T GetNewInstance (Transform parent, Vector3 position) {
      T newObj = Instantiate(_prefab, position, Quaternion.identity, parent);
      CacheObject(newObj);
      return newObj;
    }

    private void CacheObject (T obj) {
      _spawnedObjects.Add(obj);
    }

    public List<T> SpawnedObjects => _spawnedObjects;
  }
}