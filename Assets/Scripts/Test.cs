using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

  private GameObject test1;
  private GameObject test2;
  private bool test = false;

  void Start () {
    test1 = transform.GetChild(2).gameObject;
    test2 = test1.transform.GetChild(0).gameObject;
  }

	void Update () {
	  if (Input.GetKeyDown(KeyCode.F)) {
	    test1.SetActive(true);
	  }

	  if (Input.GetKeyDown(KeyCode.G)) {
	    test2.SetActive(true);
	    test = true;
	  }

	  if (test) {
	    if (test2.GetComponent<RectTransform>().offsetMin.y >= 0) {
	      test = false;
	    }
	    test2.transform.Translate(0, Time.deltaTime * 5, 0);
	  }
	}
}
