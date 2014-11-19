using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	private GameObject newGameOptionsMenu;
	private bool newGameOptionsMenuActive;

	// Use this for initialization
	void Start () {
		newGameOptionsMenu = GameObject.FindGameObjectWithTag ("NewGameOptions");
		newGameOptionsMenu.SetActive (false);
	}
	
	public void showNewGameOptions(){
		if(newGameOptionsMenuActive == false){
			newGameOptionsMenu.SetActive (true);
			newGameOptionsMenuActive = true;
		}else{
			newGameOptionsMenu.SetActive (false);
			newGameOptionsMenuActive = false;
		}
	}
}
