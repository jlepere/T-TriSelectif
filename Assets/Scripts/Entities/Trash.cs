using UnityEngine;

public class Trash : MonoBehaviour
{
  [SerializeField]
  private bool draftCollide = true;

  public bool DraftCollide
  {
    get { return draftCollide; }
    set { draftCollide = value; }
  }
}
