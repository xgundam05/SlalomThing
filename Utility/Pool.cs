using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>Dead Simple Object Pool</summary>
public class Pool<T> where T : class, new() {
	private Queue<T> _inactive;
	private List<T> _all;
	private Func<T> _generator;

	public Pool() : this(50)
	{
	}

	public Pool(int initialCap)
		: this(initialCap, () => { return new T(); })
	{
	}

	public Pool(int initialCap, Func<T> generator){
		_inactive = new Queue<T>(initialCap);
		_all = new List<T>(initialCap);
		_generator = generator;

		for (int i = 0; i < initialCap; i++){
			T tmp = _generator();
			_inactive.Enqueue(tmp);
			_all.Add(tmp);
		}
	}

	public T Request(){
		if (_inactive.Count > 0)
			return _inactive.Dequeue();
		else {
			T tmp = _generator();
			_all.Add(tmp);
			return tmp;
		}
	}

	public void Return(T item){
		_inactive.Enqueue(item);
	}
}
