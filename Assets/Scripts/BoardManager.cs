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

    Debug.Log("ScoreBonus " +  scoreBonus);
    Debug.Log("GameScore " + gameScore);
  }

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
