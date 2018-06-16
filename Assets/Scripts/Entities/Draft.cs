using System.Collections.Generic;
using UnityEngine;

public class Draft : MonoBehaviour
{
  private DraftManager draftManager;

  [SerializeField]
  private List<GameObject> trashOnDraft = new List<GameObject>();

  private void Start()
  {
    draftManager = DraftManager.Instance;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag == "Trash")
    {
      if (trashOnDraft.Count >= draftManager.MaxTrashOnDraft)
        return;
      TrashManager.Instance.RemoveTrashOnScreen(collider.gameObject);
      trashOnDraft.Add(collider.gameObject);
      collider.transform.parent = this.transform;
    }
  }
}
