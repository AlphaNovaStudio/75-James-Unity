using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolLogic {
  public class Pool : MonoBehaviour {
    public static Pool Instance;

    private readonly Dictionary<Type, ObjectPool<IPoolable>> _poolMap = new Dictionary<Type, ObjectPool<IPoolable>>();

    private void Awake() {
      if (Instance != null) {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      Initialize();
    }

    public T Get<T>() where T : IPoolable {
      Type type = typeof(T);
      return (T)_poolMap[type].Get();
    }

    public void Return<T> (T item) where T : IPoolable {
      Type type = typeof(T);

      if (_poolMap.ContainsKey(type) == false) {
        return;
      }

      _poolMap[type].Return(item);
    }

    private void Initialize() {
      LoadPrefabs();
    }

    private void LoadPrefabs() {
      const string PATH = "Poolable";
      MonoBehaviour [] prefabs = Resources.LoadAll<MonoBehaviour>(PATH);

      foreach (MonoBehaviour prefab in prefabs) {
        CreatePool(prefab);
      }

      Resources.UnloadUnusedAssets();
    }

    private void CreatePool<T> (T item) where T : MonoBehaviour {
      var pool = new ObjectPool<IPoolable>(() => (IPoolable)Instantiate(item));
      _poolMap.Add(item.GetType(), pool);
    }
  }
}