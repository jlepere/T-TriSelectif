using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField] private GameObject rules;

	private bool startGame = false;

	void Start () {
		if (PlayerPrefs.GetFloat("rules") == 0)
		{
			rules.SetActive(true);
		}
	}

	void Update () {
		if (startGame)
			Time.timeScale = 1;
		else 
			Time.timeScale = 0;
	}

	public void QuitRules() {
		rules.SetActive(false);
		startGame = true;
	}
}
