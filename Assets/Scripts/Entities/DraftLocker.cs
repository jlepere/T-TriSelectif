using UnityEngine;

public class DraftLocker : MonoBehaviour
{
  [SerializeField]
  private Draft draft;

  public void SetDraft (Draft newDraft) {
    draft = newDraft;
  }
}
