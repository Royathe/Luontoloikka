using UnityEngine;
using System.Collections;

public class forest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().hasSnow == true) {
			GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
