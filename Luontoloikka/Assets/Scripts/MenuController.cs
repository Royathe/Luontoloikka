using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	//Reference to the object that contains the options for starting a new game.
	private GameObject newGameOptionsMenu;
	private GameObject ohjeetTextMenu;

	// Boolean checks if New Game Options are visible
	private bool newGameOptionsMenuActive;
	private bool ohjeetTextMenuActive;

	// Start finds the "NewGameOptions" object, and deactivates so it is not shown at the start of the game.
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		newGameOptionsMenu = GameObject.FindGameObjectWithTag ("NewGameOptions");
		newGameOptionsMenu.SetActive (false);
		newGameOptionsMenuActive = false;
		ohjeetTextMenu = GameObject.FindGameObjectWithTag ("OhjeetTeksti");
		ohjeetTextMenu.SetActive (false);
		ohjeetTextMenuActive = false;
	}

	// When this function is run, the New Game Options (Short, medium, long, start) are activated or deactivated depending on current state.
	public void showNewGameOptions(){
		if(ohjeetTextMenuActive == true){
			showOhjeetMenu();
		}
		if(newGameOptionsMenuActive == false){
			newGameOptionsMenu.SetActive (true);
			newGameOptionsMenuActive = true;
		}else{
			newGameOptionsMenu.SetActive (false);
			newGameOptionsMenuActive = false;
		}
	}

	public void showOhjeetMenu(){
		if(newGameOptionsMenuActive == true){
			showNewGameOptions();
		}
		if(ohjeetTextMenuActive == false){
			ohjeetTextMenu.SetActive (true);
			ohjeetTextMenuActive = true;
		}else{
			ohjeetTextMenu.SetActive (false);
			ohjeetTextMenuActive = false;
		}
	}

	void Update(){
		if (Application.platform == RuntimePlatform.Android){
			if (Input.GetKey(KeyCode.Escape)){
				Application.Quit();
				return;
			}
		}
	}
}
