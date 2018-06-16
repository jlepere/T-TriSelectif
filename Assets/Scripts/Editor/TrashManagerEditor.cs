using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrashManager))]
public class TrashManagerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    GUILayout.FlexibleSpace();
    TrashManager trashManager = (TrashManager)target;
    if (GUILayout.Button("Clear Trash"))
    {
      trashManager.ClearTrash();
    }
  }
}
