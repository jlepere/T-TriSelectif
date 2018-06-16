using System.Collections.Generic;
using UnityEngine;

public class Draft : MonoBehaviour
{
  private DraftManager draftManager;

  private Vector2 direction;
  private Vector2 initialPos;
  private bool drag = false;
  private bool draftLocker = false;

  [SerializeField]
  private List<GameObject> trashOnDraft = new List<GameObject>();

  private void Start()
  {
    draftManager = DraftManager.Instance;
  }

  /*
  void Update () {
    if (Input.touchCount > 0) {
      Touch touch = Input.GetTouch(0);

      Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

      switch (touch.phase) {
//        case TouchPhase.Stationary:
//          RaycastHit2D[] hitsTest = Physics2D.RaycastAll(touchPos, (touch.position));
//          foreach (var hit in hitsTest) {
//            if (hit.collider && CompareTag(hit.collider.tag)) {
//              if (Input.touchCount == 2) {
//                Debug.Log("Double Touch");
//                Touch touch2 = Input.GetTouch(1);
//
//                GameObject g1 = hit.collider.gameObject;
//                GameObject g2 = null;
//
//                Vector2 touch2Pos = Camera.main.ScreenToWorldPoint(touch2.position);
//                RaycastHit2D[] hits2 = Physics2D.RaycastAll(touch2Pos, (touch2.position));
//
//                foreach (var hit2 in hits2) {
//                  if (hit2.collider && hit2.collider.gameObject.layer == 8) {
//                    g2 = hit2.collider.gameObject;
//                    Debug.Log("Ok");
//                    break;
//                  }
//                }
//
//                if (g1 != null && g2 != null) {
//                  Debug.Log("Swap");
//                  Transform tmp = g1.transform.parent;
//                  g1.transform.parent = g2.transform.parent;
//                  g1.transform.parent.GetComponent<DraftLocker>().SetDraft(g1.GetComponent<Draft>());
//                  g1.transform.localPosition = new Vector3(0, 0, 10);
//                  g2.transform.parent = tmp;
//                  g2.transform.parent.GetComponent<DraftLocker>().SetDraft(g2.GetComponent<Draft>());
//                  g2.transform.localPosition = new Vector3(0, 0, 10);
//                }
//              }
//              break;
//            }
//          }
//
//          break;

        case TouchPhase.Began:
          drag = false;
          RaycastHit2D[] hits = Physics2D.RaycastAll(touchPos, (touch.position));
          foreach (var hit in hits) {
            if (hit.collider && CompareTag(hit.collider.tag)) {
              if (Input.touchCount == 1) {
                initialPos = transform.position;
                drag = true;
                draftLocker = false;
              }
//              else {
//                Debug.Log("Double Touch");
//                Touch touch2 = Input.GetTouch(1);
//
//                GameObject g1 = hit.collider.gameObject;
//                GameObject g2 = null;
//
//                Vector2 touch2Pos = Camera.main.ScreenToWorldPoint(touch2.position);
//                RaycastHit2D[] hits2 = Physics2D.RaycastAll(touch2Pos, (touch2.position));
//
//                foreach (var hit2 in hits2) {
//                  if (hit2.collider && hit2.collider.gameObject.layer == 8) {
//                    g2 = hit2.collider.gameObject;
//                    Debug.Log("Ok");
//                    break;
//                  }
//                }
//
//                if (g1 != null && g2 != null) {
//                  Debug.Log("Swap");
//                  Transform tmp = g1.transform.parent;
//                  g1.transform.parent = g2.transform.parent;
//                  g1.transform.parent.GetComponent<DraftLocker>().SetDraft(g1.GetComponent<Draft>());
//                  g1.transform.localPosition = new Vector3(0, 0, 10);
//                  g2.transform.parent = tmp;
//                  g2.transform.parent.GetComponent<DraftLocker>().SetDraft(g2.GetComponent<Draft>());
//                  g2.transform.localPosition = new Vector3(0, 0, 10);
//                }
//              }
              break;
            }
          }
          break;

        case TouchPhase.Moved:
          if (drag) {
            direction = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = direction;
          }
          break;

        case TouchPhase.Ended:
          if (drag) {
            Transform tmp1 = transform.parent;
            GameObject swap = null;
            RaycastHit2D[] targetHits = Physics2D.RaycastAll(touchPos, (touch.position));
            foreach (var targetHit in targetHits) {
              Debug.Log("test : " + targetHit.collider.tag);
              if (targetHit.collider && targetHit.collider.tag == "DraftLocker") {
                draftLocker = true;
                transform.parent.gameObject.GetComponent<DraftLocker>().SetDraft(null);
                transform.parent = targetHit.collider.transform;
                transform.parent.gameObject.GetComponent<DraftLocker>().SetDraft(this);
                transform.localPosition = new Vector3(0, 0, 10);
              } else if (targetHit.collider && targetHit.collider.gameObject.layer == 8 && !CompareTag(targetHit.collider.tag)) {
                swap = targetHit.collider.gameObject;
              }
            }
            if (!draftLocker) {
              transform.position = initialPos;
            } else if (swap != null) {
              swap.transform.parent = tmp1;
              tmp1.gameObject.GetComponent<DraftLocker>().SetDraft(swap.GetComponent<Draft>());
              swap.transform.localPosition = new Vector3(0, 0, 10);
            }
          }
          drag = false;
          break;
      }
    }
  }
  */

  private void OnTriggerEnter2D(Collider2D collider)
  {
    Trash trash = collider.gameObject.GetComponent<Trash>();
    if ((collider.tag == "BlueTrash" && this.tag == "BlueDraft") ||
    (collider.tag == "GreenTrash" && this.tag == "GreenDraft") ||
    (collider.tag == "YellowTrash" && this.tag == "YellowDraft"))
    {
      if (trashOnDraft.Count >= draftManager.MaxTrashOnDraft || trash == null || !trash.DraftCollide)
        return;
      TrashManager.Instance.RemoveTrashOnScreen(collider.gameObject);
      trashOnDraft.Add(collider.gameObject);
      collider.transform.parent = this.transform;
    }
    else
    {
      if (trash != null)
        trash.DraftCollide = false;
    }
  }
}
