﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnibadassTrails : MonoBehaviour {

  private Vector3 initialPos;
  private bool go = false;

  void Start () {
    initialPos = transform.position;
  }

  void Update () {
    float y = Mathf.Sin(Time.time * 5) * 0.5f;
    transform.position = new Vector3(transform.position.x, initialPos.y + y, 10);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      SoundManager.Instance.PlayHorse();
      go = true;
    }
    if (go)
      transform.Translate(-4f * Time.deltaTime, 0, 0);
    if (transform.position.x < -8.5f) {
      go = false;
      transform.position = initialPos;
    }

    if (Input.GetKeyDown(KeyCode.A))
      go = true;
  }
}
