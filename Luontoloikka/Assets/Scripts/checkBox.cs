using UnityEngine;
using System.Collections;

public class checkBox : MonoBehaviour {

	public string option;
	//private SpriteRenderer sr = GetComponent<SpriteRenderer>().enabled = true;
	private Transform check;
	private SpriteRenderer sr;
	//private bool check;

	void Start(){
		foreach (Transform t in transform)
		{
			if(t.name == "checkBoxMark"){
				check = t;
				sr = t.GetComponent<SpriteRenderer>();
			}
		}


	}

	void Update(){
		if (staticVariables.gameSize == option) {
			sr.enabled = true;
		}else{
			sr.enabled = false;
		}
	}

	public void OnTouchDown(){
		staticVariables.gameSize = option;
		/*
		foreach (Transform t in transform)
		{
			if(t.name == "checkBoxMark"){

				if(t.GetComponent<SpriteRenderer>().enabled == false){
					//t.GetComponent<SpriteRenderer>().enabled = true;
					staticVariables.gameSize = option;
				}else{
					//t.GetComponent<SpriteRenderer>().enabled = false;
				}
				break;
				//t.GetComponent<SpriteRenderer>().enabled = t.GetComponent<SpriteRenderer>().enabled == false ? true : false;
			}
		}*/
	}
}
