using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

  private float initialX;
  private float currentX;
  private float randomFactor;

  void Awake () {
    initialX = transform.position.x;
    randomFactor = Random.Range(0.1f, 0.5f);
  }

  void Update() {
    float accel = Input.acceleration.x * randomFactor;
    currentX = transform.position.x;
    if (currentX + accel < initialX + 0.5f && accel > 0) {
      transform.Translate(accel, 0, 0);
    }
    if (currentX - accel > initialX - 0.5f && accel < 0) {
      transform.Translate(accel, 0, 0);
    }
  }

}
