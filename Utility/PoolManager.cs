using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {
	// Constants
	private const int DEFAULT_CAPACITY = 100;

	// Pool table -- Terrible Pun, but I couldn't resist >.>
	private Dictionary<Type, object> _pools;

	// Instance Member
	private static PoolManager _current;

	public static PoolManager current {
		get {
			if (_current == null){
				_current = GameObject.FindObjectOfType<PoolManager>();

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

	public Pool<T> Pools<T>() where T : class, new() {
		if (this._pools == null)
			this._pools = new Dictionary<Type, object>();

		Type key = typeof(T);
		if (!this._pools.ContainsKey(key)){
			Pool<T> tmp = new Pool<T>(DEFAULT_CAPACITY);
			this._pools.Add(key, tmp);
		}

		return this._pools[key] as Pool<T>;
	}
}
