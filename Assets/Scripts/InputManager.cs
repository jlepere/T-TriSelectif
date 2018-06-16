using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class InputManager : MonoBehaviour {

  private GameObject draft;
  private bool selected = false;
  private Animator animator;

  void Start () {
    animator = GetComponent<Animator>();
  }

  void Update () {

    if (Input.touchCount == 1) {
      Touch touch = Input.GetTouch(0);
      Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
      RaycastHit2D[] hits = Physics2D.RaycastAll(touchPos, (touch.position));

      switch (touch.phase) {
        case TouchPhase.Began:
          if (!selected) {
            foreach (var hit in hits) {
              if (hit.collider && hit.collider.gameObject.layer == 8) {
                draft = hit.collider.gameObject;
                draft.transform.localScale = new Vector3(1.1f, 1.1f, 1);
                selected = true;
                break;
              }
            }
          }
          else if (selected) {
            foreach (var hit in hits) {
              if (hit.collider && hit.collider.CompareTag("DraftLocker")) {
                if (hit.collider.transform.childCount > 0 && !draft.CompareTag(hit.collider.transform.GetChild(0).tag)) {
                  GameObject tmpDraft = hit.collider.transform.GetChild(0).gameObject;
                  Transform tmp = draft.transform.parent;
                  draft.transform.parent = tmpDraft.transform.parent;
                  draft.transform.parent.GetComponent<DraftLocker>().SetDraft(draft.GetComponent<Draft>());
                  draft.transform.localPosition = new Vector3(0, 0, 10);
                  tmpDraft.transform.parent = tmp;
                  tmpDraft.transform.parent.GetComponent<DraftLocker>().SetDraft(tmpDraft.GetComponent<Draft>());
                  tmpDraft.transform.localPosition = new Vector3(0, 0, 10);
                  animator.speed = 1f;
                } else {
                  Transform tmp = draft.transform.parent;
                  draft.transform.parent = hit.collider.transform;
                  draft.transform.parent.GetComponent<DraftLocker>().SetDraft(draft.GetComponent<Draft>());
                  draft.transform.localPosition = new Vector3(0, 0, 10);
                  animator.speed = 0.5f;
                }
                draft.transform.localScale = new Vector3(1, 1, 1);
                draft = null;
                selected = false;
                break;
              }
            }
          }
          break;
      }
    }
  }
}