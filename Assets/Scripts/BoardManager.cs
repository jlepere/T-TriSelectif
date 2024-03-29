﻿using System.Collections;
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
  private int ecoMalus = 5;

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
  [SerializeField] private GameObject endGame;

  public Button power;

  private float timer = 0;

  void Start () {
    scoreText.text = "0";
  }

  private void OnDestroy()
  {
    foreach (GameObject prefab in ecoPrefabs)
      Destroy(prefab);
    ecoPrefabs.Clear();
  }

  void Update () {
    timer += Time.deltaTime;
    string minutes = Mathf.Floor(timer / 60).ToString("00");
    string seconds = (timer % 60).ToString("00");

    slider.value = ecoPower;
    timeText.text = minutes + ":" + seconds;

    if (ecoPower <= 0)
      StartCoroutine(EndGame());

    if (ecoPower < 100 && ecoPower >= 80)
      TranslateBackground(ecoPrefabs[5]);
    else if (ecoPower < 80 && ecoPower >= 60)
      TranslateBackground(ecoPrefabs[4]);

    else if (ecoPower < 40 && ecoPower >= 20)
      TranslateBackground(ecoPrefabs[1]);
    else if (ecoPower < 20 && ecoPower >= 0)
      TranslateBackground(ecoPrefabs[0]);
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
    if (scoreChain == 5)
      power.interactable = true;
    gameScore += ecoBonus * scoreBonus;
    scoreText.text = gameScore.ToString();

    Debug.Log("ScoreBonus " +  scoreBonus);
    Debug.Log("GameScore " + ecoPower);
  }

  private void FadeBackground(GameObject toHide, GameObject toShow)
  {
    toShow.SetActive(true);
    toHide.SetActive(false);
//    StopAllCoroutines();
//    StartCoroutine(FadeBGCoroutine(toHide, toShow));
  }

  IEnumerator FadeBGCoroutine (GameObject toHide, GameObject toShow) {
    toShow.SetActive(true);
    float r = toShow.GetComponent<Image>().color.r;
    float g = toShow.GetComponent<Image>().color.g;
    float b = toShow.GetComponent<Image>().color.b;
    float a = toShow.GetComponent<Image>().color.a;
    toShow.GetComponent<Image>().color = new Color(r,g,b,0);
    while (a < 1) {
      a += 0.2f;
      toShow.GetComponent<Image>().color = new Color(r,g,b,a);
      yield return new WaitForSeconds(0.1f);
    }
//    toHide.SetActive(false);
    yield return null;
  }

  private void TranslateBackground(GameObject back) {
    back.SetActive(true);
    StopAllCoroutines();
    if (back.GetComponent<RectTransform>().offsetMin.y >= 0)
      StartCoroutine(TranslateBGCoroutine(back, -1));
    else
      StartCoroutine(TranslateBGCoroutine(back, 1));
  }

  private void OnCollisionEnter2D(Collision2D collider)
  {
    Trash trash = collider.gameObject.GetComponent<Trash>();
    if (collider.collider.tag == "BrownTrash" && trash.DraftCollide)
    {
      SoundManager.Instance.PlayRight();
      TrashManager.Instance.RemoveTrashOnScreen(collider.gameObject);
      BoardManager.Instance.EcoScore();
    }
    else
    {
      if (trash.HurtMe)
        return;
      SoundManager.Instance.PlayWrong();
      BoardManager.Instance.EcoReset();
      trash.HurtMe = true;
    }
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

    if (ecoPower >= 40 && ecoPower < 50)
      FadeBackground(ecoPrefabs[3], ecoPrefabs[2]);
    else if (ecoPower >= 50 && ecoPower < 60)
      FadeBackground(ecoPrefabs[2], ecoPrefabs[3]);
  }

  public void EcoReset()
  {
    scoreChain = 0;
    ecoPower -= ecoMalus;

    if (ecoPower >= 40 && ecoPower < 50)
      FadeBackground(ecoPrefabs[3], ecoPrefabs[2]);
    else if (ecoPower >= 50 && ecoPower < 60)
      FadeBackground(ecoPrefabs[2], ecoPrefabs[3]);
  }

  IEnumerator TranslateBGCoroutine (GameObject bg, int dir) {
    if (dir > 0) {
      while (bg.GetComponent<RectTransform>().offsetMin.y < 0) {
        bg.transform.Translate(0, Time.deltaTime, 0);
        yield return new WaitForSeconds(0.1f);
      }
    } else {
      while (bg.GetComponent<RectTransform>().offsetMin.y > -250) {
        bg.transform.Translate(0, Time.deltaTime * dir, 0);
        yield return new WaitForSeconds(0.1f);
      }
    }
    yield return null;
  }

  IEnumerator EndGame () {
    TrashManager.instance.StopSpawn();
    yield return new WaitForSeconds(1);
    endGame.SetActive(true);
    Debug.Log("GameOver");
    Time.timeScale = 0;
  }
}
