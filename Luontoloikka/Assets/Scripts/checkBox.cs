using UnityEngine;
using System.Collections;

public class checkBox : MonoBehaviour {

	//Variable which defines what option this particular instance of CheckBox selects (Short, Medium, Long)
	public string option;

	//sr is the SpriteRenderer of "checkBoxMark" child object, which shows if the current check box is checked
	private SpriteRenderer sr;

	//Start goes through all of the the current instance's child objects, and finds the one with the name "checkBoxMark"
	//It then sets this instance's "sr" variable to the SpriteRenderer of the "checkBoxMark" object.
	void Start(){
		foreach (Transform t in transform)
		{
			if(t.name == "checkBoxMark"){
				sr = t.GetComponent<SpriteRenderer>();
			}
		}


	}

	//Update checks if the currently selected game size in Static Variables is the same as the current instance's "option" variable
	//If it is, the current check box's "checkBoxMark" is shown, indicating that it is selected. If not, it is hidden
	void Update(){
		if (staticVariables.gameSize == option) {
			sr.enabled = true;
		}else{
			sr.enabled = false;
		}
	}

	//When the touched, the current instance's option is set as the game size in Static Variables
	public void OnTouchDown(){
		staticVariables.gameSize = option;
	}
}
