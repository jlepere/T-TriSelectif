using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public AudioClip click;

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
		transition = true;
		source.PlayOneShot(click);
		StartCoroutine(LoadAsyncScene());
	}

	IEnumerator LoadAsyncScene ()
	{

		yield return new WaitForSeconds(4);

		while (true)
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/TrashScene");
      		break;
		}
	}
}
