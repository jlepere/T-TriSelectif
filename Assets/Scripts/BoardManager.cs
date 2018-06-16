using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
  [Range(0, 100)]
  [SerializeField]
  private int ecoPower = 50;

  [SerializeField]
  private int ecoBonus = 5;

  [SerializeField]
  private int ecoMalus = 10;

  [SerializeField]
  private int gameScore = 0;

  [SerializeField]
  private int scoreBonus = 1;

  [SerializeField]
  private int maxScoreBonus = 5;

  [SerializeField]
  private int scoreChain = 0;

  [SerializeField]
  private List<GameObject> ecoPrefabs = new List<GameObject>();

  /*[SerializeField]
  private float timeOfTransform = 5;

  [SerializeField]
  private float transformCurrentTime = 0;

  [SerializeField]
  private float transformNormalizedValue;

  private RectTransform rectTransform;
  private Vector3 startTransform;
  private Vector3 endTransform;*/

  private void OnDestroy()
  {
    foreach (GameObject prefab in ecoPrefabs)
      Destroy(prefab);
    ecoPrefabs.Clear();
  }

  public int EcoPower
  {
    get { return ecoPower; }
  }

  public void EcoScore()
  {
    scoreChain++;
    if (scoreChain > 1)
      EcoBoost();
    gameScore += ecoBonus * scoreBonus;

    if (gameScore >= 40 && gameScore < 50)
      FadeBackground(ecoPrefabs[3], ecoPrefabs[2]);
    else if (gameScore >= 50 && gameScore < 60)
      FadeBackground(ecoPrefabs[2], ecoPrefabs[3]);

    Debug.Log("ScoreBonus " +  scoreBonus);
    Debug.Log("GameScore " + gameScore);
  }

  private void FadeBackground(GameObject toHide, GameObject toShow)
  {

  }

  private void TranslateBackground(GameObject back)
  {

  }

  /*private IEnumerator LerpObject()
  {
    while (transformCurrentTime <= timeOfTransform)
    {
      transformCurrentTime =+ Time.deltaTime;
      transformNormalizedValue = transformCurrentTime / timeOfTransform;
      rectTransform.anchoredPosition = Vector3.Lerp(startTransform, endTransform, transformNormalizedValue);
      yield return null;
    }
  }*/

  public void EcoBoost()
  {
    if (ecoPower < 100)
      ecoPower += ecoBonus;
    else
      ecoPower = 100;
    if (scoreBonus < maxScoreBonus)
      scoreBonus++;
    else
      scoreBonus = maxScoreBonus;
  }

  public void EcoReset()
  {
    scoreChain = 0;
  }
}
