using UnityEngine;
using System.Collections;

public class forest : MonoBehaviour {

	// If snow has been added to the game, forest is created with snow enabled.
	void Start () {
		if (GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().hasSnow == true) {
			GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
