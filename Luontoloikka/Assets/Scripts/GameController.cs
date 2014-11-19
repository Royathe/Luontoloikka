using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController: MonoBehaviour {
	
	public Camera mainCam; //Game Camera
	public GameObject bg; //Background grass image
	public GameObject lossbg; //Background image for loss screen
	public GameObject victorybg; //Background grass for victory screen
	public GameObject exit; //Exit to Menu button
	public GameObject textArea; //Text Box Image
	public GameObject homeCave; //Home base image
	public GameObject start; //Home base image
	public GameObject homeTile; //Home base image
	public GameObject snow1, snow2, snow3, snow4, snow5, snow6; //Snow tiles
	private GameObject forestSnow;
	private GameObject forestSnowHeavy;
	public ParticleSystem snowStorm;
	public ParticleSystem snowing;
	public GameObject forest; //Basic forest image
	public GameObject straightLeft; //Straight road image
	public GameObject straightUp; //Straight road image
	public GameObject curveLeftDown; //Curved road image
	public GameObject curveLeftUp; //Curved road image
	public GameObject curveRightUp; //Curved road image
	public GameObject curveRightDown; //Curved road image
	
	public GameObject greenBagSprite; //Image of the Green Bag
	public GameObject redBagSprite; //Image of the Red Bag
	public GameObject yellowBagSprite; //Image of the Yellow Bag
	
	public GUIText greenBagCount; //Text on top of bag, showing number of items inside
	public GUIText redBagCount; //Text on top of bag, showing number of items inside
	public GUIText yellowBagCount; //Text on top of bag, showing number of items inside
	
	public string entryDirection; //Direction where last piece was pointing in, used by the Rotate Image class
	public string nextDirection; //Direction where current piece is pointing in
	
	private int score; //Number of snow tiles accumulated
	private bool gameOver;
	
	private Quaternion rotation = new Quaternion(0,0,0,0); //Null rotation for Vector3 variables
	
	
	public float spacing; //Space in units between each upperleft corner of a tile
	public float centerTileX; //X coordinate of center tile
	public float centerTileY; //Y coordinate of center tile
	
	private float offSetX; //Offsets the map so it is centered around a specific tile  
	private float offSetY; //Offsets the map so it is centered around a specific tile
	private float getOffSetX(){return ((int)(/*map.GetLength(0) - */centerTileX))*spacing;} //Offsets the map so it is centered around a specific tile
	private float getOffSetY(){return ((int)(/*map.GetLength (1) - */centerTileY)) * spacing + 0.5f;} //Offsets the map so it is centered around a specific tile
	private Vector3 topRightCorner; //Location of the top right corner on the screen
	
	public GameObject[,] map; //2D Array for storing tiles
	public int greenBagSize; //Starting value that sets the capacity of a bag
	public int redBagSize; //Starting value that sets the capacity of a bag
	public int yellowBagSize; //Starting value that sets the capacity of a bag
	public List<GameObject> greenBag; //Bag from which tiles are selected
	public List<GameObject> redBag; //Bag from which tiles are selected
	public List<GameObject> yellowBag; //Bag from which tiles are selected
	public List<string> FlavorList; //Flavor texts currently being used
	public List<string> TaskList; //Task texts currently being used
	private generateTiles gt; //Reference to Generate Tiles class
	private rotateImage ri; //Reference to Rotate Image class
	private centerTile ct; //Reference to Center Tile class
	
	private Color invisible;
	public GUIText selectedText;
	
	// Use this for initialization
	void Start () {
		gameOver = false;

		victorybg.GetComponent<SpriteRenderer>().color = invisible;
		victorybg.SetActive (false);
		lossbg.GetComponent<SpriteRenderer>().color = invisible;
		lossbg.SetActive (false);
		
		//devEnabler = 0;
		//devEnabled = false;
		
		if(staticVariables.gameSize == "short"){
			greenBagSize = 5;
			redBagSize = 10;
		}
		else if(staticVariables.gameSize == "normal"){
			greenBagSize = 10;
			redBagSize = 20;
		}
		else if(staticVariables.gameSize == "long"){
			greenBagSize = 15;
			redBagSize = 30;
		}
		else if(staticVariables.gameSize == "test"){
			greenBagSize = 1;
			redBagSize = 1;
			yellowBagSize = 0;
		}
		
		ct = GameObject.FindGameObjectWithTag ("CenterTile").GetComponent<centerTile> ();
		map = new GameObject[11,11]; //Creates 2D Array for tiles
		score = 0; //Used for tracking the amount of snow on home cave
		
		greenBag = new List<GameObject> (); //Initializes the green bag
		redBag = new List<GameObject> (); //Initializes the green bag
		yellowBag = new List<GameObject> (); //Initializes the green bag
		
		gt = GameObject.FindGameObjectWithTag("GameController").GetComponent<generateTiles>(); //Creates reference to the Generate Tiles class
		ri = GameObject.FindGameObjectWithTag("Rotator").GetComponent<rotateImage>(); //Creates reference to the Rotator class
		
		gt.generate (); //Generates all task & flavor texts and lists all of them
		loadText("green"); //Selects which bag's flavor text list is used
		ri.central = start; //Tells the rotator what the starting image is
		
		//Following IF statements make sure states bag size does not exceed number of possible tiles. If it does, bag size is changed to maximum allowed.
		if (greenBagSize > gt.greenTileList.Count) {greenBagSize = gt.greenTileList.Count;} //Maximum 15
		if (redBagSize > gt.redTileList.Count) {redBagSize = gt.redTileList.Count;} //Maximum 30
		if (yellowBagSize > gt.yellowTileList.Count) {yellowBagSize = gt.yellowTileList.Count;} //Maximum 10
		
		
		//Fills Bag with a random selection of possible green bag texts
		for (int i = 0; i < greenBagSize; i++)
		{
			int tile = (int)Random.Range(0, gt.greenTileList.Count);
			greenBag.Add(gt.greenTileList[tile]);
			gt.greenTileList.RemoveAt(tile);
		}
		//Fills Bag with a random selection of possible green bag texts
		for (int i = 0; i < redBagSize; i++)
		{
			int tile = (int)Random.Range(0, gt.redTileList.Count);
			redBag.Add(gt.redTileList[tile]);
			gt.redTileList.RemoveAt(tile);
		}
		//Fills Bag with a random selection of possible green bag texts
		for (int i = 0; i < yellowBagSize; i++)
		{
			int tile = (int)Random.Range(0, gt.yellowTileList.Count);
			yellowBag.Add(gt.yellowTileList[tile]);
			gt.yellowTileList.RemoveAt(tile);
		}	
		//Adds prerequisite tiles
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.snowTile);
		yellowBag.Add(gt.homeTile);
		//Shuffles Yellow Bag
		for (int i = 0; i < yellowBag.Count; i++) {
			GameObject temp = yellowBag[i];
			int randomIndex = Random.Range(i, yellowBag.Count);
			yellowBag[i] = yellowBag[randomIndex];
			yellowBag[randomIndex] = temp;
		}
		
		greenBagCount.text = "" + greenBag.Count; //Sets number shown on top of Bag
		redBagCount.text = "" + redBag.Count; //Sets number shown on top of Bag
		yellowBagCount.text = "" + yellowBag.Count; //Sets number shown on top of Bag
		
		offSetX = getOffSetX(); //Initializes offset values
		offSetY = getOffSetY(); //Initializes offset values
		
		//Instantiates forest tiles into the Map Array
		for (float y = map.GetLength(1)-1; y > -1; y--) 
		{
			for (float x = 0; x < map.GetLength(0); x++) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				map[(int)x,(int)y] = Instantiate (forest,position,rotation) as GameObject;
				map[(int)x,(int)y].transform.parent = transform;
			}
		}
		//Instantiates Start piece
		Vector2 center = map [(int)centerTileX, (int)centerTileY].transform.position; //Position of center tile
		Destroy(map [(int)centerTileX, (int)centerTileY]);//Removes forest from center tile
		map [(int)centerTileX, (int)centerTileY] = Instantiate (start, center, rotation) as GameObject; // Center tile
		nextDirection = "right"; //Sets what direction start piece is pointing in
		ct.flavorText = "Syksy on jo pitkällä ja pienen karhunpojan olisi aika päästä pesään talviunille. Karhunpoika ei kuitenkaan tiedä reittiä, vaan tarvitsee apuanne sen selvittämiseen. Auttakaa karhu pesään suorittaen matkan varrella vastaan tulevia tehtäviä.	";
		ct.taskText = "Näpäytä keskiruutua sulkeaksi tämän teksti-ikkunan, ja  näpäytä pussia lisätäksesi uuden laatan.";

		//ct.taskText = "\t";
		
		//Instantiates Bags
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(50, 65, 10));
		Instantiate (greenBagSprite, worldPoint, rotation);
		Instantiate (redBagSprite, worldPoint, rotation);
		Instantiate (yellowBagSprite, worldPoint, rotation);
		
		//Instantiates Home Cave image
		topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
		Instantiate (homeCave, topRightCorner, rotation);
		//Instantiates Exit to Menu button
		worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
		//Instantiate (exit, worldPoint, rotation);
		exit.transform.localPosition = worldPoint;
		
		textArea.transform.localPosition = new Vector2(0, -5);
		textArea.GetComponent<textGUI> ().textY = Screen.height;
		
		forestSnow = GameObject.FindGameObjectWithTag ("forestSnow");
		forestSnowHeavy = GameObject.FindGameObjectWithTag ("forestSnowHeavy");
		invisible = new Color (1, 1, 1, 0);
		forestSnow.GetComponent<SpriteRenderer> ().color = invisible;
		forestSnowHeavy.GetComponent<SpriteRenderer> ().color = invisible;

		animateText ();
		
		selectedText = greenBagCount;
		redBagCount.enabled = false;
		yellowBagCount.enabled = false;
		bagCountVisible = true;
	}
	
	//LOADS FLAVOR TEXTS FROM SELECTED BAG FLAVOR TEXT LIST
	public void loadText(string color){
		switch (color) {
			
		case "green":
			FlavorList = gt.GTFlavorList;
			TaskList = gt.GTTaskList;
			break;
		case "red":
			FlavorList = gt.RTFlavorList;
			TaskList = gt.RTTaskList;
			break;
		case "yellow":
			FlavorList = gt.YTFlavorList;
			TaskList = gt.YTTaskList;
			break;
		}
	}
	
	//FUNCTION ADDS SNOW UNTO HOME CAVE IN INCREASING AMOUNTS UP TO 6 TIMES
	public bool addSnow(){
		bool end = false;
		if(score == 0){Instantiate (snow1, topRightCorner, rotation); score += 1; Instantiate (snowStorm); StartCoroutine(fadeInAnimation(forestSnow,4,0.5f));}
		else if(score == 1){Instantiate (snow2, topRightCorner, rotation); score += 1; Instantiate (snowStorm);}
		else if(score == 2){Instantiate (snow3, topRightCorner, rotation); score += 1; Instantiate (snowStorm);}
		else if(score == 3){Instantiate (snow4, topRightCorner, rotation); score += 1; Instantiate (snowStorm); Instantiate(snowing);StartCoroutine(fadeInAnimation(forestSnowHeavy,4,0.5f));}
		else if(score == 4){Instantiate (snow5, topRightCorner, rotation); score += 1; Instantiate (snowStorm);}
		else if(score == 5){Instantiate (snow6, topRightCorner, rotation); score += 1; Instantiate (snowStorm); end = true;}
		return end;
	}
	
	//FUNCTION ADDS NEW TILES TO THE CENTER OF THE MAP. WHEN ROTATING, CENTER IS NOT MOVED, AND CENTER PIECE IS OVERWRITTEN WITH A NEW ONE
	public void newTile(GameObject a, bool rotating){
		if (rotating == false) {
			//Switch-Case checks if piece that is being added can be attached to previous piece. If not, default attachable piece is used instead (Straight Up or Straight Down)
			//Next direction is then updated and the location of the center tile is moved
			entryDirection = nextDirection;
			switch (nextDirection) {
			case "up": 
				centerTileY += 1;
				if (a.GetComponent<tile> ().hasDown == false) {
					a = straightUp;
				}
				nextDirection = a.GetComponent<tile> ().getDirection ("down");
				break;
			case "down": 
				centerTileY -= 1; 
				if (a.GetComponent<tile> ().hasUp == false) {
					a = straightUp;
				}
				nextDirection = a.GetComponent<tile> ().getDirection ("up");
				break;
			case "left": 
				centerTileX -= 1; 
				if (a.GetComponent<tile> ().hasRight == false) {
					a = straightLeft;
				}
				nextDirection = a.GetComponent<tile> ().getDirection ("right");
				break;
			case "right": 
				centerTileX += 1; 
				if (a.GetComponent<tile> ().hasLeft == false) {
					a = straightLeft;
				}
				nextDirection = a.GetComponent<tile> ().getDirection ("left");
				break;
			}
			
			int textSelect = Random.Range (0, FlavorList.Count); //Selects random flavor text task from list
			ct.flavorText = FlavorList [textSelect]; //Assigns new flavor text
			ct.taskText = TaskList [textSelect]; //Assigns new flavor text
			FlavorList.RemoveAt (textSelect); //Removes added flavor text from list
			TaskList.RemoveAt (textSelect); //Removes added flavor text from list
			
			if(devEnabled == false){
				animateText();
			}
			MapUpdate ();
		}
		
		//Updates the "central" object in the RotateImage class with new object
		ri.central = a;
		
		//Adds new tile to center
		Vector2 pos = map [(int)centerTileX, (int)centerTileY].transform.position;
		Destroy(map [(int)centerTileX, (int)centerTileY]);//Removes forest
		map [(int)centerTileX, (int)centerTileY] = Instantiate (a, pos, rotation) as GameObject; // New tile
	}
	
	//FUNCTION MOVES ALL MAP OBJECTS ON SCREEN WHEN NEW TILE IS ADDED, KEEPING NEWEST TILE AT CENTER
	void MapUpdate () {
		
		offSetX = getOffSetX(); //Upates offsets based on new center tile
		offSetY = getOffSetY(); //Upates offsets based on new center tile
		
		if (centerTileY + 5 > map.GetLength(1)) //Checks if center piece is approaching the edge of the map
		{
			expandMap("up",5);
		}
		else if (centerTileX + 5 > map.GetLength(0)) //Checks if center piece is approaching the edge of the map
		{
			expandMap("right",5);
		}
		else if (centerTileX - 5 < 0) //Checks if center piece is approaching the edge of the map
		{
			expandMap("left",5);
		}
		else if (centerTileY - 5 < 0) //Checks if center piece is approaching the edge of the map
		{
			expandMap("down",5);
		}
		
		//Loop moves all objects in array
		for (float y = map.GetLength(1)-1; y > -1; y--) 
		{
			for (float x = 0; x < map.GetLength(0); x++) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				if(map[(int)x,(int)y] != null){
					map[(int)x,(int)y].transform.localPosition = position;
				}
			}
		}
	}
	
	
	//THIS FUNCTION IS MORE INTUITIVE TO USE AND ALLOWS THE ADDITION OF MORE THAN 1 COLUMN OR ROW TWO THE MAP ARRAY
	private void expandMap(string direction, int addition){
		
		switch (direction) {
		case "up":
			for(int i = 0; i < addition; i++){expandMap(0,1);}
			break;
		case "down":
			for(int i = 0; i < addition; i++){expandMap(0,-1);}
			break;
		case "left":
			for(int i = 0; i < addition; i++){expandMap(-1,0);}
			break;
		case "right":
			for(int i = 0; i < addition; i++){expandMap(1,0);}
			break;
			
		}
	}
	
	//THIS FUNCTION EXPANDS THE TWO-DIMENSIONAL ARRAY "MAP" BY 1 INCREMENT IN ONE DIRECTION
	private void expandMap(int xInc, int yInc ){
		
		int newX = map.GetLength(0);
		int newY = map.GetLength(1);
		
		//IF-ELSE PREVENTS SHRINKING THE MAP IF INCREMENT IS < 0
		if (xInc < 0 || yInc < 0) {
			newX = map.GetLength(0) + -xInc;
			newY = map.GetLength(1) + -yInc;
		}else{
			newX = map.GetLength(0) + xInc;
			newY = map.GetLength(1) + yInc;
		}
		
		//CREATES A TEMPORARY NEW MAP TO HOLD THE OLD MAP DATA
		GameObject[,] newMap = new GameObject[newX, newY];
		
		//IF MOVING OUTWARDS FROM 0,0 ALL DATA FROM THE MAP IS COPIED TO THE SAME LOCATION IN THE TEMPORARY MAP
		if (xInc > 0 || yInc > 0) {
			for (int y = map.GetLength(1)-1; y > -1; y--) {
				
				for (int x = 0; x < map.GetLength(0); x++){
					newMap[x,y] = map [x,y];
				}
			}
		}
		//IF MOVING TOWARDS 0,0 ALL COPIED DATA IS SHIFTED ONE SLOT AWAY FROM THE EXPANSION DIRECTION 
		// -So if moving the left, the map is expanded by one, and all contents are shifter to the right
		else if (xInc < 0 || yInc < 0) {
			for (int y = map.GetLength(1)-1; y > -1; y--) {
				
				for (int x = 0; x < map.GetLength(0); x++){
					newMap[x-xInc,y-yInc] = map [x,y];
				}
			}
		}
		//RESIZED TEMPORARY MAP OVERWRITES ORIGINAL MAP
		map = newMap;
		
		//THESE IF-ELSE STATEMENTS FILL THE ADDED COLUMN/ROW WITH FOREST TILES
		//WHEN MOVING TOWARDS 0,0 THE CENTER TILE IS SHIFTED IN ACCORDANCE WITH THE MAP CONTENT SHIFT DONE PREVIOUSLY
		if (xInc > 0) { //MOVING RIGHT
			float x = map.GetLength(0)-1;
			for (float y = map.GetLength(1)-1; y > -1; y--) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				map[(int)x,(int)y] = Instantiate (forest,position,rotation) as GameObject;
				map[(int)x,(int)y].transform.parent = transform;
			}
		}else if (xInc < 0){ //MOVING LEFT
			float x = 0;
			for (float y = map.GetLength(1)-1; y > -1; y--) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				map[(int)x,(int)y] = Instantiate (forest,position,rotation) as GameObject;
				map[(int)x,(int)y].transform.parent = transform;
			}
			centerTileX += 1;
			offSetX = getOffSetX(); //Upates offsets based on new center tile
			offSetY = getOffSetY(); //Upates offsets based on new center tile
			
			
		}else if (yInc < 0){ //MOVING DOWN
			float y = 0;
			for (float x = 0; x < map.GetLength(0); x++) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				map[(int)x,(int)y] = Instantiate (forest,position,rotation) as GameObject;
				map[(int)x,(int)y].transform.parent = transform;
			}
			centerTileY += 1;
			offSetX = getOffSetX(); //Upates offsets based on new center tile
			offSetY = getOffSetY(); //Upates offsets based on new center tile
			
			
		}else if (yInc > 0){ //MOVING UP
			float y = map.GetLength(1)-1;
			for (float x = 0; x < map.GetLength(0); x++) 
			{
				Vector2 position = new Vector2(x*spacing - offSetX, y*spacing - offSetY + 1);
				map[(int)x,(int)y] = Instantiate (forest,position,rotation) as GameObject;
				map[(int)x,(int)y].transform.parent = transform;
			}
		}
	}
	
	public void endGame(string condition){
		if (condition == "victory") {
			victorybg.SetActive (true);
			gameOver = true;
			StartCoroutine (textAreaAnimation());
			StartCoroutine (fadeInAnimation(victorybg,12,0.2f));
			ct.flavorText = "Viimeinkin omassa lämpöisessä kotipesässä!  Pikkukarhu kiittää avusta ja toivottaa mukavaa talvea. Nähdään taas keväällä!";
			ct.taskText = "";
		}
		else if (condition == "loss") {
			lossbg.SetActive (true);
			gameOver = true;
			StartCoroutine (textAreaAnimation());
			StartCoroutine (fadeInAnimation(lossbg,12,0.2f));
			ct.flavorText = "Kotipesän suuaukko on ehtinyt peittyä lumella. Haluatko aloittaa alusta?";
			ct.taskText = "";
		}
	}
	
	public void animateText(){
		if(gameOver == false){
			StartCoroutine (textAreaAnimation());
		}
	}

	private bool bagCountVisible;

	private IEnumerator textAreaAnimation(){
		float destY = -0.55f;
		float origY = -5;
		if(textArea.transform.position.y == origY){ //LIFTS TEXT AREA
			//bagCountVisible = false;
			textArea.GetComponent<textGUI> ().textY -= Screen.height;
			for(int i = 1; i <= 4; i++){
				Vector2 newPos = new Vector2(0, (destY/4)*i);
				textArea.transform.localPosition = newPos;
				yield return new WaitForSeconds (0.05f);
			}
		}
		else if(textArea.transform.position.y == destY){ //DROPS TEXT AREA
			//bagCountVisible = true;
			textArea.GetComponent<textGUI> ().textY += Screen.height;
			for(int i = 4; i >= 0; i--){
				Vector2 newPos = new Vector2(0, origY+i*0.05f);
				textArea.transform.localPosition = newPos;
				yield return new WaitForSeconds (0.05f);
			}
		}
	}
	
	private IEnumerator fadeInAnimation(GameObject snowVersion, float frames, float frameRate){
		invisible.a = 0;
		for(int i = 0; i < frames; i++){
			invisible.a += (1/frames);
			snowVersion.GetComponent<SpriteRenderer>().color = invisible;
			yield return new WaitForSeconds (frameRate);
		}
	}
	
	private int devEnabler;
	private bool devEnabled;
	void Update(){

		if (bagCountVisible == true) {
			selectedText.enabled = true;
		}else{
			selectedText.enabled = false;
		}

		#if UNITY_EDITOR //DEVELOPER OVERRIDES
		devEnabled = true;
		#endif
		
		if (Input.GetKeyUp (KeyCode.LeftShift)){devEnabler += 1;}
		if (Input.GetKeyUp (KeyCode.D)){devEnabler += 1;}
		
		if (devEnabler >= 2) {
			devEnabled = devEnabled == true ? devEnabled = false : devEnabled = true;
			devEnabler = 0;
			GameObject.FindGameObjectWithTag("GreenBag").SendMessage ("speedClick", SendMessageOptions.DontRequireReceiver);
			GameObject.FindGameObjectWithTag("RedBag").SendMessage ("speedClick", SendMessageOptions.DontRequireReceiver);
			GameObject.FindGameObjectWithTag("YellowBag").SendMessage ("speedClick", SendMessageOptions.DontRequireReceiver);
		}
		
		if(devEnabled == true){
			//RESTARTS WITH KEYPAD-ENTER || MOVES AROUND MAP WITH ARROW KEYS || SPACEBAR ADDS NEW TILE || 'R' ROTATES IMAGE
			if (Input.GetKeyDown (KeyCode.KeypadEnter)){Application.LoadLevel (Application.loadedLevel);}
			if (Input.GetKeyDown (KeyCode.R)){GameObject.FindGameObjectWithTag("Rotator").SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver); }
			if (Input.GetKeyUp (KeyCode.UpArrow)){centerTileY += 1; MapUpdate();}
			if (Input.GetKeyUp (KeyCode.DownArrow)){centerTileY -= 1; MapUpdate();}
			if (Input.GetKeyUp (KeyCode.LeftArrow)){centerTileX -= 1; MapUpdate();}
			if (Input.GetKeyUp (KeyCode.RightArrow)){centerTileX += 1; MapUpdate();}
			if (Input.GetKeyUp (KeyCode.Space)) {
				GameObject.FindGameObjectWithTag("YellowBag").SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
				GameObject.FindGameObjectWithTag("RedBag").SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
				GameObject.FindGameObjectWithTag("GreenBag").SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver); 
			}
		}
	}
}