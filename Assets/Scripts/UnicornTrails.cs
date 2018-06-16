using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornTrails : MonoBehaviour {

  private Vector3 initialPos;
//  private float initialY;
  private bool go = true;

  void Start () {
    initialPos = transform.position;
//    initialY = transform.position.y;
  }

  void Update () {
    float y = Mathf.Sin(Time.time * 5) * 0.5f;
    transform.position = new Vector3(transform.position.x, initialPos.y + y, 10);

    if (Input.GetKeyDown(KeyCode.Space))
      go = true;
    if (go)
      transform.Translate(4f * Time.deltaTime,0,0);
    if (transform.position.x > 16) {
      go = true;
      transform.position = initialPos;
    }
  }
}
