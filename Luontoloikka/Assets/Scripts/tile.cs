using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

	public bool hasLeft;
	public bool hasRight;
	public bool hasUp;
	public bool hasDown;
	public string type;
	//public string flavorText;
	//public string taskText;

	public string getDirection(string entryDir){
		string exitDir; //Exit direction of tile

		if (hasLeft == true && entryDir != "left") {exitDir = "left";}
		else if (hasRight == true && entryDir != "right") {exitDir = "right";}
		else if (hasDown == true && entryDir != "down") {exitDir = "down";}
		else{exitDir = "up";}

		return exitDir;
	}
	/*
	// Use this for initialization
	void Start () {

	}

	public string getFlavorText(){
		return flavorText;
	}

	public void assignTexts(string Flavor, string Task){
		//Debug.Log (Flavor + " : " + this);
		this.flavorText = Flavor;
		this.taskText = Task;
		//Debug.Log (flavorText);
	}

	// Update is called once per frame
	void Update () {
	
	}*/
}
