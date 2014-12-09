using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

	public bool hasLeft;
	public bool hasRight;
	public bool hasUp;
	public bool hasDown;
	public string type;

	public string getDirection(string entryDir){
		string exitDir; //Exit direction of tile

		if (hasLeft == true && entryDir != "left") {exitDir = "left";}
		else if (hasRight == true && entryDir != "right") {exitDir = "right";}
		else if (hasDown == true && entryDir != "down") {exitDir = "down";}
		else{exitDir = "up";}

		return exitDir;
	}
}
