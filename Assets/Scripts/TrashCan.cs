using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

//  private void Update () {
//    if ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began)) {
//      Debug.Log("OK");
//    }
//  }

  public void OnBeginDrag (PointerEventData eventData) {
    Debug.Log(gameObject.name);
  }

  public void OnDrag (PointerEventData eventData) {

  }

  public void OnEndDrag (PointerEventData eventData) {

  }

  private void OnMouseDrag () {
    Debug.Log(gameObject.name);
  }
}
