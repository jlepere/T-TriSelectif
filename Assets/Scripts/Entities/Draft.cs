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

  private void OnDestroy()
  {
    ClearTrash(true);
  }

  public int TrashInDraft
  {
    get { return trashOnDraft.Count; }
  }

  public void ClearTrash(bool end)
  {
    foreach (GameObject trash in trashOnDraft)
      Destroy(trash);
    trashOnDraft.Clear();
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    Trash trash = collider.gameObject.GetComponent<Trash>();
    if ((collider.tag == "BlueTrash" && this.tag == "BlueDraft") ||
    (collider.tag == "GreenTrash" && this.tag == "GreenDraft") ||
    (collider.tag == "YellowTrash" && this.tag == "YellowDraft"))
    {
      SoundManager.Instance.PlayRight();
      if (trash == null || !trash.DraftCollide)
        return;
      TrashManager.Instance.RemoveTrashOnScreen(collider.gameObject);
      trashOnDraft.Add(collider.gameObject);
      collider.transform.parent = this.transform;
      BoardManager.Instance.EcoScore();
    }
    else if (collider.tag == "BrownTrash")
    {
      if (trash.HurtMe)
        return;
      SoundManager.Instance.PlayWrong();
      if (trash != null)
        trash.DraftCollide = false;
      BoardManager.Instance.EcoReset();
      trash.HurtMe = true;
    }
  }
}
