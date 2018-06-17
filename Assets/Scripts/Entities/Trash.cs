using UnityEngine;

public class Trash : MonoBehaviour
{
  [SerializeField]
  private bool draftCollide = true;

  [SerializeField]
  private bool hurtMe = false;

  public bool DraftCollide
  {
    get { return draftCollide; }
    set { draftCollide = value; }
  }

  public bool HurtMe
  {
    get { return hurtMe; }
    set { hurtMe = value; }
  }
}
