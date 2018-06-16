﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] right;
	public AudioClip wrong;

	private AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();	
	}
	
	void PlayRight () {
		source.PlayOneShot(right[Random.Range[0, 5]]);
	}
	
	void PlayWrong () {
		source.PlayOneShot(wrong);
	}
}
