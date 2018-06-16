using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

	public AudioClip[] right;
	public AudioClip wrong;
	public AudioClip horse;

	private AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();
	}

	public void PlayRight () {
		source.PlayOneShot(right[Random.Range(0, 5)]);
	}

	public void PlayWrong () {
		source.PlayOneShot(wrong);
	}

	public void PlayHorse () {
		source.PlayOneShot(horse);
	}
}
