using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PrefabPoolManager : MonoBehaviour {
  // Value stuffs
  private const int DEFAULT_CAPACITY = 10;

  private Dictionary<string, object> _pools = new Dictionary<string, object>();
  private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

  // Instance Member
  private static PrefabPoolManager _current;

  public static PrefabPoolManager current {
    get {
      if (_current == null){
        _current = GameObject.FindObjectOfType<PrefabPoolManager>();

        DontDestroyOnLoad(_current.gameObject);
      }

      return _current;
    }
  }

  // Use this for initialization
  void Awake () {
    if (_current == null){
      // We are the first! We must has the precious!
      _current = this;
      DontDestroyOnLoad(this);
    }
    else {
      if (this != _current)
        Destroy(this.gameObject);
    }
  }

  // Creates a Pool of Prefabs
  public Pool<GameObject> CreatePool(string key, GameObject prefab){
    if (this._prefabs.ContainsKey(key))
      return this._pools[key] as Pool<GameObject>;

    this._prefabs.Add(key, prefab);

    // Create a new pool
    // - We call the Instantiate method to deal with the prefab-edness
    Pool<GameObject> pool = new Pool<GameObject>(
      DEFAULT_CAPACITY,
      () => {
        GameObject tmp = Instantiate(
          prefab,
          Vector3.zero,
          Quaternion.identity
        ) as GameObject;
        tmp.SetActive(false);
        return tmp;
      }
    );

    this._pools.Add(key, pool);

    return pool;
  }

  public Pool<GameObject> GetPool(string key){
    if (this._pools.ContainsKey(key))
      return this._pools[key] as Pool<GameObject>;
    else
      return null; // Throw error instead?
  }
}
