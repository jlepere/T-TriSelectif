using System.Collections.Generic;
using UnityEngine;

public class DraftManager : Singleton<DraftManager>
{
  [SerializeField]
  private int maxTrashOnDraft = 5;

  public int MaxTrashOnDraft
  {
    get { return maxTrashOnDraft; }
  }
}
