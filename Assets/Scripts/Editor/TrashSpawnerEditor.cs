using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrashSpawner))]
public class TrashSpawnerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    GUILayout.FlexibleSpace();
    TrashManager trashManager = TrashManager.Instance;
    TrashSpawner trashSpawner = (TrashSpawner)target;
    if (GUILayout.Button("Spawn Trash"))
    {
      trashManager.SpawnTrash(trashSpawner.gameObject);
    }
  }
}
