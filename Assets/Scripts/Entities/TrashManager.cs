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

  private bool active = true;

  private bool isActive = true;

  private void Start()
  {
    ActivateSpawner();
  }

  private void OnDestroy()
  {
    ClearTrash();
    isActive = false;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.tag == "BlueTrash" || collider.tag == "GreenTrash" ||
    collider.tag == "YellowTrash" || collider.tag == "BrownTrash" ||
    collider.tag == "TrickyTrash")
    {
      if (active)
        ActivateSpawner();
    }
  }

  public void ClearTrash()
  {
    foreach (GameObject trash in trashOnScreen)
      Destroy(trash);
    trashOnScreen.Clear();
  }

  private void ActivateSpawner()
  {
    int selectedSpawner = Random.Range(0, listSpawners.Count - 1);
    SpawnTrash(listSpawners[selectedSpawner]);
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

  public void StopSpawn () {
    active = false;
  }
}
