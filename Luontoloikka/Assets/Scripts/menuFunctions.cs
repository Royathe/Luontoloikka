using UnityEngine;
using System.Collections;

public class menuFunctions : MonoBehaviour {

	public string function;
	private GameObject newGameOptionsMenu;
	private bool newGameOptionsMenuActive;
	private MenuController mc;

	// Use this for initialization
	void Start () {
		//newGameOptionsMenu = GameObject.FindGameObjectWithTag ("NewGameOptions");
		//newGameOptionsMenuActive = false;
		//newGameOptionsMenu.SetActive (false);
		if(GameObject.FindWithTag("MenuController") == true){
			mc = GameObject.FindGameObjectWithTag ("MenuController").GetComponent<MenuController>();
		}
	}

	void OnTouchDown(){

		if (function == "quit") {
			Debug.Log ("Application Quit.");
			Application.Quit ();
		}
		else if (function == "newGame"){
			mc.showNewGameOptions();
		}
		else if (function == "startGame"){
			LoadingScreen.show();
			Application.LoadLevel ("Main");
		}
		else if(function == "return"){
			LoadingScreen.show();
			Application.LoadLevel("Menu");
		}
		
	}

	private void showNewGameOptions(){
		if(newGameOptionsMenuActive == false){
			newGameOptionsMenu.SetActive (true);
			newGameOptionsMenuActive = true;
		}else{
			newGameOptionsMenu.SetActive (false);
			newGameOptionsMenuActive = false;
		}
	}


}
