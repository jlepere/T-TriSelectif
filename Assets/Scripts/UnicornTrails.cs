using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnicornTrails : MonoBehaviour {

  public Button power;
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
      transform.Translate(4f * Time.deltaTime,0,0);
    if (transform.position.x > 16) {
      go = false;
      transform.position = initialPos;
    }
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag == "BlueTrash" || collider.tag == "GreenTrash" ||
    collider.tag == "YellowTrash" || collider.tag == "BrownTrash")
    {
      Destroy(collider.gameObject);
    }
  }

  public void Activate () {
    go = true;
    power.interactable = false;
  }
}
