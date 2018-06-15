using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
  protected static T instance;

  protected virtual void Awake()
  {
    instance = this as T;
  }

  public static T Instance
  {
    get
    {
      if (instance == null && (instance = FindObjectOfType<T>()) == null)
      {
        GameObject newInstance = new GameObject();
        instance = newInstance.AddComponent<T>();
      }
      return instance;
    }
  }
}
