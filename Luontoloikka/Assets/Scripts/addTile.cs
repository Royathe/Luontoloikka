using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class addTile : MonoBehaviour {

	public GameController gc;
	public string color;
	public bool enabled;
	private GameObject bagSprite;
	private List<GameObject> bag;
	private float nextClick;
	private float clickDelay;
	private Color fade;

	void Start(){

		fade = new Color (1, 1, 1, 0.6f);

		nextClick = 0.5f;
		clickDelay = 0;

		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		gc = go.GetComponent<GameController> ();

		if (color == "green") {
			bag = gc.greenBag;
			bagSprite = GameObject.FindGameObjectWithTag("GreenBag");
			enabled = true;
			gc.redBagCount.enabled = false;
			gc.selectedText = gc.greenBagCount;
		}else if (color == "red"){
			bag = gc.redBag;
			bagSprite = GameObject.FindGameObjectWithTag("RedBag");
			bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
			bagSprite.transform.localScale = new Vector2(0.95f,0.95f);
			enabled = false;
		}else{
			bag = gc.yellowBag;
			bagSprite = GameObject.FindGameObjectWithTag("YellowBag");
			bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
			bagSprite.transform.localScale = new Vector2(0.95f,0.95f);
			enabled = false;
		}
		
		#if UNITY_EDITOR //DEVELOPER OVERRIDES
		nextClick = 0;
		#endif

	}

	private void afterAddition(){
		bag.RemoveAt(0);

		GameObject.FindGameObjectWithTag ("CenterTile").audio.Play ();

		gc.greenBagCount.text = "" + gc.greenBag.Count;
		gc.redBagCount.text = "" + gc.redBag.Count;
		gc.yellowBagCount.text = "" + gc.yellowBag.Count;
	}
	
	private bool checkTile(int xI, int yI){
		int x = (int)gc.centerTileX + xI;
		int y = (int)gc.centerTileY + yI;
		bool isForest = false;
		Debug.Log (gc.map[x,y].tag);
		if (gc.map[x,y].tag == "forest"){
			isForest = true;
		}
		return isForest;
	}

	void OnTouchDown(){
		if(Time.time > clickDelay){
			clickDelay = Time.time + nextClick;
			if(enabled == true){
				if (bag [0].GetComponent<tile> ().type == "snow") { //ADDS SNOW
					afterAddition();
					if(gc.addSnow() == true){
						enabled = false;
						bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
						gc.endGame("loss");
					}
				}else if (bag [0].GetComponent<tile> ().type == "home"){
					gc.newTile(bag[0] , false);
					afterAddition();
					enabled = false;
					bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
					gc.endGame("victory");
				}else{ //ADDS NEW TILE TO CENTER

					switch (gc.nextDirection){

					case "up":
						if(checkTile(0,1) == true){
							gc.newTile(bag[0] , false);
							afterAddition();
						}
						break;
						
					case "down":
						if(checkTile(0,-1) == true){
							gc.newTile(bag[0] , false);
							afterAddition();
						}
						break;
						
					case "left":
						if(checkTile(-1,0) == true){
							gc.newTile(bag[0] , false);
							afterAddition();
						}
						break;
						
					case "right":
						if(checkTile(1,0) == true){
							gc.newTile(bag[0] , false);
							afterAddition();
						}
						break;
					}

				}
				if (bag.Count == 0) {
					enabled = false;
					nextBag(color);		
				}
			}
		}
	}

	private void nextBag(string lastColor){

		switch (lastColor) {

		case "green":
			GameObject.FindGameObjectWithTag("RedBag").GetComponent<addTile>().enabled = true;
			GameObject.FindGameObjectWithTag("RedBag").GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);
			GameObject.FindGameObjectWithTag("RedBag").transform.localScale = new Vector2(1,1);
			bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
			bagSprite.transform.localScale = new Vector2(0.95f,0.95f);
			gc.loadText("red");
			gc.greenBagCount.enabled = false;
			gc.selectedText = gc.redBagCount;
			break;
		case "red":
			GameObject.FindGameObjectWithTag("YellowBag").GetComponent<addTile>().enabled = true;
			GameObject.FindGameObjectWithTag("YellowBag").GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);
			GameObject.FindGameObjectWithTag("YellowBag").transform.localScale = new Vector2(1,1);
			bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
			bagSprite.transform.localScale = new Vector2(0.95f,0.95f);
			gc.loadText("yellow");
			gc.redBagCount.enabled = false;
			gc.selectedText = gc.yellowBagCount;
			break;
		case "yellow":
			bagSprite.GetComponentInChildren<SpriteRenderer>().color = fade;
			bagSprite.transform.localScale = new Vector2(0.95f,0.95f);
			break;
		}

		StartCoroutine (rotateBag());

	}
	
	public void speedClick(){nextClick = 0;}
	
	private IEnumerator rotateBag()
	{
		float x = bagSprite.transform.localPosition.x;
		float y = bagSprite.transform.localPosition.y;
		
		
		for(int i = 1; i < 50; i++){
			bagSprite.transform.localPosition = new Vector2(x - i/5, y);
			yield return new WaitForSeconds (0.01f);
		}
		
	}

}
