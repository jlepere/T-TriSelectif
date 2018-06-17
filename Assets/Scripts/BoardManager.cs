using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

  [SerializeField] private Text scoreText;
  [SerializeField] private Text timeText;
  [SerializeField] private Slider slider;

  private float timer = 0;

  void Start () {
    scoreText.text = "Score\n0";
  }

  void Update () {
    timer += Time.deltaTime;
    string minutes = Mathf.Floor(timer / 60).ToString("00");
    string seconds = (timer % 60).ToString("00");

    slider.value = ecoPower;
    timeText.text = minutes + ":" + seconds;
  }

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
    scoreText.text = "Score\n" + gameScore.ToString();

    if (gameScore >= 40 && gameScore < 50)
      FadeBackground(ecoPrefabs[3], ecoPrefabs[2]);
    else if (gameScore >= 50 && gameScore < 60)
      FadeBackground(ecoPrefabs[2], ecoPrefabs[3]);
  }

  private void FadeBackground(GameObject toHide, GameObject toShow)
  {

  }

  private void TranslateBackground(GameObject back)
  {

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

  public void EcoBurst()
  {
    if (ecoPower > 0)
      ecoPower -= ecoMalus;
    else 
      ecoPower = 0;
  }

  public void EcoReset()
  {
    EcoBurst();
    scoreChain = 0;
  }
}
