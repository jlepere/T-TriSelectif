using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour {

  public Vector2 direction;
  public bool directionChosen = false;

  void Update() {
    if (Input.touchCount > 0) {
      Touch touch = Input.GetTouch(0);

      Vector2 test = Camera.main.ScreenToWorldPoint(touch.position);
      RaycastHit2D hit = Physics2D.Raycast(test, (touch.position));
      if (hit.collider && CompareTag(hit.collider.tag)) {
        switch (touch.phase) {
          case TouchPhase.Began:
            directionChosen = false;
            break;

          case TouchPhase.Moved:
            direction = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = direction;
            break;

          case TouchPhase.Ended:
            directionChosen = true;
            break;
        }
      }
      if (directionChosen) {
        GetComponent<SpriteRenderer>().color = Color.magenta;
      }
    }
  }

}
