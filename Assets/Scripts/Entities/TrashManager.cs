using System.Collections.Generic;
using UnityEngine;

public class TrashManager : Singleton<TrashManager>
{
  [SerializeField]
  private List<GameObject> trashPrefabs = new List<GameObject>();

  [SerializeField]
  private List<GameObject> listSpawners = new List<GameObject>();

  [SerializeField]
  private int maxTrashOnScreen = 5;

  [SerializeField]
  private List<GameObject> trashOnScreen = new List<GameObject>();

  private void OnDestroy()
  {
    ClearTrash();
  }

  public void ClearTrash()
  {
    foreach (GameObject trash in trashOnScreen)
      Destroy(trash);
    trashOnScreen.Clear();
  }

  public void SpawnTrash(GameObject spawner)
  {
    if (trashOnScreen.Count >= maxTrashOnScreen)
      return;
    int selectedTrash = Random.Range(0, trashPrefabs.Count - 1);
    GameObject newTrash = Instantiate(trashPrefabs[selectedTrash], spawner.transform.position, Quaternion.Euler(Vector3.zero));
    newTrash.transform.parent = spawner.transform;
    trashOnScreen.Add(newTrash);
  }

  public void RemoveTrashOnScreen(GameObject trash)
  {
    if (trashOnScreen.Contains(trash))
    {
      trashOnScreen.Remove(trash);
      trash.SetActive(false);
    }
  }
  /*[SerializeField]
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

  public void MoveOnDraft(GameObject trash)
  {
    if (trashOnScreen.Contains(trash))
    {
      trashOnScreen.Remove(trash);
      trashOnDraft.Add(trash);
      trash.SetActive(false);
    }
  }

  public void SpawnTrash()
  {
    if (trashOnScreen.Count >= maxTrashOnScreen)
      return;
    GameObject newTrash = Instantiate(trashPrefab, new Vector3(0f, 8f, 0f), Quaternion.Euler(Vector3.zero));
    newTrash.transform.parent = this.transform;
    trashOnScreen.Add(newTrash);
  }*/
}
