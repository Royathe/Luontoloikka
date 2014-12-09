using UnityEngine;
using System.Collections;

public class menuFunctions : MonoBehaviour {

	//Variable defines the current instance's function
	public string function;
	//Reference to the MenuController objects "MenuController" script
	private MenuController mc;

	// Checks if MenuController can be found. If it can, it referenced in the "mc" variable
	void Start () {
		if(GameObject.FindWithTag("MenuController") == true){
			mc = GameObject.FindGameObjectWithTag ("MenuController").GetComponent<MenuController>();
		}
	}

	//When touched, an action is done based on the "function" variable 
	//Should be a 'switch case' instead of an if/else. Turns out I'm an idiot
	void OnTouchDown(){

		if (function == "quit") {
			Debug.Log ("Application Quit.");
			Application.Quit ();
		}
		else if (function == "newGame"){
			mc.showNewGameOptions();
		}
		else if (function == "ohjeet"){
			mc.showOhjeetMenu();
		}
		else if (function == "startGame"){
			LoadingScreen.show();
			Application.LoadLevel ("Main");
		}
		else if(function == "return"){
			if(GameObject.FindWithTag("CenterTile") == true){
				GameObject.FindGameObjectWithTag ("CenterTile").GetComponent<centerTile>().flavorText = "";
			}
			LoadingScreen.show();
			Application.LoadLevel("Menu");
		}
		
	}

}
