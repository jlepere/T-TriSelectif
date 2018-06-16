using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornTrails : MonoBehaviour {

  private float initialY;
  private bool go = false;

  void Start () {
    initialY = transform.position.y;
  }

  void Update () {
    float y = Mathf.Sin(Time.time * 5) * 0.5f;
    transform.position = new Vector3(transform.position.x, initialY + y, 10);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      SoundManage.Instance.PlayHorse();
      go = true;
    }
    if (go)
      transform.Translate(.1f,0,0);
  }
}
