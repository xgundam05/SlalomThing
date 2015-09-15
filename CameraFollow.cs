using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
  public GameObject target;
  public float FollowDist = 5.0f;
  public float Speed = 1.0f;
  public float Height = 2.0f;

  private Vector3 _look = new Vector3(0,0,0);

  // Use this for initialization
  void Start () {
    InitLook();
  }

  // Update is called once per frame
  void Update () {
    if (target != null){
      float _x = Mathf.Lerp(this.transform.position.x,
                            target.transform.position.x,
                            this.Speed * 0.1f);

      this._look.x = this.transform.position.x;
      this.transform.LookAt(this._look);
      this.transform.Translate(Vector3.right * (_x - this._look.x));

      this.transform.LookAt(target.transform.position);
    }
  }

  private void InitLook(){
    this.transform.position = new Vector3(
      target.transform.position.x,
      this.Height,
      target.transform.position.z - this.FollowDist
    );

    this._look.y = this.Height;
    this._look.z = this.transform.position.z + this.FollowDist;

    this.transform.LookAt(target.transform.position);
  }
}
