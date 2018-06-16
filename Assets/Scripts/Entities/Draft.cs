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

  void Update () {
    if (Input.touchCount > 0) {
      Touch touch = Input.GetTouch(0);

      Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

      switch (touch.phase) {
        case TouchPhase.Began:
          drag = false;
          RaycastHit2D[] hits = Physics2D.RaycastAll(touchPos, (touch.position));
          foreach (var hit in hits) {
            if (hit.collider && CompareTag(hit.collider.tag)) {
              initialPos = transform.position;
              drag = true;
              draftLocker = false;
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

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if ((collider.tag == "BlueTrash" && this.tag == "BlueDraft") ||
    (collider.tag == "GreenTrash" && this.tag == "GreenDraft") ||
    (collider.tag == "YellowTrash" && this.tag == "YellowDraft"))
    {
      if (trashOnDraft.Count >= draftManager.MaxTrashOnDraft)
        return;
      TrashManager.Instance.RemoveTrashOnScreen(collider.gameObject);
      trashOnDraft.Add(collider.gameObject);
      collider.transform.parent = this.transform;
    }
  }
}
