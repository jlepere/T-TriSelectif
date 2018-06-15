using System.Collections.Generic;
using UnityEngine;

public class TrashManager : Singleton<TrashManager>
{
  [SerializeField]
  private GameObject trashPrefab;

  [SerializeField]
  private int maxTrashOnScreen;

  [SerializeField]
  private int maxTrashOnDraft;

  [SerializeField]
  private List<GameObject> trashOnScreen;

  [SerializeField]
  private List<GameObject> trashOnDraft;

  protected override void Awake()
  {
    trashOnScreen = new List<GameObject>();
    trashOnDraft = new List<GameObject>();
  }

  private void OnDestroy()
  {
    foreach (GameObject trash in trashOnScreen)
      Destroy(trash);

    foreach (GameObject trash in trashOnDraft)
      Destroy(trash);
  }

  public void SpawnTrash()
  {
    if (trashOnScreen.Count >= maxTrashOnScreen)
      return;
    GameObject newTrash = Instantiate(trashPrefab, new Vector3(0f, 8f, 0f), Quaternion.Euler(Vector3.zero));
    newTrash.transform.parent = this.transform;
    trashOnScreen.Add(newTrash);
  }
}
