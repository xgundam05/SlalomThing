using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>Dead Simple Object Pool</summary>
public class Pool<T> where T : class, new() {
	private Queue<T> inactive;
	private List<T> all;

	public Pool() : this(50){
		// Nuthin
	}

	public Pool(int initialCap){
		inactive = new Queue<T>(initialCap);
		all = new List<T>(initialCap);

		for (int i = 0; i < initialCap; i++){
			T tmp = new T();
			inactive.Enqueue(tmp);
			all.Add(tmp);
		}
	}

	public T Request(){
		if (inactive.Count > 0)
			return inactive.Dequeue();
		else {
			T tmp = new T();
			all.Add(tmp);
			return tmp;
		}
	}

	public void Return(T item){
		inactive.Enqueue(item);
	}
}
