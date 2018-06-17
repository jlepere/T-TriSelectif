﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public AudioClip click;

	[SerializeField] private GameObject image;

	private AudioSource source;
	private bool transition = false;

	void Start () {
		source = GetComponent<AudioSource>();
	}

	void Update () {
		if (transition)
		{
			if (transform.position.y >= -18.75)
			{
				transform.Translate(0, -5 * Time.deltaTime, 0);
				source.volume -= Time.deltaTime / 3;
			}
		}
	}

	public void LoadNextScene () {
		if (PlayerPrefs.GetFloat("rules", 0) != 0) 
		{
			transition = true;
			source.PlayOneShot(click);
			StartCoroutine(LoadAsyncScene());
		} else {
			PlayerPrefs.SetFloat("rules", 1f);
			image.SetActive(true);
		}
	}

	IEnumerator LoadAsyncScene ()
	{

		yield return new WaitForSeconds(4);

		while (true)
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
      		break;
		}
	}
}
