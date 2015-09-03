using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	public float speed = 0f;
	public float sideV = 0f;
	public float maxSideV = 10f;
	public float sideAcc = 8f;

	private Vector3 _right = new Vector3(1, 0, 0);

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		float steering = Input.GetAxis("Horizontal");

		if (steering != 0){
			this.sideV = Mathf.Lerp(this.sideV,
			                        this.maxSideV * Mathf.Sign(steering),
			                        Time.deltaTime * this.sideAcc);
		}
		else {
			this.sideV = Mathf.Lerp(this.sideV, 0, Time.deltaTime * this.sideAcc);
		}

		this.sideV = Mathf.Clamp(this.sideV, -this.maxSideV, this.maxSideV);
		this.transform.Translate(_right * this.sideV * Time.deltaTime);
	}
}
