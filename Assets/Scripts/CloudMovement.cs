using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

  private float randomFactor;

  void Awake () {
    randomFactor = Random.Range(0.02f, 0.04f);
  }

  void Update() {
    float accel = Input.acceleration.x * randomFactor;
    transform.Translate(accel, 0, 0);
    if (transform.position.x < -7.5f) {
      transform.position = new Vector3(7.5f, transform.position.y, transform.position.z);
    }
    if (transform.position.x > 7.5f) {
      transform.position = new Vector3(-7.5f, transform.position.y, transform.position.z);
    }
  }

}
